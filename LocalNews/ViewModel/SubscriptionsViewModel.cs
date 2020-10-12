using LocalNews.Helpers;
using LocalNews.Model;
using LocalNews.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocalNews.ViewModel
{
    public class SubscriptionsViewModel:BaseViewModel
    {
        private ObservableCollection<Subscription> subscriptions;

        public ObservableCollection<Subscription> Subscriptions
        {
            get { return subscriptions; }
            set 
            { 
                subscriptions = value;
                OnPropertyChanged("Subscriptions");
            }
        }

        private Subscription selectedSubscription;

        public Subscription SelectedSubscription
        {
            get { return selectedSubscription; }
            set 
            { 
                selectedSubscription = value;
                OnPropertyChanged("SelectedSubscription");
                if (SelectedSubscription != null)
                    App.Current.MainPage.Navigation.PushAsync(new SubscriptionDetailView(SelectedSubscription));
            }
        }

        public ICommand SignoutCommand { get; set; }
        public SubscriptionsViewModel()
        {
            Subscriptions = new ObservableCollection<Subscription>();
            SignoutCommand = new Command(SignoutImplementation);
        }

        private async void SignoutImplementation(object parameter)
        {
            var result = await Auth.SignoutUser();
            if (result)
                await App.Current.MainPage.Navigation.PushAsync(new LoginView());
            else
                await App.Current.MainPage.DisplayAlert("Error", "Error in signout process.", "Ok");
        }

        public async void AssignSubscriptions()
        {
            var subscriptions = await DatabaseHelper.ReadSubscriptions();
            if (subscriptions != null)
            {
                Subscriptions.Clear();
                foreach (Subscription s in subscriptions)
                    Subscriptions.Add(s);
            }
        }
    }
}
