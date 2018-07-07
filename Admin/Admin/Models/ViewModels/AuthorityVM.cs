using Admin.Models.db;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Admin.Models.ViewModels
{
    public class AuthorityVM
    {
        public int Id { get; set; }

        [DisplayName("Namn")]
        [Required(ErrorMessage = "Ange ett namn")]
        public string Name { get; set; }

        //[DisplayName("Beskrivning")]
        //[Required(ErrorMessage = "Ange en beskrivning")]
        //public string Description { get; set; }

        public int? Address_Id { get; set; }
        public int? Category_Id { get; set; }
        public string CategoryName { get; set; }

        public byte[] Logo { get; set; }

        public virtual Address Address { get; set; }
        public virtual Category Category { get; set; }

        [Required(ErrorMessage = "Ange gatuadress")]
        public string StreetAddress { get; set; }

        [RegularExpression(@"^(\d{5})$", ErrorMessage = "Felaktigt angivet postnummer. Ex: 12345")]
        [Required(ErrorMessage = "Ange postnummer")]
        public int? Zipcode { get; set; }

        [Required(ErrorMessage = "Ange stad")]
        public string City { get; set; }

        public List<NumberVM> Numbers { get; set; }
        public List<HourVM> Hours { get; set; }
      
        public NumberVM Number { get; set; }
        public HourVM Hour { get; set; }
        public bool RightSize { get; set; }
    }
}