using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using TravelRecordApp.Helpers;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        private Post Exp { get; set; }

        public DetailsPage(Post exp)
        {
            InitializeComponent();
            Exp = exp;
            expEntry.Text = Exp.Experience;
        }

        private async void Update_Clicked(object sender, EventArgs e)
        {
            /*using (SQLiteConnection con = new SQLiteConnection(App.DbLocation))
            {
                con.CreateTable<Post>();
                Exp.Experience = expEntry.Text;
                con.Update(Exp);
            }*/

            Exp.Experience = expEntry.Text;

            var result = await Firestore.Update(Exp);
            if (result)
                await Navigation.PushAsync(new HomePage());
            else
                await DisplayAlert("Error", "An error has occured while updating the record.", "ok");
        }

        private async void Delete_Clicked(object sender, EventArgs e)
        {
            /*using (SQLiteConnection con = new SQLiteConnection(App.DbLocation))
            {
                con.CreateTable<Post>();
                con.Delete(Exp);
            }*/
            var result = await Firestore.Delete(Exp);
            if (result)
                await Navigation.PushAsync(new HomePage());
            else
                await DisplayAlert("Error", "An error has occured while deleting the record.", "ok");
        }
    }
}