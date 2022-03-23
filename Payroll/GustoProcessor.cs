
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Payroll
{
    internal class GustoProcessor
    {
        private string filePath;
        private DBTracker dbTracker = new DBTracker(null); 

        public enum csvColumn : int
        {
            ID = 0,
            FullName = 1,
            RegHours = 5,
            OTHours = 6
        }

        public GustoProcessor(string filePath)
        {
            this.filePath = filePath;

        }

        public Boolean areHeadersValid(String[] headers)
        {
            if (headers[0].Equals("EmployeeCode") && headers[1].Equals("EmployeeDescription")
                && headers[2].Equals("TotProd")
                && headers[3].Equals("TotNonProdHrs")
                && headers[4].Equals("ProdPercent")
                && headers[5].Equals("TotRegHrs")
                && headers[6].Equals("TotOTHrs"))
            {

                return true;
            }

            
            return false;
        }

        public void parse()
        {
            DBTracker db = new DBTracker(null);
            Boolean isValid = false;
            /////https://stackoverflow.com/questions/3507498/reading-csv-files-using-c-sharp
            using (TextFieldParser parser = new TextFieldParser(@filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                int i = 0;
                while (!parser.EndOfData)
                {
                    if (i == 0)
                    {
                        //first line: headers
                        string[] headers = parser.ReadFields();
                        isValid = this.areHeadersValid(headers);
                    }
                    else
                    {
                        //Processing row
                        string[] fields = parser.ReadFields();

                        string[] fullname = fields[(int)csvColumn.FullName].Trim().Split(' ');
                        string fn = fullname[1];
                        string ln = fullname[0];

                        if (!db.doesPersonExistInSSNDB(fields[(int)csvColumn.ID]))
                        {
                            db.insertIntoSSNDB(fields[(int)csvColumn.ID], "00000", fn, ln);
                        }

                        if (!db.doesPersonExistInPayroll(fields[(int)csvColumn.ID]))
                        {
                            db.insertIntoPayrollDB(fields[(int)csvColumn.ID], "00000", fn, ln, fields[(int)csvColumn.RegHours], fields[(int)csvColumn.OTHours]);
                        }
                    }
                    i++;
                }
            }

        }

        public void createE2Format(string filename, string path)
        {
            List<string[]> csvTable = new List<string[]>();
            List<Person> payrollEmployees = this.dbTracker.getAllPayrollEmployees();

            using (var w = new StreamWriter(path + filename))
            {
                foreach (Person p in payrollEmployees)
                {
                    var line = string.Format("{0},{1},{2},{3},{4},{5},{6}", p.Empl_ID, p.ssn, p.firstName, p.lastName, "Primary", p.reg_hours, p.ot_hours);
                    w.WriteLine(line);
                    w.Flush(); 
                }
            }
        }

        /*check if exists within ssn db, add if it does, mark for correction if it does not*/
        private void add_ssn()
        {

        }
    }
}

    

