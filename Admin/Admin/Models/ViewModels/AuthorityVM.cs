using Admin.Models.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class AuthorityVM
    {
        public int Id { get; set; }
        [DisplayName("Namn")]
        public string Name { get; set; }
        [DisplayName("Beskrivning")]
        public string Description { get; set; }
        public Nullable<int> Address_Id { get; set; }
        public Nullable<int> Category_Id { get; set; }
        public byte[] Logo { get; set; }

        public virtual Address Address { get; set; }
        public virtual Category Category { get; set; }

        public string StreetAddress { get; set; }
        public int? Zipcode { get; set; }
        public string City { get; set; }

        public string CategoryName { get; set; }
    }
}