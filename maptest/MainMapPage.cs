﻿
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
//using XLabs.Forms.Controls;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Geolocation;

using Toasts.Forms.Plugin.Abstractions;


using XLabs.Ioc;

namespace test_geolocation
{
    public class MainMapPage : ContentPage
    {

        bool DEBUG = true;

        private IGeolocator _geolocator;
        private IDevice _device;
        private CancellationTokenSource _cancelSource;
        private string _positionStatus = string.Empty;
        private string _positionLatitude = string.Empty;
        private string _positionLongitude = string.Empty;
        public string PositionStatus
        {
            get
            {
                return _positionStatus;
            }
            set
            {
                _positionStatus = value;
            }
        }
            
        public string PositionLatitude
        {
            get
            {
                return _positionLatitude;
            }
            set
            {
                _positionLatitude = value;
            }
        }

        public string PositionLongitude
        {
            get
            {
                return _positionLongitude;
            }
            set
            {
                _positionLongitude = value;
            }
        }
            

        Xamarin.Forms.Maps.Map _map;

        public MainMapPage()
		{

            var map = new Map(
                MapSpan.FromCenterAndRadius(
                    new Xamarin.Forms.Maps.Position(37,-122), Distance.FromMiles(0.3))) {
                IsShowingUser = true,
                HeightRequest = 1000,
                WidthRequest = 800,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            var stack = new StackLayout { Spacing = 0 };
            stack.BackgroundColor = Color.White;
            stack.Children.Add(map);
            Content = stack;

            GetPosition1();

            #if false
            try
            {
                _map = new Xamarin.Forms.Maps.Map()
                {
                    VerticalOptions = LayoutOptions.FillAndExpand
                };                     

				// Make map large - tried to use device size but didn't work
				_map.HeightRequest = 2500; //LoginPage.ScreenHeight;
				_map.WidthRequest = 800; //LoginPage.ScreenWidth;

				this.Content = _map; //_popupLayout;

				//  Get current position
                GetPosition();
            }
            catch (Exception exception)
            {
                // error
            }
            #endif
        }
            

        private async Task GetPosition1()
        {
            try
            {
                _cancelSource = new CancellationTokenSource();
                PositionStatus = string.Empty;
                PositionLatitude = string.Empty;
                PositionLongitude = string.Empty;
                IsBusy = true;

                if (DEBUG)
				{
					var notificator = DependencyService.Get<IToastNotificator>();
					bool tapped = await notificator.Notify(ToastNotificationType.Info, 
					"GetPosition", "Getting Position", TimeSpan.FromSeconds(2));
				}
				
					
				//  Get current position
                await Geolocator.GetPositionAsync(timeout: 10000, cancelToken: _cancelSource.Token, includeHeading: true)
                    .ContinueWith(t => UpdatePosition(t.Result));

            }
            catch (Exception e)
            {
                if (DEBUG) {
                    var notificator = DependencyService.Get<IToastNotificator> ();
                    var tapped = await notificator.Notify (ToastNotificationType.Info, 
                        "GeoLocator", "GetPositionAsyncContinueWithError: "+e.InnerException.ToString(), TimeSpan.FromSeconds (3)); 
                }
            }
        }

        void UpdatePosition(XLabs.Platform.Services.Geolocation.Position result)
            {


            //  Show on map - needs code made to work
  
            try
                            {
                var position = new Xamarin.Forms.Maps.Position(result.Latitude, result.Longitude);

                Device.BeginInvokeOnMainThread( async () =>
                                        {
                                            try
                                            {
                                                IsBusy = false;
//                                                if (result.IsFaulted)
//                                                {
//                                                    PositionStatus = ((GeolocationException)t.Exception.InnerException).Error.ToString();
//                                                }
//                                                else if (t.IsCanceled)
//                                                {
//                                                    PositionStatus = "Canceled";
//                                                }
                                                //else
                                                {
                                                    _map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1)));
                                                   var pin = new Pin
                                                    {
                                                        Type = PinType.Place,
                                                        Position = position,
                                                        Label = "Current Location"
                                                    };
                                                    _map.Pins.Add(pin);
                                                }
                                            }
                                            catch (Exception e)
                                            {
                                                if (DEBUG) {
                                                    var notificator = DependencyService.Get<IToastNotificator> ();
                                                    var tapped = await notificator.Notify (ToastNotificationType.Info, 
                                    "GeoLocator", "GetPositionAsyncContinueWithError: "+e.InnerException.ToString(), TimeSpan.FromSeconds (3)); 
                                                }
                                            }
                                        });
                                }
            catch (Exception e) {
            }
           
            }
            
        private IGeolocator Geolocator
        {
            get
            {
                if (_geolocator == null)
                {
                    //_geolocator = Resolver.Resolve<IGeolocator> ();
                    _geolocator = DependencyService.Get<IGeolocator>();
                    _geolocator.PositionError += OnListeningError;
                    _geolocator.PositionChanged += OnPositionChanged;
                    //_geolocator.StartListening(30000, 0, true);
                }
                return _geolocator;
            }
        }

        private async void OnListeningError(object sender, PositionErrorEventArgs e)
        {
            if (DEBUG) {
                var notificator = DependencyService.Get<IToastNotificator> ();
                var tapped = await notificator.Notify (ToastNotificationType.Info, 
                    "GeoLocator", "OnListeningError: "+e.Error.ToString(), TimeSpan.FromSeconds (3));
            }
        }

        private void OnPositionChanged(object sender, PositionEventArgs e)
        {
        }
        protected override bool OnBackButtonPressed()
        {
            return true;
        }

        protected override void OnDisappearing()
        {
            BindingContext = null;
            Content = null;
            base.OnDisappearing();
            GC.Collect();
        }

    }
}