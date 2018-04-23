using Admin.Models.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class NumberVM
    {
        public int Id { get; set; }

        [DisplayName("Nummer")]
        public string Number1 { get; set; }

        [DisplayName("Beskrivning")]
        public string Name { get; set; }

        public int? Authority_Id { get; set; }


        public virtual Authority Authority { get; set; }
    }
}