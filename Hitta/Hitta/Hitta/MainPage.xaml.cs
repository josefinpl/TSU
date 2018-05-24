using Hitta.Data;
using Hitta.Models;
using Hitta.Models.ViewModels;
using Hitta.Resources;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hitta
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<Language> Languages { get; }
        AuthorityVM avm;

        public MainPage()
        {
            InitializeComponent();

            var culture = CrossMultilingual.Current.DeviceCultureInfo;

            if (culture.Name.Contains("sv"))
            {
                if (culture.Name == "sv" ||culture.Name == "sv-SE" || culture.Name == "sv-FI" || culture.Name == "sv-AX")
                {
                    Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Sweden));
                }
            }
            else if (culture.Name.Contains("ar"))
            {
                if (culture.Name == "ar" || culture.Name == "ar-AE" || culture.Name == "ar-BH" || culture.Name == "ar-DZ" || culture.Name == "ar-EG"
               || culture.Name == "ar-IQ" || culture.Name == "ar-JO" || culture.Name == "ar-KW" || culture.Name == "ar-LB" || culture.Name == "ar-LY"
               || culture.Name == "ar-MA" || culture.Name == "ar-OM" || culture.Name == "ar-QA" || culture.Name == "ar-SA"
               || culture.Name == "ar-SY" || culture.Name == "ar-TN" || culture.Name == "ar-YE")
                {
                    Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Saudi));
                }
            }
            else if (culture.Name.Contains("en"))
            {
                if (culture.Name == "en" || culture.Name == "en-AU" || culture.Name == "en-BZ" || culture.Name == "en-CA" || culture.Name == "en-CB" ||
                culture.Name == "en-GB" || culture.Name == "en-IE" || culture.Name == "en-JM" || culture.Name == "en-NZ" ||
                culture.Name == "en-PH" || culture.Name == "en-TT" || culture.Name == "en-US" || culture.Name == "en-ZA" || culture.Name == "en-ZW")
                {
                    Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.UK));
                }
            }
            else if (culture.Name.Contains("fa"))
            {
                if (culture.Name == "fa" || culture.Name == "fa-IR")
                {
                    Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Iran));
                }
            }
            else if (culture.Name.Contains("sw"))
            {
               if (culture.Name == "sw" || culture.Name == "sw-KE")
                {
                    Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Kenya));
                }
            }
            else if (culture.Name.Contains("ti"))
            {
               if (culture.Name == "ti" || culture.Name == "ti-ER" || culture.Name == "ti-ET")
                {
                    Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Tigrinya));
                }
            }
            else
            {
                Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.UK));
            }

            avm = new AuthorityVM();

            //foreach (var authority in avm.Authorities)
            //{
            //    if (authority.Logo != null)
            //    {
            //        authority.Image = ImageSource.FromStream(() => new MemoryStream(authority.Logo));

            //    }
            //}

            //AuthorityView.ItemsSource = avm.Authorities;

            ImgLang.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.langicon));
            ImgAuthority.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Building));
            ImgHealth.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Health1));
            ImgSchool.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.School1));
            ImgStore.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Food));


            Languages = new ObservableCollection<Language>()
            {
                new Language { DisplayName =  "عربى - Arabic", ShortName = "ar" },
                new Language { DisplayName =  "English", ShortName = "en" },
                new Language { DisplayName =  "Fârsi - Persian", ShortName = "fa" },
                new Language { DisplayName =  "Kiswahili - Swahili", ShortName = "sw" },
                new Language { DisplayName = "Svenska - Swedish", ShortName = "sv" },
                new Language { DisplayName =  "ትግርኛ - Tigrinya", ShortName = "ti" }

            };

            BindingContext = this;

            PickerLanguages.SelectedIndexChanged += PickerLanguages_SelectedIndexChanged;

            //AuthorityView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            //{
            //    var item = (Authority)e.SelectedItem;

            //    var page = new AuthorityPage(item);

            //    Navigation.PushAsync(page);

            //};
        }

        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var frameSender = (Frame)sender;

            ((Frame)sender).IsEnabled = false;

            if(frameSender.StyleId == "Authority")
            {
                var page = new ListAuthorities(1);

                await Navigation.PushAsync(page);
            }
            else if (frameSender.StyleId == "Health")
            {
                var page = new ListAuthorities(2);

                await Navigation.PushAsync(page);
            }
            else if (frameSender.StyleId == "School")
            {
                var page = new ListAuthorities(3);

                await Navigation.PushAsync(page);
            }
            else if (frameSender.StyleId == "Store")
            {
                var page = new ListAuthorities(4);

                await Navigation.PushAsync(page);
            }

            ((Frame)sender).IsEnabled = true;
        }

            private void PickerLanguages_SelectedIndexChanged(object sender, EventArgs e)
        {
            var language = Languages[PickerLanguages.SelectedIndex];

            try
            {
                var culture = new CultureInfo(language.ShortName);
                AppResources.Culture = culture;
                CrossMultilingual.Current.CurrentCultureInfo = culture;
            }
            catch (Exception)
            {
            }

            if (language.ShortName == "sv")
            {
                Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Sweden));
            }
            else if (language.ShortName == "ar")
            {
                Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Saudi));
            }
            else if (language.ShortName == "en")
            {
                Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.UK));
            }
            else if (language.ShortName == "fa")
            {
                Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Iran));
            }
            else if (language.ShortName == "sw")
            {
                Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Kenya));
            }
            else if (language.ShortName == "ti")
            {
                Img.Source = ImageSource.FromStream(() => new MemoryStream(AppResources.Tigrinya));
            }
            else
            {
                Img.Source = null;
            }


            LabelLanguage.Text = AppResources.Language;
            LabelHello.Text = AppResources.Hello;
            LabelAuthority.Text = AppResources.Authority;
            LabelHealth.Text = AppResources.Health;
            LabelSchool.Text = AppResources.School;
            LabelStore.Text = AppResources.Store;
        }



    }
}

