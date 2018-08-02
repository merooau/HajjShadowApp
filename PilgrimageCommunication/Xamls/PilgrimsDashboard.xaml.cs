using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Plugin.MediaManager;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace PilgrimageCommunication.Xamls
{
    public partial class PilgrimsDashboard : ContentPage
    {
        private Xamarin.Forms.Maps.Position _position;
        public PilgrimsDashboard()
        {
            InitializeComponent();

            var map = new Map(
              MapSpan.FromCenterAndRadius(
                    new Xamarin.Forms.Maps.Position(37, -122), Distance.FromMiles(0.3)))
            {
                IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };



            if (IsLocationAvailable())
            {
                GetPosition();

                currentLocation.Text = "Zone A, Mena Towers 1";

                map.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromKilometers(0.1)));

            }

            map.MapType = MapType.Satellite;
            var stack = this.MapStackLayout;
            stack.Children.Add(map);
            //Content = stack;

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                _position = new Xamarin.Forms.Maps.Position(21.419628, 39.8756252);

                currentLocation.Text = "Zone B, AlJamarat bridge";

                var pin = new Pin
                {
                    Type = PinType.Place,
                    Position = _position,
                    Label = "custom pin",
                    Address = "custom detail info"
                };
                map.Pins.Add(pin);

                map.MoveToRegion(MapSpan.FromCenterAndRadius(_position, Distance.FromKilometers(0.1)));


                return false; // True = Repeat again, False = Stop the timer
            });

        }

        public bool IsLocationAvailable()
        {
            if (!CrossGeolocator.IsSupported)
                return false;

            return CrossGeolocator.Current.IsGeolocationAvailable;
        }
        public async void GetPosition()
        {
            Plugin.Geolocator.Abstractions.Position position = null;
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;

                position = await locator.GetLastKnownLocationAsync();



                if (position != null)
                {
                    _position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                    //got a cahched position, so let's use it.
                    return;
                }

                if (!locator.IsGeolocationAvailable || !locator.IsGeolocationEnabled)
                {
                    //not available or enabled
                    return;
                }

                position = await locator.GetPositionAsync(TimeSpan.FromSeconds(20), null, true);



            }
            catch (Exception ex)
            {
                throw ex;
                //Display error as we have timed out or can't get location.
            }
            _position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            if (position == null)
                return;

        }

        async void OnPlayButtonClicked(object sender, EventArgs e)
        {
            try
            {
                await CrossMediaManager.Current.Play("http://ksarent.com/test.mp3"); 
            }
            catch(Exception ex){

                await DisplayAlert("info", ex.Message, "OK");
                
            }


        }



    }
}
