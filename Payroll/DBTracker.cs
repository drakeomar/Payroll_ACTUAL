using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms; 

namespace Payroll
{
    internal class DBTracker
    {
        public static DBTracker instance; //singleton? changesss

        private string fileName;

        private string payrollDatabasePath;
        private string payrollDatabaseConnectionString;
        private string SSNDatabasePath;
        private string SSNDatabaseConnectionString;
        public static int ssnFixCount; 

        private SqlCeConnection ssnConnection;
        private string sqlCommandString;

        private int activeEmployeeCount;
        private int totalEmployeeCount; 

        public DBTracker(string fileName)
        {
            if (instance == null)
            {
                instance = this;
                ssnFixCount = 0;
                this.fileName = fileName;
                this.activeEmployeeCount = 0;
                this.totalEmployeeCount = 0;
                this.payrollDatabasePath = @"C:\Users\drake\payroll.sdf";
                this.SSNDatabasePath = @"C:\Users\drake\empl_cred.sdf";
                this.payrollDatabaseConnectionString = "Data Source = " + payrollDatabasePath;
                this.SSNDatabaseConnectionString = "Data Source = " + SSNDatabasePath;

                ssnConnection = new SqlCeConnection("Data Source= " + @"C:\Users\drake\empl_cred.sdf");
                if (!this.doesPayrollDbExist())
                {
                    setupPayrollDB();
                }

                if (!this.doesSSNDbExist())
                {
                    setupSSNDB();
                }
            }
            ssnConnection = new SqlCeConnection("Data Source= " + @"C:\Users\drake\empl_cred.sdf");
        }

        public void setupPayrollDB()
        {

            SqlCeEngine engine = new SqlCeEngine(payrollDatabaseConnectionString);
            try
            {
                engine.CreateDatabase();

                SqlCeConnection connection = new SqlCeConnection("Data Source= " + @"C:\Users\drake\payroll.sdf");

                connection.Open(); 
                sqlCommandString = "CREATE TABLE Payroll_Persons (Empl_id nchar(4) NOT NULL PRIMARY KEY, last_name nchar(10), first_name nchar(10), ssn nchar(5), title nchar(8), regular_hours nchar(5), overtime_hours nchar(5));";


                SqlCeCommand command = new SqlCeCommand(sqlCommandString, connection);
                command.ExecuteNonQuery();
                connection.Close();

                MessageBox.Show("DataBase '" + payrollDatabasePath + "' was created successfully");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Exception in CreateDatabase " + ex.ToString(), "Exception in CreateDatabase", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                engine.Dispose();
            }

        }


        public void setupSSNDB()
        {

            SqlCeEngine engine = new SqlCeEngine(SSNDatabaseConnectionString);
            try
            {
                engine.CreateDatabase();
                
                ssnConnection.Open();

                sqlCommandString = "CREATE TABLE Persons (Empl_ID nchar(4) NOT NULL PRIMARY KEY, ssn nchar(5), FirstName nchar(20), LastName nchar(20), Active bit);";
    

                SqlCeCommand command = new SqlCeCommand(sqlCommandString, ssnConnection);
                command.ExecuteNonQuery();
                ssnConnection.Close();

                MessageBox.Show("DataBase '" + SSNDatabasePath + "' was created successfully");
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Exception in CreateDatabase " + ex.ToString(), "Exception in CreateDatabase", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                engine.Dispose();
            }

        }

        public SqlCeConnection getSsnConnection()
        {
            return this.ssnConnection; 
        }

        

        public void insertIntoSSNDB(string empl_id, string num, string fn, string ln)
        {
            string saveEmployee = "INSERT into Persons (Empl_ID, ssn, FirstName, LastName, Active) VALUES (@Empl_ID, @ssn, @FirstName, @LastName, @Active)";

            using (SqlCeConnection openCon = new SqlCeConnection("Data Source= " + @"C:\Users\drake\empl_cred.sdf"))
            {
                //openCon.Open();
                using (SqlCeCommand querySaveEmployee = new SqlCeCommand(saveEmployee))
                {
                    
                    querySaveEmployee.Connection = openCon;
                    querySaveEmployee.Parameters.Add("@Empl_ID", SqlDbType.NChar, 4).Value = empl_id;
                    querySaveEmployee.Parameters.Add("@ssn", SqlDbType.NChar, 5).Value = num;
                    querySaveEmployee.Parameters.Add("@FirstName", SqlDbType.NChar, 20).Value = fn;
                    querySaveEmployee.Parameters.Add("@LastName", SqlDbType.NChar, 20).Value = ln;
                    querySaveEmployee.Parameters.Add("@Active", SqlDbType.Bit, 1).Value = 1; 
                    openCon.Open();
                    

                    querySaveEmployee.ExecuteNonQuery();
                }
            }
        }

        public void insertIntoPayrollDB(string empl_id, string num, string fn, string ln, string reg_hours, string over_hours)
        {
            string saveEmployee = "INSERT into Payroll_Persons (Empl_ID, last_name, first_name, ssn, title, regular_hours, overtime_hours) VALUES (@Empl_ID, @first_name, @last_name, @ssn, @title, @reg_hours, @ot_hours)";

            using (SqlCeConnection openCon = new SqlCeConnection("Data Source= " + @"C:\Users\drake\payroll.sdf"))
            {
                //openCon.Open();
                using (SqlCeCommand querySaveEmployee = new SqlCeCommand(saveEmployee))
                {

                    querySaveEmployee.Connection = openCon;
                    querySaveEmployee.Parameters.Add("@Empl_ID", SqlDbType.NChar, 4).Value = empl_id;
                    querySaveEmployee.Parameters.Add("@ssn", SqlDbType.NChar, 5).Value = num;
                    querySaveEmployee.Parameters.Add("@first_name", SqlDbType.NChar, 20).Value = fn;
                    querySaveEmployee.Parameters.Add("@last_name", SqlDbType.NChar, 20).Value = ln;
                    querySaveEmployee.Parameters.Add("@title", SqlDbType.NChar, 8).Value = "Primary";
                    querySaveEmployee.Parameters.Add("@reg_hours", SqlDbType.Float, 3).Value = reg_hours;
                    querySaveEmployee.Parameters.Add("@ot_hours", SqlDbType.Float, 3).Value = over_hours; 
                    openCon.Open();


                    querySaveEmployee.ExecuteNonQuery();
                }
            }
        }

        public void updatePayroll(string empl_id, string param, string var)
        {
            string cmd = @"UPDATE Payroll_Persons 
                                SET " + param + " = @var WHERE Empl_ID = @Empl_ID";
                                
            using (SqlCeConnection connection = new SqlCeConnection("Data Source= " + @"C:\Users\drake\payroll.sdf"))
            {
                
                using (SqlCeCommand command = new SqlCeCommand(cmd, connection))
                {
                    command.Parameters.AddWithValue("@var", var);
                    command.Parameters.AddWithValue("@Empl_ID", empl_id);

                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
            } 
                
        }

        public Person getPersonSSN(string empl_id)
        {
            Person p = new Person();

            using (SqlCeConnection myConnection = new SqlCeConnection("Data Source= " + @"C:\Users\drake\empl_cred.sdf"))
            {
                myConnection.Open();
                SqlCeCommand command = new SqlCeCommand("SELECT * from [Persons] WHERE Empl_ID=@id", myConnection);
                command.Parameters.AddWithValue("@id", empl_id);

                using (SqlCeDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        
                        p.Empl_ID = reader["Empl_ID"].ToString();
                        p.ssn = reader["ssn"].ToString(); 
                        return p; 
                    }
                }
            }

            return p; 
            
        }


        public Boolean doesPersonExistInPayroll(string id)
        {
            SqlCeConnection connection = new SqlCeConnection("Data Source= " + @"C:\Users\drake\payroll.sdf");
            using (SqlCeConnection myConnection = connection)
            {
                //myConnection.Open();
                SqlCeCommand command = new SqlCeCommand("SELECT * from [Payroll_Persons] WHERE Empl_id=@id", myConnection);
                command.Parameters.AddWithValue("@id", id);
                myConnection.Open();
                using (SqlCeDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("TESTING");
                        Console.WriteLine(reader["Empl_ID"].ToString());
                        Console.WriteLine(reader["ssn"].ToString());
                        return true;
                    }
                }
            }
            
            return false;
        }

        public Boolean doesPersonExistInSSNDB(string empl_id)
        {
            SqlCeConnection connection = new SqlCeConnection("Data Source= " + @"C:\Users\drake\empl_cred.sdf");
            using (SqlCeConnection myConnection = connection)
            {
                //myConnection.Open();
                SqlCeCommand command = new SqlCeCommand("SELECT * from [Persons] WHERE Empl_id=@id", myConnection);
                command.Parameters.AddWithValue("@id", empl_id);
                myConnection.Open();
                using (SqlCeDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Console.WriteLine("TESTING");
                        Console.WriteLine(reader["Empl_ID"].ToString());
                        Console.WriteLine(reader["ssn"].ToString());
                        return true;
                    }
                }
            }
            return false; 
        }

        public List<Person> getAllPayrollEmployees()
        {
            List<Person> payrollEmployees = new List<Person>(); 

            SqlCeConnection con = new SqlCeConnection();
            //con = new System.Data.SqlClient.SqlCeConnection();
            con.ConnectionString = "Data Source =C:\\Users\\drake\\payroll.sdf;";
            using (SqlCeConnection myConnection = con)
            {
                myConnection.Open();
                SqlCeCommand command = new SqlCeCommand("SELECT * from [Payroll_Persons]", myConnection);
                //command.Parameters.AddWithValue("@id", empl_id);

                using (SqlCeDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person p = new Person();
                        p.Empl_ID = reader["Empl_ID"].ToString();
                        p.ssn = reader["ssn"].ToString();
                        p.firstName = reader["first_name"].ToString();
                        p.lastName = reader["last_name"].ToString();
                        p.reg_hours = reader["regular_hours"].ToString();
                        p.ot_hours = reader["overtime_hours"].ToString();

                        Console.WriteLine("TESTING");
                        payrollEmployees.Add(p);
                        
                    }
                    myConnection.Close(); 
                    return payrollEmployees;
                }
            }
            
                
        }

        public List<Person> getAllEmployees()
        {
            List<Person> employees = new List<Person>();
            SqlCeConnection con = new SqlCeConnection();
            //con = new System.Data.SqlClient.SqlCeConnection();
            con.ConnectionString = "Data Source =C:\\Users\\drake\\empl_cred.sdf;";
            using (SqlCeConnection myConnection = con)
            {
                myConnection.Open();
                SqlCeCommand command = new SqlCeCommand("SELECT * from [Persons]", myConnection);
                //command.Parameters.AddWithValue("@id", empl_id);

                using (SqlCeDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Person p = new Person();
                        p.Empl_ID = reader["Empl_ID"].ToString();
                        p.ssn = reader["ssn"].ToString();
                        p.firstName = reader["FirstName"].ToString();
                        p.lastName = reader["LastName"].ToString(); 

                        employees.Add(p);
                        
                    }
                    myConnection.Close(); 
                    return employees;
                }
            }
        }

        public Boolean doesPayrollDbExist()
        {
            return File.Exists(payrollDatabasePath); 
        }

        public Boolean doesSSNDbExist()
        {
            return File.Exists(SSNDatabasePath);
        }
        public void deletePayrollDB()
        {
            if (File.Exists(payrollDatabasePath))
            {
                try
                {
                    File.Delete(payrollDatabasePath);
                    MessageBox.Show("DataBase '" + payrollDatabasePath + "' was deleted successfully");
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "Exception in DeleteDatabase '" + payrollDatabasePath + "'", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
