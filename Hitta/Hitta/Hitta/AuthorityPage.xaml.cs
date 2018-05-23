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
using System.IO;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;

namespace Hitta
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorityPage : ContentPage
    {
        HourVM hvm;
        NumberVM nvm;
        AddressVM avm;
        Authority authority;

        public AuthorityPage(Authority auth)
        {
            InitializeComponent();

            hvm = new HourVM(auth.Id);
            nvm = new NumberVM(auth.Id);
            avm = new AddressVM(auth.Address_Id);

            AuthorityView.ItemsSource = hvm.Hours;
            AuthorityNumber.ItemsSource = nvm.Numbers;

            int i = hvm.Hours.Count;
            int heightRowList = 100;
            i = (hvm.Hours.Count * heightRowList);
            AuthorityView.HeightRequest = i;

            int n = nvm.Numbers.Count;
            int heightRowsList = 100;
            n = (nvm.Numbers.Count * heightRowsList);
            AuthorityNumber.HeightRequest = n;

            auth.Address1 = avm.Address.Address1;
            auth.City1 = avm.Address.City;
            auth.Zipcode1 = avm.Address.Zipcode;
            auth.MapAddress = auth.Address1 + ", " + auth.Zipcode1.ToString() + " " + auth.City1;

            auth.HelpText = "Hej! Jag försöker hitta till " + auth.Name + ". Skulle du kunna peka mig i rätt riktning? Adressen är " + auth.MapAddress + ".";

            BindingContext = auth;

        }


        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;
            
            ((Image)sender).IsEnabled = false;

            if(imageSender.StyleId == "Voice")
            {
                bool sv = false;
                CrossLocale country;

                var id = ((Image)sender).BindingContext.ToString();
                authority = new Authority();
                authority = authority.GetAuthority(id);

                var locales = await CrossTextToSpeech.Current.GetInstalledLanguages();

                foreach (var item in locales)
                {
                    if(item.ToString() == "sv-SE")
                    {
                        sv = true;
                        country = item;
                    }
                }

                if (sv)
                {
                    await CrossTextToSpeech.Current.Speak(authority.Description, country);
                }
                else
                {
                    await CrossTextToSpeech.Current.Speak(authority.Description);
                }

            }
            else if (imageSender.StyleId == "Help")
            {
                bool sv = false;
                CrossLocale country;

                var helptxt = ((Image)sender).BindingContext.ToString();

                var locales = await CrossTextToSpeech.Current.GetInstalledLanguages();

                foreach (var item in locales)
                {
                    if (item.ToString() == "sv-SE")
                    {
                        sv = true;
                        country = item;
                    }
                }

                if (sv)
                {
                    await CrossTextToSpeech.Current.Speak(helptxt, country);
                }
                else
                {
                    await CrossTextToSpeech.Current.Speak(helptxt);
                }

            }
            else if (imageSender.StyleId == "Maps")
            {
                try
                {
                    var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                    if (status != PermissionStatus.Granted)
                    {
                        status = await Utils.CheckPermissions(Permission.Location);
                    }

                    if (status == PermissionStatus.Granted)
                    {
                        var maps = ((Image)sender).BindingContext.ToString();
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

                    }
                    else if (status != PermissionStatus.Unknown)
                    {
                        await DisplayAlert("Location Denied", "Can not continue, try again.", "OK");
                    }
                }
                catch (Exception)
                {

                }
            }

            ((Image)sender).IsEnabled = true;

        }

    }
}