﻿using System;

using Xamarin.Forms;

namespace maptest
{
    public class SearchPage : ContentPage
    {
        public SearchPage ()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            View toolbar = new Toolbar(this);

            Content = new StackLayout { 
                Children = {
                        toolbar,
                        new Label { Text = "Search Page" }
                }
            };
        }
    }
}


