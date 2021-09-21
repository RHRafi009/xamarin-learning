using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelRecordApp.Helpers;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void Login_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            bool isPassowordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if(isEmailEmpty || isPassowordEmpty)
            {

            }else
            {
                var result = await Auth.LoginUser(EmailEntry.Text, PasswordEntry.Text);
                if(result)
                    await Navigation.PushAsync(new HomePage());
            }

        }
    }
}
