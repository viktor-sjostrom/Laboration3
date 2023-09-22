using System.Data.SqlClient;

namespace Laboration3.Models
{
    public class CourseMethod
    {

        public List<Course> GetCourseList(out String errormsg)
        {
            SqlConnection dbConnection = new SqlConnection();

            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";

            string sqlString = "SELECT * FROM Tbl_Course;";

            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            SqlDataReader reader = null;

            List<Course> courses = new List<Course>();
            errormsg= string.Empty;

            try
            {
                dbConnection.Open();
                reader= dbCommand.ExecuteReader();

                while (reader.Read()) 
                {
                    Course c = new Course();
                    c.CourseId = Convert.ToInt32(reader["Course_Id"]);
                    c.CourseName = reader["Course_Name"].ToString();

                    courses.Add(c);
                }
                reader.Close();
                return courses;

            } catch (Exception ex)
            {
                errormsg= ex.Message;
                return null;

            } finally
            {
                dbConnection.Close();
            }
        }

    }
}
