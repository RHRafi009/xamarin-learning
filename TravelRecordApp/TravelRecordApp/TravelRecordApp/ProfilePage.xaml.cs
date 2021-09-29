using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using TravelRecordApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            /*using (SQLiteConnection con = new SQLiteConnection(App.DbLocation))
            {*/
            var posts = await Firestore.ReadAll<Post>();
            /*var posts = con.Table<Post>().ToList();*/
            this.PostCountLabel.Text = posts.Count.ToString();

            var categories = posts.Select(p => p.CategoryName).Distinct().ToList();

            Dictionary<string, int> countByCategories = new Dictionary<string, int>();
            foreach(var cat in categories)
            {
                int count = posts.Where(p => p.CategoryName == cat).Count();
                string name = string.IsNullOrEmpty(cat) ? "Generic" : cat;
                countByCategories.Add(name, count);
            }
            this.CountByCategories.ItemsSource = countByCategories;
            /*}*/
        }
    }
}