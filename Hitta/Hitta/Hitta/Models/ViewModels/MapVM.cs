using System;
using System.Collections.Generic;
using System.Text;

namespace Hitta.Models.ViewModels
{
    public class MapVM
    {
        public string StreetAdress { get; set; }
        public Authority Authority { get; set; }
        public MapVM (string adress, Authority auth)
        {
            StreetAdress = adress;
            Authority = auth;
            
        }

    }
}
