using LocalNews.Helpers;
using LocalNews.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocalNews.ViewModel
{
    public class SubscriptionDetailsViewModel : BaseViewModel
    {
        private Subscription subscription;

        public Subscription Subscription
        {
            get { return subscription; }
            set 
            {
                subscription = value;
                Name = subscription.Name;
                IsActive = subscription.IsActive;
                OnPropertyChanged("Subscription");
            }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                Subscription.Name = name;
                OnPropertyChanged("Name");
                OnPropertyChanged("Subscription");

            }
        }

        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set 
            { 
                isActive = value;
                Subscription.IsActive = isActive;
                OnPropertyChanged("IsActive");
                OnPropertyChanged("Subscription");
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public SubscriptionDetailsViewModel()
        {
            UpdateCommand = new Command(Update, UpdateCanExecute);
            DeleteCommand = new Command(Delete);
        }

        private async void Delete(object obj)
        {
            var result = await DatabaseHelper.DeleteSubscription(Subscription);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "Some error occured while deleting the data", "Ok");
        }

        private async void Update(object parameter)
        {
            var result = await DatabaseHelper.UpdateSubscription(Subscription);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
            else
                await App.Current.MainPage.DisplayAlert("Error", "Some error occured while updating the data", "Ok");
        }

        private bool UpdateCanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}
