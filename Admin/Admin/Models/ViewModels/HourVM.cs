using Admin.Models.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class HourVM
    {
        public int Id { get; set; }

        [DisplayName("Öppnar")]
        public string Open { get; set; }

        [DisplayName("Stänger")]
        public string Close { get; set; }

        [DisplayName("Beskrivning")]
        public string Name { get; set; }

        public Nullable<int> Authority_Id { get; set; }

        public virtual Authority Authority { get; set; }
    }
}