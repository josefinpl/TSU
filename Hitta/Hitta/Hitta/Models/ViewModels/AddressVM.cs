using System;
using System.Collections.Generic;
using System.Text;

namespace Hitta.Models.ViewModels
{
    public class AddressVM
    {
        public Address Address { get; set; }

        
        public AddressVM(int Id)
        {

            Address = new Address().GetAddress(Id);

        }
    }
}
