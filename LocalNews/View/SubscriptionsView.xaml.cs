using LocalNews.Helpers;
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
    public partial class SubscriptionsView : ContentPage
    {
        public SubscriptionsView()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!Auth.IsAuthenticated())
            {
                await Task.Delay(300);
                await App.Current.MainPage.Navigation.PushAsync(new LoginView());
            }
                
        }
    }
}