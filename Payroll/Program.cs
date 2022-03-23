using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Payroll
{
    internal static class Program

    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //connect to database
            string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; 
                                    
                                    Integrated Security = True";
            //SqlConnection conn = new SqlConnection(connectionString); 
            //conn.Open();
            //MessageBox.Show("Database Connection Successful!"); 
            //conn.Close();

            
                //SqlCommand cmd = null;
                //using (var connection = new SqlConnection(connectionString))
                //{
                 //   connection.Open();

                  //  using (cmd = new SqlCommand($"If(db_id(N'Database20') IS NULL) CREATE DATABASE [Database20]", connection))
                  //  {
                   //     cmd.ExecuteNonQuery();
                   // }
              //  }

            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MasterView());

            DBTracker db = new DBTracker("dummy"); 

        }
    }
}
