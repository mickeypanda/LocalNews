using System;
using System.Collections.Generic;
using System.Text;

namespace LocalNews.Model
{
    public class Subscription
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public bool IsActive { get; set; }
    }
}
