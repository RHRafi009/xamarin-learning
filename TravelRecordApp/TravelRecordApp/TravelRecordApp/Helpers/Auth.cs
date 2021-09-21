using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TravelRecordApp.Helpers
{
    public class Auth
    {
        private static IAuth auth = DependencyService.Get<IAuth>();

        public static async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                return await auth.RegisterUser(email, password);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Substring(0, ex.Message.IndexOf("."));
                await App.Current.MainPage.DisplayAlert("Error", msg, "Ok");
                return false;
            }
        }

        public static async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                return await auth.LoginUser(email, password);
            }
            catch (Exception ex)
            {
                string msg = ex.Message.Substring(0, ex.Message.IndexOf("."));
                await App.Current.MainPage.DisplayAlert("Error", msg, "Ok");
                return msg.Contains("There is no user record corresponding to this identifier") 
                    ? await RegisterUser(email, password) : false;
            }

        }

        public static bool IsAuthenticated()
        {
            return true;
        }

        public static string GetCurrentUserId()
        {
            return "";
        }
    }
}
