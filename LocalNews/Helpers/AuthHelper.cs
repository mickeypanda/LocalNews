using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LocalNews.Helpers
{
    public interface IAuth
    {
        Task<bool> RegisterUser(string name, string email, string password);
        Task<bool> AuthenticateUser(string email, string password);
        bool IsAuthenticated();
        string GetCurrentUserID();
    }

    public class Auth
    {
        private static IAuth auth;
        public static async Task<bool> RegisterUser(string name, string email, string password)
        {
            return await auth.RegisterUser(name, email, password);
        }
        public static async Task<bool> AuthenticateUser(string email, string password)
        {
            return await auth.AuthenticateUser(email, password);
        }
        public static bool IsAuthenticated()
        {
            return auth.IsAuthenticated();
        }
        public static string GetCurrentUserID()
        {
            return auth.GetCurrentUserID();
        }
    }
}
