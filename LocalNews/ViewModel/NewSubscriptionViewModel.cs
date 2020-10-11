using LocalNews.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace LocalNews.ViewModel
{
    public class NewSubscriptionViewModel : BaseViewModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set 
            { 
                name = value;
                OnPropertyChanged("Name");
            }
        }
        private bool isActive;

        public bool IsActive
        {
            get { return isActive; }
            set 
            { 
                isActive = value;
                OnPropertyChanged("IsActive");
            }
        }

        public ICommand SaveSubscriptionCommand { get; set; }
        public NewSubscriptionViewModel()
        {
            SaveSubscriptionCommand = new Command(SaveSubscriptionImplementation, SaveSubscriptionCanExecute);
        }

        private void SaveSubscriptionImplementation(object obj)
        {
            var result = DatabaseHelper.InsertSubscription(new Model.Subscription
            {
                UserId = Auth.GetCurrentUserID(),
                Name = this.Name,
                SubscriptionDate = DateTime.Now,
                IsActive = this.IsActive
            });

            if (result)
                App.Current.MainPage.Navigation.PopAsync();
            else
                App.Current.MainPage.DisplayAlert("Error", "Some error occured while adding the subscription.", "Ok");
        }

        private bool SaveSubscriptionCanExecute(object arg)
        {
            return !string.IsNullOrEmpty(Name);
        }
    }
}
