using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.API;
using TravelRecordApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewTravelPage : ContentPage
    {
        public NewTravelPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var curLocation = CrossGeolocator.Current;
            var position = await curLocation.GetPositionAsync();
            List<Venues> venues = await VenuesAPI.GetVenues(position.Latitude, position.Longitude);
        }

        private void Save_clicked(object sender, EventArgs e)
        {
            Post post = new Post()
            {
                Experience = expEntry.Text
            };
            int row = -1;
            using (SQLiteConnection con = new SQLiteConnection(App.DbLocation))
            {
                con.CreateTable<Post>();
                row = con.Insert(post);
            }

            if (row == 1)
            {
                DisplayAlert("Success", "Experience succesfully inserted", "OK");
            }
            else
            {
                DisplayAlert("Falied", "Error occured", "OK");
            }
        }
    }
}