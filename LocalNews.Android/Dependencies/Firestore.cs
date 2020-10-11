using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Firebase.Firestore;
using Java.Util;
using LocalNews.Helpers;
using LocalNews.Model;

[assembly: Xamarin.Forms.Dependency(typeof(LocalNews.Droid.Dependencies.Firestore))]
namespace LocalNews.Droid.Dependencies
{
    /// <summary>
    /// this class needs to be a JAVA object. IOnCompleteListener needs to be implemented to call the oncompletion listener while
    /// reading the firebase documents.
    /// </summary>
    public class Firestore : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        List<Subscription> subscriptions;
        //hasSubscriptionsRead will be used for checking if the subscriptions are read or not. We need to wait for some while until the OnComplete method is executed/
        bool hasSubscriptionsRead = false;
        public Firestore()
        {
            subscriptions = new List<Subscription>();
        }
        public Task<bool> DeleteSubscription(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        public bool InsertSubscription(Subscription subscription)
        {
            try
            {

                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
                var subscriptionDocument = new Dictionary<string, Java.Lang.Object>
                {
                    {"auther", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                    {"name", subscription.Name },
                    {"isActive", subscription.IsActive },
                    {"subscriptionDate", dateTimeToNativeDate(subscription.SubscriptionDate)} // because this needs Java.util.Date object
                };

                collection.Add(subscriptionDocument);
                return true;
            }catch(Exception ex)
            {
                return false;
            }
        }

        //method will be executed after completing the document retrieving. Like an event.
        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var document = (QuerySnapshot)task.Result;
                subscriptions.Clear();
                foreach(var doc in document.Documents)
                {
                    Subscription subscription = new Subscription
                    {
                        IsActive = (bool)doc.Get("isActive"),
                        Name = doc.Get("name").ToString(),
                        UserId=doc.Get("auther").ToString(),
                        SubscriptionDate=nativeDateToDateTime(doc.Get("subscriptionDate") as Date)
                    };
                    subscriptions.Add(subscription);
                }

            }
            else
            {
                subscriptions.Clear();
            }
            //Make this true to know that OnComplete methid has completed reading the subscriptions.
            hasSubscriptionsRead = true;
        }

        public async Task<IList<Subscription>> ReadSubscriptions()
        {
            hasSubscriptionsRead = false;
            //get the collection
            var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("subscriptions");
            //query for the specific user resource
            var query = collection.WhereEqualTo("auther", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
            //use the listener to get the data on completion of the request
            query.Get().AddOnCompleteListener(this);

            //wait for some while before sending the list to check if the hasSubscriptionRead is true.
            for(int i = 0; i < 10; i++)
            {
                await System.Threading.Tasks.Task.Delay(100);
                if (hasSubscriptionsRead)
                    break;
            }
            return subscriptions;
        }

        public Task<bool> UpdateSubscription(Subscription subscription)
        {
            throw new NotImplementedException();
        }

        //This method is used for converting DateTime type to Java.Util.Date type
        private Date dateTimeToNativeDate(DateTime dateTime)
        {
            var dateTimeUTCAsMilliseconds = (long)dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
            return new Date(dateTimeUTCAsMilliseconds);
        }

        //This method is used for converting Java.Util.Date type to System.DateTime type
        private DateTime nativeDateToDateTime(Date date)
        {
            DateTime reference = System.TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1, 0, 0, 0));
            return reference.AddMilliseconds(date.Time);
        }
    }
}