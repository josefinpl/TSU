using Hitta.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Hitta.Models
{
    public class Address
    {

        public int Id { get; set; }
        public string Address1 { get; set; }
        public int? Zipcode { get; set; }
        public string City { get; set; }

        private SqlOperations sqlOp;
        private DataTable dt;

        Address a;

        public Address GetAddress(int Id)
        {
            dt = new DataTable();
            sqlOp = new SqlOperations();
                        

            string sql = "SELECT * FROM Address WHERE Id =" + Id;
            dt = sqlOp.QueryRead(sql);

            foreach (DataRow dr in dt.Rows)
            {
                a = new Address()
                {
                    Id = (int)dr["Id"],
                    Address1 = (string)dr["Address"],
                    Zipcode = (int)dr["Zipcode"],
                    City = (string)dr["City"]
                };
               
            }

            return a;
        }

    }
}
