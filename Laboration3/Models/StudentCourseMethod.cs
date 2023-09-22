using System.Data;
using System.Data.SqlClient;

namespace Laboration3.Models
{
    public class StudentCourseMethod
    {

        public List<StudentCourse> GetStudentCourse(out string errormsg)
        {
            //Skapa sqlConnetion
            SqlConnection dbConnection = new SqlConnection();

            //Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";


            //SqlString och för att hämta förnamn, efternmn och kursnamn
            String sqlString = "SELECT Tbl_Student.First_Name, Tbl_Student.Last_Name, Tbl_Course.Course_Name FROM Tbl_Student INNER JOIN Tbl_Registration ON Tbl_Student.Student_Id = Tbl_Registration.Student_Id INNER JOIN Tbl_Course ON Tbl_Registration.Course_Id = Tbl_Course.Course_Id;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            //Declare the sqlDataReader, which is used in
            //both the try block and the finally block
            SqlDataReader reader = null;

            List<StudentCourse> studentCourseList = new List<StudentCourse>();

            errormsg = "";

            try
            {
                //open conneciton
                dbConnection.Open();

                //1. get an instance of the sqlDataReader
                reader = dbCommand.ExecuteReader();

                //2. read necessary columns in each record

                while (reader.Read())
                {
                    StudentCourse sc = new StudentCourse();
                    sc.FirstName = reader["First_Name"].ToString();
                    sc.LastName = reader["Last_Name"].ToString();
                    sc.CourseName = reader["Course_Name"].ToString();

                    studentCourseList.Add(sc);
                }

                reader.Close();
                return studentCourseList;

            }catch(Exception ex)
            {
                errormsg= ex.Message;
                return null;
            } finally
            { 
                dbConnection.Close(); 
            }

        }

        public List<StudentCourse> GetStudentCourses(out string errormsg, int filterId)
        {
            //Skapa sqlConnetion
            SqlConnection dbConnection = new SqlConnection();

            //Koppling mot SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";

            String sqlString = "SELECT Tbl_Student.First_Name, Tbl_Student.Last_Name, Tbl_Course.Course_Name FROM Tbl_Student INNER JOIN Tbl_Registration ON Tbl_Student.Student_Id = Tbl_Registration.Student_Id INNER JOIN Tbl_Course ON Tbl_Registration.Course_Id = Tbl_Course.Course_Id WHERE Tbl_Course.Course_id = @filterId;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("filterId", SqlDbType.Int). Value = filterId;

            SqlDataReader reader = null;

            List<StudentCourse> studentCourseList = new List<StudentCourse>();

            errormsg = string.Empty;

            try
            {
                dbConnection.Open();

                reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    StudentCourse sc = new StudentCourse();
                    sc.FirstName = reader["First_Name"].ToString();
                    sc.LastName = reader["Last_Name"].ToString();
                    sc.CourseName = reader["Course_Name"].ToString();

                    studentCourseList.Add(sc);
                }
                reader.Close();
                return studentCourseList;

            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return null;
            }
            finally
            {
                dbConnection.Close();
            }
        }
    }
}
