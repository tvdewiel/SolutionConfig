using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class ADOLogDAO
    {
        private string connectionString;

        public ADOLogDAO(string connectionString)
        {
            this.connectionString = connectionString;
        }
        private SqlConnection getConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            return connection;
        }
        public int WriteToDB(string loginfo) //returns recordId
        {
            SqlConnection connection = getConnection();
            string query = "INSERT INTO dbo.logTable (info) output INSERTED.Id VALUES(@info)";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.Parameters.Add(new SqlParameter("@info", SqlDbType.NVarChar));
                    command.CommandText = query;
                    command.Parameters["@info"].Value = loginfo;
                    return (int)command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new DAOException("WriteToDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
        public List<(int, string)> ReadFromDB(int n = 10)
        {
            List<(int, string)> logs = new List<(int, string)>();
            SqlConnection connection = getConnection();
            string query = "SELECT Top(@n) * FROM dbo.logTable ORDER BY id DESC";
            using (SqlCommand command = connection.CreateCommand())
            {
                connection.Open();
                try
                {
                    command.CommandText = query;
                    command.Parameters.Add(new SqlParameter("@n", SqlDbType.Int));
                    command.Parameters["@n"].Value = n;
                    SqlDataReader dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string info = (string)dataReader["info"];
                        int id = (int)dataReader["id"];
                        logs.Add((id, info));
                    }
                    dataReader.Close();
                    return logs;
                }
                catch (Exception ex)
                {
                    throw new DAOException("ReadFromDB", ex);
                }
                finally
                {
                    connection.Close();
                }
            }
        }
    }
}
