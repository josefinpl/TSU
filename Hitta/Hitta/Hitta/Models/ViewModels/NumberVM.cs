using System;
using System.Collections.Generic;
using System.Text;

namespace Hitta.Models.ViewModels
{
    class NumberVM
    {
        public List<Number> Numbers { get; set; }


        public NumberVM(int id)
        {
            Numbers = new Number().GetNumbers(id);
        }
    }
}
