using System;

using Xamarin.Forms;

namespace maptest
{
    public class WallPage : ContentPage
    {
        public WallPage ()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            View toolbar = new Toolbar(this);

            Content = new StackLayout { 
                Children = {
                    toolbar,
                    new Label { Text = "Wall Page" }
                }
            };
        }
    }
}

