using Hitta.Models;
using Hitta.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Hitta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorityPage : ContentPage
    {
        HourVM hvm;
        NumberVM nvm;
        AddressVM avm;
        MapVM mvm;
        Authority authority;

        public AuthorityPage(Authority auth)
        {
            InitializeComponent();

            auth.MapAddress = auth.Address1 + ", " + auth.Zipcode1.ToString() + " " + auth.City1;

            hvm = new HourVM(auth.Id);
            nvm = new NumberVM(auth.Id);
            avm = new AddressVM(auth.Address_Id);
            mvm = new MapVM(auth.MapAddress, auth);

            AuthorityView.ItemsSource = hvm.Hours;
            AuthorityNumber.ItemsSource = nvm.Numbers;

            auth.Address1 = avm.Address.Address1;
            auth.City1 = avm.Address.City;
            auth.Zipcode1 = avm.Address.Zipcode;

            auth.MapVM = mvm;

            BindingContext = auth;

        }

        bool busy;

        async void btnMap(object sender, EventArgs e)
        {
            if (busy)
                return;

            busy = true;
            //((Button)sender).IsEnabled = false;

            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    status = await Utils.CheckPermissions(Permission.Location);

                    await DisplayAlert("Results", status.ToString(), "OK");
                }

                if (status == PermissionStatus.Granted)
                {
                    var maps = ((Button)sender).BindingContext.ToString();
                    authority = new Authority();                 
                    authority = authority.GetAuthority(maps);

                    avm = new AddressVM(authority.Address_Id);
                    authority.Address1 = avm.Address.Address1;
                    authority.City1 = avm.Address.City;
                    authority.Zipcode1 = avm.Address.Zipcode;
                    authority.MapAddress = authority.Address1 + ", " + authority.Zipcode1.ToString() + " " + authority.City1;
                    var place = await CrossGeolocator.Current.GetPositionsForAddressAsync(authority.MapAddress);

                    foreach (var p in place)
                    {
                        var page = new MapPage(p.Latitude, p.Longitude, authority);
                        await Navigation.PushAsync(page);
                    }
                   
                   // var results = await CrossGeolocator.Current.GetPositionAsync();
                    
                    
                }
                else if (status != PermissionStatus.Unknown)
                {
                    await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                }
            }
            catch (Exception)
            {

            }

            //((Button)sender).IsEnabled = true;


            busy = false;


        }


    }
}