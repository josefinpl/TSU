using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class UserVM
    {
        public int Id { get; set; }
        [DisplayName("Användarnamn")]
        public string Username { get; set; }
        [DisplayName("Lösenord")]
        public string Password { get; set; }
        [DisplayName("Förnamn")]
        public string Firstname { get; set; }
        [DisplayName("Efternamn")]
        public string Lastname { get; set; }
        [DisplayName("E-post")]
        public string Email { get; set; }
        [DisplayName("Nummer")]
        public string Number { get; set; }
        public Nullable<int> Access_Id { get; set; }
        public string AccessName { get; set; }
        public Nullable<int> Address_Id { get; set; }

        [DisplayName("Adress")]
        public string StreetAddress { get; set; }
        [DisplayName("Postnummer")]
        public int? Zipcode { get; set; }
        [DisplayName("Stad")]
        public string City { get; set; }

        [DisplayName("Namn")]
        public string Fullname { get { return string.Format("{0} {1}", Firstname, Lastname); } }

    }
}