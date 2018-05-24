using System;
using System.Collections.Generic;
using System.Text;

namespace Hitta.Models.ViewModels
{
    public class ListAuthoritiesVM
    {
        public List<Authority> Authorities { get; set; }

        public ListAuthoritiesVM(int id)
        {
            Authorities = new Authority().GetTheAuthority(id);

        }

    }
}
