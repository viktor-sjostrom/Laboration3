using NuGet.Protocol;
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

        public int DeleteStudent(int id, out string errormsg)
        {
            //Skapa SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";


            String sqlString = "DELETE FROM Tbl_Student WHERE Student_Id = @id;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = id;

            try
            {
                dbConnection.Open();
                int i = dbCommand.ExecuteNonQuery();
                if (i == 1) { errormsg = ""; }
                else { errormsg = "Det raderas ingen student i databasen."; }
                return i;
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return 0;
            }
            finally 
            { 
                dbConnection.Close(); 
            }
        }

        public int Update(Student student, out string errormsg)
        {
            //Skapa SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";


            String sqlString = "UPDATE Tbl_Student SET First_Name = @firstname, Last_Name = @lastname, Email = @email WHERE Student_Id = @id;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("firstname", SqlDbType.NVarChar, 255).Value = student.FirstName;
            dbCommand.Parameters.Add("lastname", SqlDbType.NVarChar, 255).Value = student.LastName;
            dbCommand.Parameters.Add("email", SqlDbType.NVarChar, 255).Value = student.Email;
            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = student.StudentId;

            try
            {
                dbConnection.Open();
                int i = dbCommand.ExecuteNonQuery();
                if (i == 1) 
                { 
                    errormsg = ""; 
                }
                else 
                { 
                    errormsg = "Studentens information uppdaterades inte i databasen."; 
                }
                return i;
            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return 0;
            }
            finally
            {
                dbConnection.Close();
            }
        }

        public Student getStudent(int id, out string errormsg)
        {
            //Skapa SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";


            String sqlString = "SELECT * FROM Tbl_Student WHERE Student_Id = @id;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("id", SqlDbType.Int).Value = id;

            //Skapa adapter
            SqlDataAdapter myAdapter = new SqlDataAdapter(dbCommand);
            DataSet myDS = new DataSet();

            try
            {
                dbConnection.Open();

                //Fyller dataset med data i en tabell med namnet myStudent
                myAdapter.Fill(myDS, "myStudent");

                int count = myDS.Tables["myStudent"].Rows.Count;
                int i = 0;

                if(count > 0)
                {
                    Student student = new Student();
                    student.StudentId = Convert.ToInt32(myDS.Tables["myStudent"].Rows[i]["Student_Id"].ToString());
                    student.FirstName = myDS.Tables["myStudent"].Rows[i]["First_Name"].ToString();
                    student.LastName = myDS.Tables["myStudent"].Rows[i]["Last_Name"].ToString();
                    student.Email = myDS.Tables["myStudent"].Rows[i]["Email"].ToString();

                    errormsg = "";
                    return student;
                }
                else
                {
                    errormsg = "Det hämtas ingen student";
                    return null;
                }
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
                        student.FirstName = myDS.Tables["myStudent"].Rows[i]["First_Name"].ToString();
                        student.LastName = myDS.Tables["myStudent"].Rows[i]["Last_Name"].ToString();
                        student.Email = myDS.Tables["myStudent"].Rows[i]["Email"].ToString();

                        i++;
                        studentList.Add(student);
                    }
                    errormsg = "";
                    return studentList;

                } else
                {
                    errormsg = "Det hämtas ingen Student.";
                    return null;
                }
            } catch (Exception e) 
            { 
                errormsg = e.Message;
                return null;
            }
            finally 
            { 
                dbConnection.Close(); 
            }

        }

        public List<Student> GetStudentsWithReader(out string errormsg) 
        {
            //Skapa SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";

            //SqlString ochg för att hämta alla personer
            String sqlString = "SELECT * FROM Tbl_Student;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            //declare the sqlDataReader, which is used in both the try block and the finally block
            SqlDataReader reader = null;

            List<Student> studentList = new List<Student>();

            errormsg = "";

            try
            {
                dbConnection.Open();

                reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student();
                    student.StudentId = Convert.ToInt32(reader["Student_Id"]);
                    student.FirstName = reader["First_Name"].ToString();
                    student.LastName = reader["Last_Name"].ToString();
                    student.Email = reader["Email"].ToString();

                    studentList.Add(student);
                }
                reader.Close();
                return studentList;
            }
            catch (Exception e)
            { 
                errormsg = e.Message;
                return null;
            }
            finally
            { 
                dbConnection.Close(); 
            }
        }

        public List<Student> SearchStudent(string input, out string errormsg)
        {
            //Skapa SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";

            //SqlString och för att hämta info gällande studenter som matchar input
            String sqlString = "SELECT * FROM Tbl_Student WHERE First_Name = @input OR Last_name = @input OR Email = @input;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add("input", SqlDbType.NVarChar, 255).Value = input;

            //declare the sqlDataReader, which is used in both the try block and the finally block
            SqlDataReader reader = null;

            List<Student> studentList = new List<Student>();

            errormsg = "";

            try
            {
                dbConnection.Open();
                
                reader = dbCommand.ExecuteReader();

                while (reader.Read())
                {
                    Student student = new Student();
                    student.StudentId = Convert.ToInt32(reader["Student_Id"]);
                    student.FirstName = reader["First_Name"].ToString();
                    student.LastName = reader["Last_Name"].ToString();
                    student.Email = reader["Email"].ToString();

                    studentList.Add(student);
                }
                reader.Close();
                return studentList;

            } catch (Exception e)
            {
                errormsg= e.Message;
                return null;
            } finally
            {
                dbConnection.Close();
            }


        }

    }
}
