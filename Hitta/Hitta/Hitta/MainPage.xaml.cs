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

            avm = new AuthorityVM();

            foreach (var authority in avm.Authorities)
            {
                authority.Image = ImageSource.FromStream(() => new MemoryStream(authority.Logo));
            }

            AuthorityView.ItemsSource = avm.Authorities;


            Languages = new ObservableCollection<Language>()
            {
                new Language { DisplayName =  "عربى - Arabic", ShortName = "ar" },
                new Language { DisplayName =  "English", ShortName = "en" },
                new Language { DisplayName =  "Fârsi - Persian", ShortName = "fa" },
                new Language { DisplayName =  "Kiswahili - Swahili", ShortName = "sw" },
                new Language { DisplayName =  "Svenska - Swedish", ShortName = "sv" },
                new Language { DisplayName =  "ትግርኛ - Tigrinya", ShortName = "ti" }

            };

            BindingContext = this;
            PickerLanguages.SelectedIndexChanged += PickerLanguages_SelectedIndexChanged;

            AuthorityView.ItemSelected += (object sender, SelectedItemChangedEventArgs e) =>
            {
                var item = (Authority)e.SelectedItem;

                var page = new AuthorityPage(item);
                NavigationPage.SetHasNavigationBar(page, false);

                Navigation.PushAsync(page);

            };
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

            LabelLanguage.Text = AppResources.Language;
            LabelHello.Text = AppResources.Hello;
            LabelAuthority.Text = AppResources.Authority;
        }

    }
}

