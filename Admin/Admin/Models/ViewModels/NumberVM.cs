using Admin.Models.db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class NumberVM
    {
        public int Id { get; set; }
        public string Number1 { get; set; }
        public string Name { get; set; }
        public int? Authority_Id { get; set; }
        public string NumberName { get; set; }
        public string NumberNumber { get; set; }


        public virtual Authority Authority { get; set; }
    }
}