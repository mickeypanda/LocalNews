using LocalNews.Model;
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
    public partial class SubscriptionDetailView : ContentPage
    {
        SubscriptionDetailsViewModel vm;
        public SubscriptionDetailView()
        {
            InitializeComponent();
            vm = Resources["vm"] as SubscriptionDetailsViewModel;
        }
        public SubscriptionDetailView(Subscription selectedSubscription)
        {
            InitializeComponent();
            vm = Resources["vm"] as SubscriptionDetailsViewModel;
            vm.Subscription = selectedSubscription;
        }
    }
}