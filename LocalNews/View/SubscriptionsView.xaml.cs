using LocalNews.Helpers;
using LocalNews.ViewModel;
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
        SubscriptionsViewModel vm;
        public SubscriptionsView()
        {
            InitializeComponent();
            vm = Resources["vm"] as SubscriptionsViewModel;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (!Auth.IsAuthenticated())
            {
                await Task.Delay(300);
                await App.Current.MainPage.Navigation.PushAsync(new LoginView());
            }
            else
            {
                //each time user returns back to this page will be able to see the subscription list, if he is logged in.
                vm.AssignSubscriptions();
            }
                
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new NewSubscriptionView());
        }
    }
}