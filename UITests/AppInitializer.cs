using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace maptest.UITests
{
    public class AppInitializer
    {

		// Test change for Git


        public static IApp StartApp (Platform platform)
        {
            if (platform == Platform.Android) {
                return ConfigureApp.Android.StartApp ();
            }

            return ConfigureApp.iOS.StartApp ();
        }
    }
}

