using Hitta.Models;
using Hitta.Models.ViewModels;
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
        HourVM hvm;
        NumberVM nvm;

		public AuthorityPage (Authority auth)
		{
			InitializeComponent ();

            hvm = new HourVM(auth.Id);
            nvm = new NumberVM(auth.Id);

            AuthorityView.ItemsSource = hvm.Hours;
            AuthorityNumber.ItemsSource = nvm.Numbers;

            this.BindingContext = auth;

        }
	}
}