using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalNews.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void RegisterTapGesture_Tapped(object sender, EventArgs e)
        {
            loginStack.IsVisible = false;
            registerStack.IsVisible = true;
        }

        private void LoginTapGesture_Tapped(object sender, EventArgs e)
        {
            registerStack.IsVisible = false;
            loginStack.IsVisible = true;
        }
    }
}