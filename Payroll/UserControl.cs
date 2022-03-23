using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlServerCe;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll
{
    public partial class UserControl : Form
    {
        private DBTracker dbTracker;
        public UserControl()
        {
            InitializeComponent();
            this.dbTracker = new DBTracker(null);
            populateListBox(); 
        }
        
        private void populateListBox()
        {
            foreach (Person p in this.dbTracker.getAllEmployees())

            {
                if (!employeeListBox.Items.Contains("" + p.firstName.Trim() + " " + p.lastName.Trim() + "      " + p.Empl_ID)) { 
                employeeListBox.Items.Add("" + p.firstName.Trim() + " " + p.lastName.Trim() + "      " + p.Empl_ID);
                }
            }
        }

        private void b(object sender, EventArgs e)
        {
            /*grab entered employee*/
            TextBox fnTextBox = (TextBox)firstNameTextBox;
            string employee_firstName = fnTextBox.Text;
            TextBox lnTextBox = (TextBox)lastNameTextBox;
            string employee_lastName = lnTextBox.Text;
            //Console.WriteLine(employee_id);
            TextBox ssn_readTextBox = (TextBox)ssnTextBox; 
            string employee_ssn = ssn_readTextBox.Text;

            TextBox empl_idtextbox = (TextBox)EmployeeIDTextBox; 
            string employee_id = empl_idtextbox.Text;


            /*perform checks on entered data*/ 
            


            if (!this.dbTracker.doesPersonExistInSSNDB(employee_id))
            {
                this.dbTracker.insertIntoSSNDB(employee_id, employee_ssn, employee_firstName, employee_lastName);
            }

            populateListBox(); 

            //change update label
            
        }

        private void selectedEmployeeChanged(object sender, EventArgs e)
        {
            string employee_id = employeeListBox.GetItemText(employeeListBox.SelectedItem);
            int startindex = employee_id.Length - 4;
            int endindex = employee_id.Length - 1; 
            employee_id = employee_id.Substring(startindex, 4);
            ssnDisplayLabel.Text = this.dbTracker.getPersonSSN(employee_id).ssn; 
        }

        private void clickEditSSN(object sender, EventArgs e)
        {



            //var result = System.Windows.Forms.MessageBox.Show("Are you sure you want to exit this app?", "Edit SSN", System.Windows.Forms.MessageBox System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Question, System.Windows.Forms.MessageBoxDefaultButton.Button2, (System.Windows.Forms.MessageBoxOptions)8192 /*MB_TASKMODAL*/);
        }
    }
}
