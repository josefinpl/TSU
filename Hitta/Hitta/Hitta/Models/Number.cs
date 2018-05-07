using Hitta.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hitta.Models
{
    public class Number
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Number1 { get; set; }
        public int Authority_Id { get; set; }


        private SqlOperations sqlOp;
        private DataTable dt;

        public List<Number> GetNumbers(int id)
        {
            dt = new DataTable();
            sqlOp = new SqlOperations();

            List<Number> numbers = new List<Number>();
            Number n;

            string sql = "SELECT * FROM Number WHERE Authority_Id =" + id;
            dt = sqlOp.QueryRead(sql);

            foreach (DataRow dr in dt.Rows)
            {
                n = new Number()
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Number1 = (string)dr["Number"],
                    Authority_Id = (int)dr["Authority_Id"]
                };
                numbers.Add(n);
            }

            return numbers;
        }
    }
}
