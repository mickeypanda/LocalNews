using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LocalNews.Helpers
{
    public interface IAuth
    {
        Task<bool> RegisterUser(string name, string email, string password);
        Task<bool> AuthenticateUser(string email, string password);
        Task<bool> SignoutUser();
        bool IsAuthenticated();
        string GetCurrentUserID();
    }

    public class Auth 
    {
        private static IAuth auth = DependencyService.Get<IAuth>();
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            try
            {
                return await auth.RegisterUser(name, email, password);
            }catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }
            
        }
        public static async Task<bool> AuthenticateUser(string email, string password)
        {
            try
            {
                return await auth.AuthenticateUser(email, password);
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
                return false;
            }
        }
        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }
        public static string GetCurrentUserID()
        {
            return auth.GetCurrentUserID();
        }


        public static async Task<bool> SignoutUser()
        {
            return await auth.SignoutUser();
        }

    }
}
