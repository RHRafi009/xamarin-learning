using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SQLite;
using TravelRecordApp.Models;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryPage : ContentPage
    {
        public HistoryPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection con = new SQLiteConnection(App.DbLocation))
            {
                con.CreateTable<Post>();
                List<Post> posts = con.Table<Post>().ToList();
                expListView.ItemsSource = posts;
            }
        }

        private void Exp_Selected(object sender, SelectedItemChangedEventArgs e)
        {
            Post selected = expListView.SelectedItem as Post;
            if(selected != null)
            {
                Navigation.PushAsync(new DetailsPage(selected));
            }
        }
    }
}