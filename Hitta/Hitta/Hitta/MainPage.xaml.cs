using Hitta.Data;
using Hitta.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Hitta
{
	public partial class MainPage : ContentPage
	{
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
                items.Add(new Authority() { Name = number.Name });
            }

            AuthorityView.ItemsSource = items;

        }
	}
}
