using LocalNews.Helpers;
using LocalNews.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

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

        public SubscriptionsViewModel()
        {
            Subscriptions = new ObservableCollection<Subscription>();
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
