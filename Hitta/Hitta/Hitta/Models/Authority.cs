using System;
using System.Collections.Generic;
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


    }
}
