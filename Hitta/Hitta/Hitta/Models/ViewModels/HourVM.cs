using System;
using System.Collections.Generic;
using System.Text;

namespace Hitta.Models.ViewModels
{
    public class HourVM
    {
        public List<Hour> Hours { get; set; }
      
        

        public HourVM(int id)
        {
            Hours = new Hour().GetHours(id);
        }
    }
}

