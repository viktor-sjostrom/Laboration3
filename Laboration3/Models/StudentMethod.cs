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

        public List<Student> GetStudentsWithDataSet(out string errormsg) 
        {
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";

            String sqlString = "SELECT * FROM Tbl_Student;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            List<Student> studentList = new List<Student>();

            try
            {
                dbConnection.Open();

                myAdapter.Fill(myDS, "myStudent");

                int count = myDS.Tables["myStudent"].Rows.Count;
                int i = 0;

                if(count > 0)
                {
                    while(i < count)
                    {
                        Student student = new Student();
                        student.StudentId = Convert.ToInt32(myDS.Tables["myStudent"].Rows[i]["Student_Id"].ToString());
                    }
                }
            }
        }

    }
}
