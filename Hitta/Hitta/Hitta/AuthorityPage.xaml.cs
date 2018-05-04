using Hitta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hitta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AuthorityPage : ContentPage
	{
		public AuthorityPage (Authority auth)
		{
			InitializeComponent ();

            this.BindingContext = auth;

        }
	}
}