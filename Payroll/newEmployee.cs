using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll
{
    public partial class newEmployee : Form
    {
        
        private string firstName, lastName, ssn, emplID, regHours, OTHours;

        private void clickAddNew(object sender, EventArgs e)
        {
            Console.WriteLine("Attemping to add new employee");
            addNewEmployee(); 
            
        }

        public newEmployee()
        {
            InitializeComponent();
        }

        public void addNewEmployee()
        {
            //check if name can be split
            if(newfullNameTextBox.Text.Trim().Count(Char.IsWhiteSpace) == 1)
            {
                splitName(newfullNameTextBox.Text.Trim().ToUpper()); 
            }
            else
            {
                Console.WriteLine("Attempt to add new employee has failed because of Full Name"); 
                clearAllTextBoxes();
                //MessageBox.Show(); //show alert that the input data is incorrect: please provide name as "firstname lastname" with a single space separating them
                return; 
            }

            //check ssn is 4 digits
            if(newSSNTextBox.Text.Trim().Length != 4)
            {
                Console.WriteLine("Attempt to add new employee has failed because of SSN");
                clearAllTextBoxes(); 
                return; 
            }
            DBTracker db = new DBTracker(null); 
            //db.insertIntoPayrollDB(emplID, ssn, firstName, lastName, regHours, OTHours);
        }

        private void splitName(string fullName)
        {
            string[] names = fullName.Split(' ');
            firstName = names[0];
            lastName = names[1];
        }

        private void clearAllTextBoxes()
        {
            newfullNameTextBox.Clear(); 
            newEmployeeIdTextBox.Clear();
            newSSNTextBox.Clear();
            newRegHoursTextBox.Clear();
            newOTHoursTextBox.Clear(); 
        }
    }
}
