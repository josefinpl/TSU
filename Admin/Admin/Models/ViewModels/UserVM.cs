using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Number { get; set; }
        public Nullable<int> Access_Id { get; set; }
        public string AccessName { get; set; }
        public Nullable<int> Address_Id { get; set; }

        public string StreetAddress { get; set; }
        public int? Zipcode { get; set; }
        public string City { get; set; }

        public string Fullname { get { return string.Format("{0} {1}", Firstname, Lastname); } }

    }
}