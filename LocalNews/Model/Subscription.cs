using System;
using System.Collections.Generic;
using System.Text;

namespace LocalNews.Model
{
    public class Subscription
    {
        //This is the default Id of an document created for this Subscription in the Firestore Collection. Required during update.
        public string Id { get; set; }
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public bool IsActive { get; set; }
    }
}
