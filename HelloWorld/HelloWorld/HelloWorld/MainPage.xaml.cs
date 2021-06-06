using System;
using Xamarin.Forms;

namespace HelloWorld
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void SayHello_Clicked(object sender, EventArgs e)
        {
            string personName = nameEntry.Text;
            string outputRes = $"Hello {personName}!!";
            outputLabel.Text = outputRes;
            outputFrame.IsVisible = true;
        }
    }
}
