using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private void Save_clicked(object sender, EventArgs e)
        {
            Post post = new Post()
            {
                Experience = expEntry.Text
            };

            SQLiteConnection con = new SQLiteConnection(App.DbLocation);
            con.CreateTable<Post>();
            int row = con.Insert(post);
            con.Close();

            if(row == 1)
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