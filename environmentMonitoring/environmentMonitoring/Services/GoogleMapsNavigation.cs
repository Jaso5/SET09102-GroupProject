using System;

namespace environmentMonitoring.Services;

public class GoogleMapsNavigation: INavigateToSensor
{
    /*! NavigateToSensor command navigates opens either google maps app, or a browser
     *  Uses the sensor longitude and latitude, to plot a route from the current location
     *  throws an error message if theres an issue attempting to open the browser, or google maps app
     */
    public async Task NavigateToSensor(double? lon, double? lat) {
        try {
            Uri uri = new Uri("https://www.google.com/maps/dir/?api=1&destination="+lat+"+"+lon+"&travelmode=driving");
            BrowserLaunchOptions options = new BrowserLaunchOptions()
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Show,
            };

            await Browser.Default.OpenAsync(uri, options);

        } catch (Exception) {
            throw new Exception("Error when trying to open google maps for sensor navigation");
        }
    }

}
