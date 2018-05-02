using Hitta.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Hitta.Data
{
    public class SqlOperations
    {
        private DataTable dt;
        private SqlDataReader dr;
        private SqlCommand cmd;

        #region Metod som används för att connecta till databas
        public SqlConnection SqlConnection()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = "tusjos18.database.windows.net";
            builder.InitialCatalog = "tusjose";
            builder.UserID = "josefin";
            builder.Password = "TUSjos123";
            SqlConnection connection = new SqlConnection(builder.ConnectionString);
            return connection;
        }
        #endregion

        #region Metod som läser en query och returnerar ett datatable
        public DataTable QueryRead(string sql)
        {
            SqlConnection conn = SqlConnection();

            try
            {
                conn.Open();
                cmd = new SqlCommand(sql, conn);
                dr = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(dr);
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
        }
        #endregion

        public List<Authority> GetAuthorities()
        {
            List<Authority> authorities = new List<Authority>();
            Authority a;
            
            string sql = "SELECT * FROM Authority";
            dt = QueryRead(sql);

            foreach (DataRow dr in dt.Rows)
            {
                a = new Authority()
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Description = (string)dr["Description"],
                    Address_Id = (int)dr["Address_Id"],
                    Category_Id= (int)dr["Category_Id"],
                    Logo = (byte[])dr["Logo"]
                };
                authorities.Add(a);
            }

            return authorities;
        }

    }
}
