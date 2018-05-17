using Hitta.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hitta.Models
{
    public class Hour
    {
        public int Id { get; set; }
        public string Open { get; set; }
        public string Close { get; set; }
        public string Name { get; set; }
        public int Authority_Id { get; set; }


        private SqlOperations sqlOp;
        private DataTable dt;

        public List<Hour> GetHours(int id)
        {
            dt = new DataTable();
            sqlOp = new SqlOperations();

            List<Hour> hours = new List<Hour>();
            Hour h;

            string sql = "SELECT * FROM Hour WHERE Authority_Id =" + id;
            dt = sqlOp.QueryRead(sql);
            try
            {
                foreach (DataRow dr in dt.Rows)
                {
                    h = new Hour()
                    {
                        Id = (int)dr["Id"],
                        Open = (string)dr["Open"],
                        Close = (string)dr["Close"],
                        Name = (string)dr["Name"],
                        Authority_Id = (int)dr["Authority_Id"],

                    };
                    hours.Add(h);
                }
            }
            catch (Exception)
            {

                throw;
            }


            return hours;
        }
    }
}
