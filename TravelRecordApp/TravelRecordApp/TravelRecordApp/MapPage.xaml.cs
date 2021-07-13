using Plugin.Geolocator;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TravelRecordApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        private bool HasLocationPermission { get; set; } = false;

        public MapPage()
        {
            InitializeComponent();
            GetPermission();
        }

        private async void GetPermission()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync<LocationWhenInUsePermission>();
                if (status != PermissionStatus.Granted)
                {
                    // currently using plugin.permission But I'm sure xamarin has some built in method to do this.
                    // need to research on this.
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.LocationWhenInUse))
                    {
                        await DisplayAlert("Need location to function", "Give us ur location", "Ok");
                    }
                    status = await CrossPermissions.Current.RequestPermissionAsync<LocationWhenInUsePermission>();
                }
                else if (status == PermissionStatus.Granted)
                {
                    HasLocationPermission = true;
                    locationsMap.IsShowingUser = true;
                    SetLocation();
                }else if(status == PermissionStatus.Denied)
                {
                    await DisplayAlert("Location denied", "what r u doing? u expect me to access ur location but won't give me permission? F*** u.", "Ok");
                }
            }
            catch(Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if(HasLocationPermission)
            {
                var curLocation = CrossGeolocator.Current;
                curLocation.PositionChanged += CurLocation_PositionChanged;
                await curLocation.StartListeningAsync(TimeSpan.Zero, 100);
            }

            SetLocation();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            _ = CrossGeolocator.Current.StopListeningAsync();
            CrossGeolocator.Current.PositionChanged -= CurLocation_PositionChanged;
        }

        private void CurLocation_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            MoveMap(e.Position);
        }

        private async void SetLocation()
        {
            if(HasLocationPermission)
            {
                var curLocation = CrossGeolocator.Current;
                var position = await curLocation.GetPositionAsync();
                MoveMap(position);
            }
        }

        private void MoveMap(Plugin.Geolocator.Abstractions.Position position)
        {
            var center = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
            var span = new MapSpan(center, 0.02, 0.02);
            locationsMap.MoveToRegion(span);
        }
    }
}