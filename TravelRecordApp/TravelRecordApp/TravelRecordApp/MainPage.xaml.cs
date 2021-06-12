using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecordApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Login_Clicked(object sender, EventArgs e)
        {
            bool isEmailEmpty = string.IsNullOrEmpty(EmailEntry.Text);
            bool isPassowordEmpty = string.IsNullOrEmpty(PasswordEntry.Text);

            if(isEmailEmpty || isPassowordEmpty)
            {

            }else
            {
                Navigation.PushAsync(new HomePage());
            }

        }
    }
}
