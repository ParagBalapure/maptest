using System;

using Xamarin.Forms;

namespace maptest
{
    public class MessagesPage : ContentPage
    {
        public MessagesPage ()
        {
            NavigationPage.SetHasNavigationBar(this, false);

            View toolbar = new Toolbar();

            Content = new StackLayout { 
                Children = {
                    toolbar,
                    new Label { Text = "Messages Page" }
                }
            };
        }
    }
}
