using Hitta.Data;
using Hitta.Models;
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
        List<Authority> authorities;
        SqlOperations sqlOp = new SqlOperations();

        public MainPage()
        {
            InitializeComponent();

            ObservableCollection<Authority> items = new ObservableCollection<Authority>();

            authorities = new List<Authority>();
            authorities = sqlOp.GetAuthorities();

            foreach (var number in authorities)
            {
                number.Image = ImageSource.FromStream(() => new MemoryStream(number.Logo));
                items.Add(new Authority() { Name = number.Name, Image = number.Image });

            }

            AuthorityView.ItemsSource = items;


            Languages = new ObservableCollection<Language>()
            {
                new Language { DisplayName =  "عربى - Arabic", ShortName = "ar" },
                new Language { DisplayName =  "Deutsche - German", ShortName = "de" },
                new Language { DisplayName =  "English", ShortName = "en" },
                new Language { DisplayName =  "Fârsi - Persian", ShortName = "fa" },
                new Language { DisplayName =  "Svenska - Swedish", ShortName = "sv" },
                new Language { DisplayName =  "Kiswahili - Swahili", ShortName = "sw" },
                new Language { DisplayName =  "ትግርኛ - Tigrinya", ShortName = "ti" }

            };

            BindingContext = this;
            PickerLanguages.SelectedIndexChanged += PickerLanguages_SelectedIndexChanged;
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
        }
    }
}

