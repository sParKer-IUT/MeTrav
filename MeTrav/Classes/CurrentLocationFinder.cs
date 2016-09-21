using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace MeTrav
{
    public class CurrentLocationFinder
    {
        public static Geoposition currGeoposition = null;
        public static async Task<bool> GetCurrentLocationAsync()
        {
            Geolocator geolocator = new Geolocator();
            
            try
            {
                currGeoposition = await geolocator.GetGeopositionAsync();
            }
            catch (Exception ex)
            {
                currGeoposition = null;
            }
            return !(currGeoposition==null);
        }
    }
}
