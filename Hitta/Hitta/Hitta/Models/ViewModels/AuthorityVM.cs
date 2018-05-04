using System;
using System.Collections.Generic;
using System.Text;

namespace Hitta.Models.ViewModels
{
    public class AuthorityVM
    {
        public List<Authority> Authorities { get; set; }

        public AuthorityVM()
        {
            Authorities = new Authority().GetAuthorities();
        }
    }
}
