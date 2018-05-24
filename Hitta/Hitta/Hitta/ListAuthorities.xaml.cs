using Hitta.Models;
using Hitta.Models.ViewModels;
using Hitta.Resources;
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
        ListAuthoritiesVM aList;

        public ListAuthorities (int category)
		{
			InitializeComponent ();

            aList = new ListAuthoritiesVM(category);

            foreach (var authority in aList.Authorities)
            {
                if (authority.Logo != null)
                {
                    authority.Image = ImageSource.FromStream(() => new MemoryStream(authority.Logo));
                }
            }

            if (category == 1)
            {
                LabelAuthority.Text = AppResources.Authority;

            }
            else if (category == 2)
            {
                LabelAuthority.Text = AppResources.Health;

            }
            else if (category == 3)
            {
                LabelAuthority.Text = AppResources.School;

            }
            else if (category == 4)
            {
                LabelAuthority.Text = AppResources.Store;
            }

            AuthorityView.ItemsSource = aList.Authorities;

            int heightRowsList = 100;
            var n = (aList.Authorities.Count * heightRowsList);
            AuthorityView.HeightRequest = n;


            AuthorityView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var item = (Authority)e.SelectedItem;

                var page = new AuthorityPage(item);

                Navigation.PushAsync(page);

            };
        }
    }
}