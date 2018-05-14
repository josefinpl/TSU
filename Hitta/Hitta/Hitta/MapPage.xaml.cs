using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Hitta
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPage : ContentPage
	{
		//public MapPage ()
		//{
		//	InitializeComponent ();
		//}

        public MapPage(double lat, double lon)
        {
            var map = new Map(
                MapSpan.FromCenterAndRadius(
                        new Position(lat, lon), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var pin = new Pin()
            {
                Position = new Position(lat, lon),
                Label = "Some Pin!"
            };
            map.Pins.Add(pin);

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
        }
    }
}