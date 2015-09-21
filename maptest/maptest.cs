﻿using System;

using Xamarin.Forms;
using test_geolocation;

namespace maptest
{
    public class App : Application
    {
        public App ()
        {
            // The root page of your application


            MainPage = new MainMapPage ();

//            MainPage = new ContentPage {
//                Content = new StackLayout {
//                    VerticalOptions = LayoutOptions.Center,
//                    Children = {
//                        new Label {
//                            XAlign = TextAlignment.Center,
//                            Text = "Welcome to Xamarin Forms!"
//                        }
//                    }
//                }
//            };
        }

        protected override void OnStart ()
        {
            // Handle when your app starts
        }

        protected override void OnSleep ()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume ()
        {
            // Handle when your app resumes
        }
    }
}

