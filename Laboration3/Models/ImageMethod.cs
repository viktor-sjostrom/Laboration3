using System.Data.SqlClient;
using System.Data;

namespace Laboration3.Models
{
    public class ImageMethod
    {

        public int AddImage(byte[] imageData, string contentType, out string errormsg)
        {
            //Skapa SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";

            //SqlString och lägg till en student i databasen
            string sqlString = "INSERT INTO [Tbl_Imagesss] (ImageData, ContentType) VALUES (@data, @content);";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);

            dbCommand.Parameters.Add(new SqlParameter("@data", SqlDbType.VarBinary) { Value = imageData });
            dbCommand.Parameters.Add(new SqlParameter("@content", SqlDbType.NVarChar, 255) { Value = contentType });


            try
            {
                dbConnection.Open();
                int rowsAffected = dbCommand.ExecuteNonQuery();
                errormsg = rowsAffected == 1 ? "" : "No new image was added to the database.";
                return rowsAffected;

            }
            catch (Exception ex)
            {
                errormsg = ex.Message;
                return 0;
            }
            finally { dbConnection.Close(); }
        }

        public byte[] GetImageData(int imageId, out string errormsg)
        {
            //Skapa SqlConnection
            SqlConnection dbConnection = new SqlConnection();

            //Koppling till SQL Server
            dbConnection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=School_Register;Integrated Security=True";

            string sqlString = "SELECT ImageData FROM Tbl_Imagesss WHERE Id = @imageId;";
            SqlCommand dbCommand = new SqlCommand(sqlString, dbConnection);
            dbCommand.Parameters.Add(new SqlParameter("@imageId", SqlDbType.Int) { Value = imageId });

            try
            {
                dbConnection.Open();
                using (SqlDataReader reader = dbCommand.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Check if the column exists and it's not null
                        if (!(reader.IsDBNull(0)))
                        {
                            byte[] imageData = (byte[])reader["ImageData"];
                            errormsg = "";
                            return imageData;
                        }
                    }
                }

                // If no image data found for the given imageId
                errormsg = "Image not found in the database.";
                return null;
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
