using Microsoft.Maui.Controls;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Devices;
using System;

namespace Open_Source_Maps___Maui___Eng_Maged_Ali
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGetLocationButtonClicked(object sender, EventArgs e)
        {
            // Check if the platform supports the permission request
            if (DeviceInfo.Platform == DevicePlatform.Android && DeviceInfo.Version >= new Version(21, 0))
            {
                var status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted)
                {
                    // Inform the user if the permission is denied
                    await DisplayAlert("Permission Denied", "Location permission is required for this feature.", "OK");
                    return;
                }
            }
            else
            {
                // Handle cases where permissions are not supported (for iOS or other platforms)
                await DisplayAlert("Error", "Location permissions are not supported on this platform.", "OK");
                return;
            }

            // If permission is granted, get the current location
            try
            {
                var location = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Medium));

                if (location != null)
                {
                    // Use the location (e.g., display or use coordinates)
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}");
                    await DisplayAlert("Location", $"Latitude: {location.Latitude}, Longitude: {location.Longitude}", "OK");
                }
            }
            catch (FeatureNotSupportedException)
            {
                // Handle feature not supported exception
                await DisplayAlert("Error", "Geolocation is not supported on this device.", "OK");
            }
            catch (PermissionException)
            {
                // Handle permission exception (if not granted)
                await DisplayAlert("Permission Denied", "Location permission was denied.", "OK");
            }
            catch (Exception)
            {
                // Handle any other errors
                await DisplayAlert("Error", "Unable to get location.", "OK");
            }
        }
    }
}
