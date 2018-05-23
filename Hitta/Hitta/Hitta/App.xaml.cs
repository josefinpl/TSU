using Hitta.Resources;
using Plugin.Multilingual;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Hitta
{
	public partial class App : Application
	{
        //static CrossLocale? locale = null;

        public App ()
		{
			InitializeComponent();

            var culture = CrossMultilingual.Current.DeviceCultureInfo;
            AppResources.Culture = culture;

            var Main = new MainPage();
            NavigationPage.SetHasNavigationBar(Main, false);

            MainPage = new NavigationPage(Main);

        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
