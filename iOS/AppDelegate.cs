using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Mvvm;
using XLabs.Platform.Services.Geolocation;
using XLabs.Forms;
using CoreLocation;

namespace maptest.iOS
{
    [Register ("AppDelegate")]
    public partial class AppDelegate : XFormsApplicationDelegate
    {
        //CLLocationManager manager = new CLLocationManager();

        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            this.SetIoc();
            global::Xamarin.Forms.Forms.Init ();
            Xamarin.FormsMaps.Init();
            //global::Xamarin.Forms.SetTitleBarVisibility(AndroidTitleBarVisibility.Never);

            // Code for starting up the Xamarin Test Cloud Agent
            #if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
            #endif

            LoadApplication (new App ());

            return base.FinishedLaunching (app, options);
        }

    private void SetIoc()
    {
        var resolverContainer = new SimpleContainer();

        var app = new XFormsAppiOS();
        app.Init(this);
        resolverContainer.Register<IXFormsApp>(app);

        var documents = app.AppDataDirectory;

        //resolverContainer.Register<IGeolocator, Geolocator>();
        //resolverContainer.Register<IEmailService, EmailService>();
        //resolverContainer.Register<IMediaPicker, MediaPicker>();
        //resolverContainer.Register<IDevice>( t => AppleDevice.CurrentDevice);
        Resolver.SetResolver(resolverContainer.GetResolver());

        DependencyService.Register<Geolocator> ();


        //resolverContainer.Register<IDevice>(t => AndroidDevice.CurrentDevice);
        //Resolver.SetResolver(resolverContainer.GetResolver());

            Console.WriteLine ("Here");
            Util util = new Util ();
            util.EnableLocationServices ();



            //manager.AuthorizationChanged += (sender, args) => {
               // Console.WriteLine ("Authorization changed to: {0}", args.Status);
            //};
            //if (UIDevice.CurrentDevice.CheckSystemVersion(8,0))
            //    manager.RequestWhenInUseAuthorization();

    }
    }

}

