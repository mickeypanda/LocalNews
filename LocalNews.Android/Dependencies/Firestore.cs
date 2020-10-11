using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Java.Util;
using LocalNews.Helpers;
using LocalNews.Model;
using Xamarin.Forms;

[assembly: Dependency(typeof(LocalNews.Droid.Dependencies.Firestore))]
namespace LocalNews.Droid.Dependencies
{
    public class Firestore : IFirestore
    {
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

        public Task<IList<Subscription>> ReadSubscriptions()
        {
            throw new NotImplementedException();
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
    }
}