using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll
{
    public partial class MasterView : Form
    {
        private string fileText;
        private string fileName;
        private int selectedCellRow = 0; 
        private int selectedCellColumn = 4;
        private int ssnFixCount = 0; 
        private Boolean emptyTable = true; 
        DBTracker dbTracker;
        GustoProcessor gp; 

        public MasterView()
        {
            InitializeComponent();
            this.AllowDrop = true;
            this.DragEnter += new DragEventHandler(Form1_DragEnter);
            this.DragDrop += new DragEventHandler(Form1_DragDrop);
            this.dbTracker = new DBTracker(null);
            populateEmployeeView(); 
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.LightSeaGreen;
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            fileNameLabel.Text = "Payroll_" + DateTime.Today.ToString("MM_dd_yyyy"); //print current date as default filename to create
        }

        public void populateEmployeeView()
        {
            

            string[] employeeRow = new string[6];
            List<Person> payrollEmployees = this.dbTracker.getAllPayrollEmployees();
            //var employeeView = listView1; 
            if(payrollEmployees.Count == 0)
            {
                dataGridView1.Rows.Clear();
                ssnFixCount--; 
                return;
            }

            emptyTable = false; 
            foreach (Person payrollEmp in payrollEmployees)
            {

                //listview\
                /*
                employeeRow[0] = payrollEmp.Empl_ID;
                employeeRow[1] = payrollEmp.ssn;
                employeeRow[2] = payrollEmp.firstName;
                employeeRow[3] = payrollEmp.lastName;
                employeeRow[4] = payrollEmp.reg_hours;
                employeeRow[5] = payrollEmp.ot_hours;
                var listViewItem = new ListViewItem(employeeRow);
                employeeView.Items.Add(listViewItem);
                */

                //datagridview not binded to datasource
                int rowId = dataGridView1.Rows.Add(); //add row
                DataGridViewRow row = dataGridView1.Rows[rowId]; //grab row to fill in

                row.Cells["Employee_ID_column"].Value = payrollEmp.Empl_ID;
                row.Cells["FirstName_column"].Value = payrollEmp.firstName;
                row.Cells["LastName_column"].Value = payrollEmp.lastName;
                if (payrollEmp.ssn == "00000") {
                    row.Cells["SSN_column"].Value = "MISSING";
                    ssnFixCount++; 
                }
                else
                {
                    row.Cells["SSN_column"].Value = "FOUND"; 
                }
                row.Cells["Regular_Hours_column"].Value = payrollEmp.reg_hours;
                row.Cells["Overtime_Hours_column"].Value = payrollEmp.ot_hours; 


            }
        }
        public void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        public void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            foreach (string file in files) Console.WriteLine(file);
        }


        private void clickChooseFile(object sender, EventArgs e)
        {
            int size = -1;

            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;
                Console.WriteLine(file); 
                try
                {
                    //fileText = File.ReadAllText(file);

                    //size = fileText.Length;

                    gp = new GustoProcessor(file);
                     
                    //List<string[]> csvParsed = gp.parse(); 
                    gp.parse();
                    populateEmployeeView();
                    ///foreach(string[] row in csvParsed)
                    //{

                    //}
                    //Console.WriteLine(row);
                }
                catch (IOException)
                {
                    Console.WriteLine("IOException thrown");
                }
            }
            //Console.WriteLine(size); // <-- Shows file size in debugging mode.
            //Console.WriteLine(result); // <-- For debugging use.

            /*test if selected file is valid-- check is csv, check csv format is as expected*/
            /*
            if (fileText == null)
            {
                Console.WriteLine("null file chosen"); 
            }
            else if (!Path.GetExtension(fileName).Equals(".csv"))
            {

                Console.WriteLine("Incorrect filetype for action");

            }
            else if (fileText.Substring(0,10).Equals("LastName, FirstName, hours"))
            {

                Console.WriteLine("Matching with expected csv columns");

            }
            */

        }

        private void populateListView()
        {

        }

        private void clickEditEmployees(object sender, EventArgs e)
        {
            //this.IsMdiContainer = true;
            UserControl editEmployeesForm = new UserControl();
            //editEmployeesForm.TopLevel = false;
            //editEmployeesForm.Parent = this; 
            this.Hide();
            editEmployeesForm.Closed += (s, args) => this.Show(); 
            editEmployeesForm.Show(); 
            
            
        }

        private void cellClick(object sender, DataGridViewCellEventArgs e)
        {
            //DataGridCell cell = (DataGridCell)sender;
            //if(cell.ColumnNumber == 4 || cell.ColumnNumber == 5)
            //{

            //}
        }

        private void onStateChange(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell == null || e.StateChanged != DataGridViewElementStates.Selected)
            {
                return;
            }

            if(e.Cell.ColumnIndex == 4)
            {
                if (e.Cell.Selected)
                {
                    e.Cell.Style.BackColor = Color.Gainsboro;
                }
                else
                {
                    e.Cell.Style.BackColor = Color.White; 
                }
                return;
            }

            if(e.Cell.ColumnIndex == 5)
            {
                if (e.Cell.Selected)
                {
                    e.Cell.Style.BackColor = Color.Gainsboro;
                }
                else
                {
                    e.Cell.Style.BackColor = Color.White;
                }
                return; 
            }
            if (!e.Cell.Selected)
            {
                return; 
            }

            if(e.Cell.ColumnIndex < 4 )
            {
                e.Cell.Selected = false;
                
                //e.Cell.DataGridView.Rows[selectedCellRow].Cells[selectedCellColumn].Selected = true; ;
            }
            else
            {
                selectedCellRow = e.Cell.RowIndex;
                selectedCellColumn = e.Cell.ColumnIndex; 
            }

        }

        private void cellUpdated(object sender, DataGridViewCellEventArgs e)
        {
            string param= null;

            if (emptyTable) { return; }
            
            if(e.RowIndex < 0 || e.ColumnIndex < 2) { return; }

            string empl_id = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();

            string updated_val = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();

            switch (e.ColumnIndex)
            {
                case 2:
                    param = "first_name";
                    break; 
                case 3:
                    param = "last_name";
                    break; 
                case 4:
                    param = "regular_hours";
                    break; 
                case 5:
                    param = "overtime_hours";
                    break;
                default:
                    return; 
                    
            }
            this.dbTracker.updatePayroll(empl_id, param, updated_val);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void clickDeleteDB(object sender, EventArgs e)
        {
            //clean slate
            this.dbTracker.deletePayrollDB();
            this.dbTracker.setupPayrollDB();
            populateEmployeeView();
        }

        private void clickCreateCSV(object sender, EventArgs e)
        {
            gp = new GustoProcessor(null); 
            gp.createE2Format(fileNameLabel.Text + ".csv", "C:\\Users\\drake\\");
        }

        private void clickAddNew(object sender, EventArgs e)
        {
            Form addNewPopUp = new newEmployee(); 
            var DialogResult = addNewPopUp.ShowDialog(this);

            if (DialogResult == DialogResult.OK)
            {
                
                addNewPopUp.Close(); 
            }else if(DialogResult == DialogResult.Cancel)
            {
                addNewPopUp.Close();
            }
        }
    }

}
