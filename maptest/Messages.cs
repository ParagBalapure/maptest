using System;

using Xamarin.Forms;

namespace maptest
{
    public class MessagesPage : ContentPage
    {
        public MessagesPage ()
        {
            NavigationPage.SetHasNavigationBar(this, false);
			View toolbar = new Toolbar(this);

			var _header = new Label
			{
				Text = "   INBOX   ",
				FontSize = 20,
				FontFamily = "Helvetica Neue,Helvetica,Arial",
				XAlign = TextAlignment.Start,
				YAlign = TextAlignment.Center,
				WidthRequest = 2000,
				//HeightRequest = 40,
				HorizontalOptions = LayoutOptions.StartAndExpand,
				BackgroundColor = Color.FromHex ("29B6F6"), 
				TextColor = Color.White,
			};

			var _newButton = new Button
			{
				Text = "New",
				VerticalOptions = LayoutOptions.FillAndExpand,
				TextColor = Color.White,
				//HeightRequest = 100
			};

			var newButton = new ContentView
			{
				Padding = new Thickness (0,0,0,0),
				Content = _newButton
			};



			StackLayout header = new StackLayout 
			{
				Orientation = StackOrientation.Horizontal,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Padding = new Thickness (0,-7,30,0),
				WidthRequest = 2000,
				BackgroundColor = Color.FromHex ("29B6F6"),
				Children = {_header, newButton}
			};

			var searchMessages = new Entry()
			{
				Keyboard = Keyboard.Chat,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Black,
				HeightRequest = 30,
				//Padding = new Thickness (5,0,0,0),
				Placeholder = "Search Messages"
			};




            Content = new StackLayout { 
				VerticalOptions = LayoutOptions.Start,
                Children = {
                    toolbar,
					header,
					searchMessages,
                    new Label { Text = "Messages" }
                }
            };
        }
    }
}
