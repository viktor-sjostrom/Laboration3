using System.Data;
using System.Data.SqlClient;

namespace Laboration3.Models
{
    public class StudentMethod
    {

        public int AddStudent(Student student, out string errormsg)
        {
            //Skapa SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";

            //SqlString och lägg till en student i databasen
            String sqlString = "INSERT INTO Tbl_Student (First_Name, Last_Name, Email) VALUES (@firstname, @lastname, @email);";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("firstname", SqlDbType.NVarChar, 255).Value = student.FirstName;
            dbCommand.Parameters.Add("lastname", SqlDbType.NVarChar, 255).Value = student.LastName;
            dbCommand.Parameters.Add("email", SqlDbType.NVarChar, 255).Value = student.Email;

            try
            {
                dbConnection.Open();
                int i = 0;
                i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Det skapades inte en ny student i databasen."; }
                return i; 
                
            } catch (Exception ex)
            {
                errormsg = ex.Message;
                return 0;
            }
            finally { dbConnection.Close(); }
        }

    }
}
