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
            if (personName == null || personName == string.Empty)
                personName = "Stranger";
            string outputRes = $"Hello {personName} !!";
            outputLabel.Text = outputRes;
            outputFrame.IsVisible = true;
            resetButton.IsVisible = true;
        }

        private void Reset_clicked(object sender, EventArgs e)
        {
            nameEntry.Text = string.Empty;
            outputLabel.Text = string.Empty;
            outputFrame.IsVisible = false;
            resetButton.IsVisible = false;
        }
    }
}
