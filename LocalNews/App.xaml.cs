﻿using LocalNews.View;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalNews
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //hello
            MainPage = new NavigationPage(new SubscriptionsView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
