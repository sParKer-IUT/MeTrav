using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace MeTrav
{
    public class Location
    {
        public string name;
        public double latitude;
        public double longitude;
        public LocationType type;
        public BasicGeoposition basicGeoPosition;
        public int routeIndex;

        public Location()
        {

        }

        public Location(string n, double la, double lo)
        {
            name = n;
            latitude = la;
            longitude = lo;
            type = LocationType.Other;
            basicGeoPosition = new BasicGeoposition();
            basicGeoPosition.Latitude = la;
            basicGeoPosition.Longitude = lo;
        }

        public Location(string n, double la, double lo, LocationType t)
        {
            name = n;
            latitude = la;
            longitude = lo;
            type = t;
            basicGeoPosition = new BasicGeoposition();
            basicGeoPosition.Latitude = la;
            basicGeoPosition.Longitude = lo;
        }
    }

    public class SelectedLocations
    {
        public Location source { get; set; }
        public Location destination { get; set; }

        public SelectedLocations()
        {

        }

        public SelectedLocations(Location s,Location d)
        {
            source = s;
            destination = d;
        }
    }

    public class Bus
    {
        public string name;
        public List<Location> route;

        public Bus(string n, List<Location>r)
        {
            name = n;
            route = r;
        }
    }
}
