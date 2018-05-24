using Hitta.Models;
using Hitta.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hitta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ListAuthorities : ContentPage
	{
        List<Authority> aList;
        Authority a;

        public ListAuthorities (int category)
		{
			InitializeComponent ();

            if(category == 1)
            {
                aList = new List<Authority>();
                a = new Authority();
                aList = a.GetTheAuthority(1);

                foreach (var authority in aList)
                {
                    if (authority.Logo != null)
                    {
                        authority.Image = ImageSource.FromStream(() => new MemoryStream(authority.Logo));

                    }
                }

                AuthorityView.ItemsSource = aList;

            }
            else if (category == 2)
            {
                aList = new List<Authority>();
                a = new Authority();
                aList = a.GetTheAuthority(2);

                foreach (var authority in aList)
                {
                    if (authority.Logo != null)
                    {
                        authority.Image = ImageSource.FromStream(() => new MemoryStream(authority.Logo));

                    }
                }

                AuthorityView.ItemsSource = aList;
            }
            else if (category == 3)
            {
                aList = new List<Authority>();
                a = new Authority();
                aList = a.GetTheAuthority(3);

                foreach (var authority in aList)
                {
                    if (authority.Logo != null)
                    {
                        authority.Image = ImageSource.FromStream(() => new MemoryStream(authority.Logo));

                    }
                }

                AuthorityView.ItemsSource = aList;
            }
            else if (category == 4)
            {
                aList = new List<Authority>();
                a = new Authority();
                aList = a.GetTheAuthority(4);

                foreach (var authority in aList)
                {
                    if (authority.Logo != null)
                    {
                        authority.Image = ImageSource.FromStream(() => new MemoryStream(authority.Logo));

                    }
                }

                AuthorityView.ItemsSource = aList;
            }


            AuthorityView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var item = (Authority)e.SelectedItem;

                var page = new AuthorityPage(item);

                Navigation.PushAsync(page);

            };
        }
    }
}