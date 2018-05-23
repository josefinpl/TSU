using Hitta.Data;
using Hitta.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xamarin.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Hitta.Models
{
    public class Authority
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Address_Id { get; set; }
        public int Category_Id { get; set; }
        public byte[] Logo { get; set; }

        public ImageSource Image { get; set; }

        public string Address1 { get; set; }
        public string City1 { get; set; }  
        public int? Zipcode1 { get; set; }   

        public string MapAddress { get; set; }
        public string HelpText { get; set; }

        private SqlOperations sqlOp;
        private DataTable dt;

        Authority auth;

        public List<Authority> GetAuthorities()
        {
            dt = new DataTable();
            sqlOp = new SqlOperations();

            List<Authority> authorities = new List<Authority>();
            Authority a;

            string sql = "SELECT * FROM Authority ORDER BY Name ASC";
            dt = sqlOp.QueryRead(sql);

            foreach (DataRow dr in dt.Rows)
            {
                a = new Authority()
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Description = (string)dr["Description"],
                    Address_Id = (int)dr["Address_Id"],
                    Category_Id = (int)dr["Category_Id"],
                    Logo = (byte[])dr["Logo"]
                };
                authorities.Add(a);
            }



            return authorities;
        }
        public Authority GetAuthority(string id)
        {
            dt = new DataTable();
            sqlOp = new SqlOperations();

            string sql = "SELECT * FROM Authority WHERE Id =" + id;
            dt = sqlOp.QueryRead(sql);
            

            foreach (DataRow dr in dt.Rows)
            {
                auth = new Authority()
                {
                    Id = (int)dr["Id"],
                    Name = (string)dr["Name"],
                    Description = (string)dr["Description"],
                    Address_Id = (int)dr["Address_Id"],
                    Category_Id = (int)dr["Category_Id"],
                    Logo = (byte[])dr["Logo"]
                };
                
            }

            return auth;
        }

    }
}
