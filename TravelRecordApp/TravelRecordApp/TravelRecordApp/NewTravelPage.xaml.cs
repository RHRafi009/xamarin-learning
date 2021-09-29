using Plugin.Geolocator;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.API;
using TravelRecordApp.Helpers;
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
            List<Venue> venues = await VenuesAPI.GetVenues(position.Latitude, position.Longitude);
            foreach (var venue in venues)
                venue.CategoriesName = venue.categories.FirstOrDefault()?.name;
            this.venueListView.ItemsSource = venues;
        }

        private async void Save_clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = venueListView.SelectedItem as Venue;
                Post post = new Post()
                {
                    Experience = expEntry.Text,
                    VenueId = selectedItem.id,
                    VenueName = selectedItem.name,
                    Address = selectedItem.location.address,
                    Latitude = selectedItem.location.lat,
                    Longitude = selectedItem.location.lng,
                    CategoryId = selectedItem.categories.FirstOrDefault()?.id,
                    CategoryName = selectedItem.categories.FirstOrDefault()?.name
                };
                /*int row = -1;*/
                /*using (SQLiteConnection con = new SQLiteConnection(App.DbLocation))
                {
                    con.CreateTable<Post>();
                    row = con.Insert(post);
                }*/

                var result = await Firestore.Write(post);

                if (result)
                {
                    await DisplayAlert("Success", "Experience succesfully inserted", "OK");
                    await Navigation.PushAsync(new HomePage());
                }
                else
                {
                    await DisplayAlert("Falied", "Error occured", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Falied", $"Error occured due to {ex.Message}", "OK");
            }
        }
    }
}