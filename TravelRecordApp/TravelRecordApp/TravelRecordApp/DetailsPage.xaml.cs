using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;

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

        private void Update_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.DbLocation))
            {
                con.CreateTable<Post>();
                Exp.Experience = expEntry.Text;
                con.Update(Exp);
            }
            Navigation.PushAsync(new HomePage());
        }

        private void Delete_Clicked(object sender, EventArgs e)
        {
            using (SQLiteConnection con = new SQLiteConnection(App.DbLocation))
            {
                con.CreateTable<Post>();
                con.Delete(Exp);
            }
            Navigation.PushAsync(new HomePage());
        }
    }
}