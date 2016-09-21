using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Phone.UI.Input;
using Windows.Services.Maps;
using Windows.Storage.Streams;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace MeTrav
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Map : Page
    {
        SelectedLocations selectedLocations = null;

        bool sourceIsBusStop = false;
        bool destIsBusStop = false;

        List<List<Location>> allAlternateRoutes = null;
        int currentRouteIndex = -1;
        int numberOfRoutes = 0;
        List<bool> isReverseRoute;

        string routeDetails = "No details available";

        double busFarePerKM = 2;
        double rckshwFarePerKM = 12;
        double cngFarePerKM = 20;

        List<string> buses = null;

        public Map()
        {
            this.InitializeComponent();
            HardwareButtons.BackPressed += HardwareButtons_BackPressed;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            selectedLocations = e.Parameter as SelectedLocations;
            DetermineRouteAsync(/*selectedLocations*/);
        }

        void HardwareButtons_BackPressed(object sender, BackPressedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            if (rootFrame != null && rootFrame.CanGoBack)
            {
                rootFrame.GoBack();
                e.Handled = true;
            }
        }

        private async void DetermineRouteAsync(/*SelectedLocations selectedLocations*/)
        {
            sourceIsBusStop = CheckBusStop(selectedLocations.source);
            destIsBusStop = CheckBusStop(selectedLocations.destination);

            //both bus stop
            if (sourceIsBusStop && destIsBusStop)
            {
                allAlternateRoutes = BothBusStop(/*selectedLocations*/);

                numberOfRoutes = allAlternateRoutes.Count;
                currentRouteIndex = 0;

                ShowMap();

                //SHOW FUNCTION FOR BOTHSTOP
                //ShowBothStopRouteAsync(route);
            }
            //only source bus stop
            else if (sourceIsBusStop && !destIsBusStop)
            {
                allAlternateRoutes =  await SourceBusStopAsync(/*selectedLocations*/);
                //Debug.WriteLine(isReverseRoute.ToString());

                numberOfRoutes = allAlternateRoutes.Count;
                currentRouteIndex = 0;

                ShowMap();

                //Debug.WriteLine("here");
                //SHOW FUNCTION FOR SOURCESTOP
                //ShowSourceStopRouteAsync(allAlternateRoutes[0]/*, selectedLocations*/);
            }
            //only dest bus stop
            else if (!sourceIsBusStop && destIsBusStop)
            {
                allAlternateRoutes = await DestBusStopAsync(/*selectedLocations*/);

                numberOfRoutes = allAlternateRoutes.Count;
                currentRouteIndex = 0;

                ShowMap();

                ////SHOW FUNCTION FOR DESTSTOP
                //ShowDestStopRouteAsync(allAlternateRoutes[0]/*, selectedLocations*/);
            }
            //none are bus stop
            else if (!sourceIsBusStop && !destIsBusStop)
            {
                allAlternateRoutes = await NoneBusStopAsync(/*selectedLocations*/);

                numberOfRoutes = allAlternateRoutes.Count;
                currentRouteIndex = 0;

                ShowMap();

                //SHOW FUNCTION FOR NOSTOP
                //Debug.WriteLine("Showing, total paths: " + allAlternateRoutes.Count + " isreverse: " + isReverseRoute.Count);
                //if(numberOfRoutes > 0)
                //{
                //    ShowNoneStopRouteAsync(allAlternateRoutes[0]/*, selectedLocations*/);
                //}
                //else
                //{
                //    ShowDirectRoute();
                //}
                
            }
            
        }

        private bool CheckBusStop(Location loc)
        {
            Location sc = null;
            bool isBusStop = false;
            foreach (List<Location> route in MeTravUtility.AllRoutes)
            {
                foreach (Location l in route)
                {
                    if (l.name.ToString().Equals(loc.name.ToString()))
                    {
                        isBusStop = true;
                        break;
                    }
                }
                if (isBusStop)
                {
                    break;
                }
            }
            return isBusStop;
        }

        private List<List<Location>> BothBusStop(/*SelectedLocations selectedLocations*/)
        {
            buses = new List<string>();

            List<List<Location>> multipleRoutes = new List<List<Location>>();
            List<Location> selectedRoute = null;

            isReverseRoute = new List<bool>();
            bool sourceFound = false;
            bool destFound = false;
            int sourceIndex = -1, destIndex = -1;
            int rin = 0;
            foreach (List<Location> route in MeTravUtility.AllRoutes)
            {
                sourceFound = false;
                destFound = false;
                foreach (Location l in route)
                {
                    if (l.name.ToString().Equals(selectedLocations.source.name.ToString()))
                    {
                        sourceFound = true;
                        sourceIndex = route.IndexOf(l);
                    }
                    if (l.name.ToString().Equals(selectedLocations.destination.name.ToString()))
                    {
                        destFound = true;
                        destIndex = route.IndexOf(l);
                    }

                    if (sourceFound && destFound)
                    {
                        selectedRoute = route;
                        break;
                    }

                }
                if (sourceFound && destFound)
                {
                    break;
                }

                rin++;
            }

            if ((sourceFound && destFound))
            {
                buses.Add(MeTravUtility.AllBuses[rin]);
                multipleRoutes.Add(MakePath(selectedRoute, sourceIndex, destIndex));
            }
            return multipleRoutes;
        }

        private async Task<List<List<Location>>> SourceBusStopAsync(/*SelectedLocations selectedLocations*/)
        {
            buses = new List<string>();

            List<List<Location>> multipleRoutes = new List<List<Location>>();
            isReverseRoute = new List<bool>();
            //double thresholdDistance = 1.5;
            int rin = 0;
            foreach (List<Location> route in MeTravUtility.AllRoutes)
            {
                if (RouteContains(route, selectedLocations.source))
                {
                    double min = 1000;
                    Location bs = new Location();
                    foreach (Location busStop in route)
                    {
                        double dist =  CheckDistanceFromCoordinate(busStop, selectedLocations.destination);
                        if(dist < min)
                        {
                            min = dist;
                            bs = busStop;
                        }
                    }
                    int start = -1, end = -1;
                    for (int i = 0; i < route.Count; i++)
                    {
                        if (route[i].name.ToString().Equals(selectedLocations.source.name.ToString())) start = i;
                        if (route[i].name.ToString().Equals(bs.name.ToString())) end = i;
                    }
                    //Debug.WriteLine("start,end: " + start.ToString() + " " + end.ToString());

                    buses.Add(MeTravUtility.AllBuses[rin]);
                    multipleRoutes.Add(MakePath(route, start, end));
                }
                rin++;
            }

            return multipleRoutes;
        }

        private async Task<List<List<Location>>> DestBusStopAsync(/*SelectedLocations selectedLocations*/)
        {
            buses = new List<string>();
            List<List<Location>> multipleRoutes = new List<List<Location>>();
            isReverseRoute = new List<bool>();
            //double thresholdDistance = 1.5;
            int rin = 0;
            foreach (List<Location> route in MeTravUtility.AllRoutes)
            {
                if (RouteContains(route, selectedLocations.destination))
                {
                    double min = 1000;
                    Location bs = new Location();
                    foreach (Location busStop in route)
                    {
                        double dist = CheckDistanceFromCoordinate(busStop, selectedLocations.source);
                        if (dist < min)
                        {
                            min = dist;
                            bs = busStop;
                        }
                    }
                    int start = -1, end = -1;
                    for (int i = 0; i < route.Count; i++)
                    {
                        if (route[i].name.ToString().Equals(bs.name.ToString()))
                        {
                            start = i;
                        }
                        if (route[i].name.ToString().Equals(selectedLocations.destination.name.ToString()))
                        {
                            end = i;
                        }
                    }
                    buses.Add(MeTravUtility.AllBuses[rin]);
                    multipleRoutes.Add(MakePath(route, start, end));
                }
                rin++;
            }

            return multipleRoutes;
        }

        private async Task<List<List<Location>>> NoneBusStopAsync(/*SelectedLocations selectedLocations*/)
        {
            buses = new List<string>();

            List<List<Location>> multipleRoutes = new List<List<Location>>();
            isReverseRoute = new List<bool>();
            double thresholdDistance = 1.5;
            bool invalidStop = false;
            int rin = 0;
            foreach (List<Location> route in MeTravUtility.AllRoutes)
            {
                invalidStop = false;
                double min = 1000;
                Location bs = new Location();
                foreach (Location busStop in route)
                {
                    double dist = CheckDistanceFromCoordinate(busStop, selectedLocations.source);//await CheckDistance(busStop, selectedLocations.source);
                    if(dist<min)
                    {
                        min = dist;
                        bs = busStop;
                    }
                    //Debug.WriteLine("NBS1 " + dist.ToString());
                }

                int start = -1;
                for (int i = 0; i < route.Count; i++)
                {
                    if (route[i].name.ToString().Equals(bs.name.ToString()))
                    {
                        start = i;
                        break;
                    }
                }

                //Debug.WriteLine("NBS2 " + start);

                min = 1000;
                int end = -1;
                for (int i = 0; i < route.Count; i++)
                {
                    double tempDist = CheckDistanceFromCoordinate(route[i], selectedLocations.destination);//await CheckDistance(route[i], selectedLocations.destination);
                    //Debug.WriteLine("tempDist: " + tempDist + ", route[i]: " + route[i] + ", selectedLocations.destination: " + selectedLocations.destination);
                    if (tempDist < min)
                    {
                        min = tempDist;
                        end = i;
                    }
                }

                if (end == start)
                {
                    invalidStop = true;
                }

                if (!invalidStop)
                {
                    buses.Add(MeTravUtility.AllBuses[rin]);
                    multipleRoutes.Add(MakePath(route, start, end));
                }
                rin++;
            }

            //Debug.WriteLine("no. of paths" + multipleRoutes.Count.ToString());
            //if(multipleRoutes.Count == 0)
            return multipleRoutes;
        }

        private bool RouteContains(List<Location> route, Location loc)
        {
            foreach (Location l in route)
            {
                if (l.name.ToString().Equals(loc.name.ToString()))
                {
                    return true;
                }
            }
            return false;
        }

        private async void ShowBothStopRouteAsync(List<Location> route)
        {
            routeDetails = "Type: Public Transport\n";

            mapControl1.Children.Clear();

            // Pushpin for Source
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = route[0].latitude;
            startLocation.Longitude = route[0].longitude;
            Geopoint startPoint = new Geopoint(startLocation);

            AddPushpin(startPoint, route[0].name, "ms-appx:///Assets/bluePin50.png");

            // Pushpin for Destination
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = route[route.Count - 1].latitude;
            endLocation.Longitude = route[route.Count - 1].longitude;
            Geopoint endPoint = new Geopoint(endLocation);

            AddPushpin(endPoint, route[route.Count - 1].name, "ms-appx:///Assets/redPin50.png");



            for (int i = 1; i < route.Count - 1; i++)
            {
                AddPushpin(new Geopoint(route[i].basicGeoPosition), route[i].name, "ms-appx:///Assets/greenPin50.png");
            }

            double len1 = 0;
            int fare1 = 0;

            MapRouteFinderResult routeResult = await GetBusRouteAsync(route);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                len1 = routeResult.Route.LengthInMeters / 1000;
                routeDetails += "Distance: " + len1.ToString() + " km\n";

                mapControl1.Routes.Clear();

                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.ForestGreen;
                viewOfRoute.OutlineColor = Colors.ForestGreen;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);
                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Bow);

            }

            fare1 = (int)Math.Ceiling((len1 * busFarePerKM));

            routeDetails += "Estimated Fare: " + fare1.ToString() + " Tk\n";
            routeDetails += "Available buses: " + buses[currentRouteIndex] + "\n";
        }

        private async void ShowSourceStopRouteAsync(List<Location> route/*, SelectedLocations selectedLocations*/)
        {
            routeDetails = "Type: Public Transport\n";

            string sourcePin = "ms-appx:///Assets/bluePin50.png";
            string middlePin = "ms-appx:///Assets/greenPin50.png";
            string destPin = "ms-appx:///Assets/redPin50.png";

            string pin;

            mapControl1.Children.Clear();

            // Pushpin for first stopage of a route 
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = route[0].latitude;
            startLocation.Longitude = route[0].longitude;
            Geopoint startPoint = new Geopoint(startLocation);

            if (!isReverseRoute[currentRouteIndex]) pin = sourcePin;
            else pin = middlePin;
            AddPushpin(startPoint, route[0].name, pin);

            // Pushpin for last stopage of a route
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = route[route.Count - 1].latitude;
            endLocation.Longitude = route[route.Count - 1].longitude;
            Geopoint endPoint = new Geopoint(endLocation);

            if (isReverseRoute[currentRouteIndex]) pin = sourcePin;
            else pin = middlePin;
            AddPushpin(endPoint, route[route.Count - 1].name, pin);

            //The real destination
            BasicGeoposition destBgp = new BasicGeoposition();
            destBgp.Latitude = selectedLocations.destination.latitude;
            destBgp.Longitude = selectedLocations.destination.longitude;
            Geopoint destPoint = new Geopoint(destBgp);

            AddPushpin(destPoint, selectedLocations.destination.name, destPin);

            //The last bus stop
            Geopoint lastStopPoint;

            if (!isReverseRoute[currentRouteIndex]) lastStopPoint = endPoint;
            else lastStopPoint = startPoint;

            //Pushpin for in between stopages of a route
            for (int i = 1; i < route.Count - 1; i++)
            {
                AddPushpin(new Geopoint(route[i].basicGeoPosition), route[i].name, middlePin);
            }

            double len1 = 0, len2 = 0;
            int fare1 = 0, fare2 = 0;

            mapControl1.Routes.Clear();

            MapRouteFinderResult routeResult = await GetBusRouteAsync(route);
            
            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                len1 = routeResult.Route.LengthInMeters/1000;
                routeDetails += "Distance: " + len1.ToString() + " + "; 

                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.ForestGreen;
                viewOfRoute.OutlineColor = Colors.ForestGreen;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);
                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Linear);
            }

            routeResult = await MapRouteFinder.GetWalkingRouteAsync(lastStopPoint, destPoint);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                len2 = routeResult.Route.LengthInMeters / 1000;
                routeDetails += len2.ToString() + " = " + (len1 + len2).ToString() + " km\n";

                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Red;
                viewOfRoute.OutlineColor = Colors.Red;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);
                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Linear);
            }

            fare1 = (int)Math.Ceiling((len1 * busFarePerKM));
            fare2 = (int)Math.Ceiling((len2 * rckshwFarePerKM));

            routeDetails += "Estimated Fare: " + fare1.ToString() + " + " + fare2.ToString() + " = " + (fare1 + fare2).ToString() + " Tk\n";
            routeDetails += "Available buses: " + buses[currentRouteIndex] + "\n";
        }

        private async void ShowDestStopRouteAsync(List<Location> route/*, SelectedLocations selectedLocations*/)
        {
            routeDetails = "Type: Public Transport\n";

            string sourcePin = "ms-appx:///Assets/bluePin50.png";
            string middlePin = "ms-appx:///Assets/greenPin50.png";
            string destPin = "ms-appx:///Assets/redPin50.png";

            string pin;

            mapControl1.Children.Clear();

            // Pushpin for Source
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = route[0].latitude;
            startLocation.Longitude = route[0].longitude;
            Geopoint startPoint = new Geopoint(startLocation);

            if (isReverseRoute[currentRouteIndex]) pin = destPin;
            else pin = middlePin;
            AddPushpin(startPoint, route[0].name, pin);

            // Pushpin for Destination
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = route[route.Count - 1].latitude;
            endLocation.Longitude = route[route.Count - 1].longitude;
            Geopoint endPoint = new Geopoint(endLocation);

            if (!isReverseRoute[currentRouteIndex]) pin = destPin;
            else pin = middlePin;
            AddPushpin(endPoint, route[route.Count - 1].name, pin);

            //The real source
            BasicGeoposition sourceBgp = new BasicGeoposition();
            sourceBgp.Latitude = selectedLocations.source.latitude;
            sourceBgp.Longitude = selectedLocations.source.longitude;
            Geopoint sourcePoint = new Geopoint(sourceBgp);

            AddPushpin(sourcePoint, selectedLocations.source.name, sourcePin);

            //The first bus stop
            Geopoint firstStopPoint;

            if (isReverseRoute[currentRouteIndex]) firstStopPoint = endPoint;
            else firstStopPoint = startPoint;

            for (int i = 1; i < route.Count - 1; i++)
            {
                AddPushpin(new Geopoint(route[i].basicGeoPosition), route[i].name, middlePin);
            }

            double len1 = 0, len2 = 0;
            int fare1 = 0, fare2 = 0;

            mapControl1.Routes.Clear();

            MapRouteFinderResult routeResult = await GetBusRouteAsync(route);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                len2 = routeResult.Route.LengthInMeters / 1000;
                

                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.ForestGreen;
                viewOfRoute.OutlineColor = Colors.ForestGreen;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);
                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Linear);
            }

            routeResult = await MapRouteFinder.GetWalkingRouteAsync(sourcePoint, firstStopPoint);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                len1 = routeResult.Route.LengthInMeters / 1000;
                

                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Red;
                viewOfRoute.OutlineColor = Colors.Red;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);
                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Linear);
            }

            routeDetails += "Distance: " + len1.ToString() + " + ";
            routeDetails += len2.ToString() + " = " + (len1 + len2).ToString() + " km\n";

            fare1 = (int)Math.Ceiling((len1 * rckshwFarePerKM));
            fare2 = (int)Math.Ceiling((len2 * busFarePerKM));

            routeDetails += "Estimated Fare: " + fare1.ToString() + " + " + fare2.ToString() + " = " + (fare1 + fare2).ToString() + " Tk\n";
            routeDetails += "Available buses: " + buses[currentRouteIndex] + "\n";
        }

        private async void ShowNoneStopRouteAsync(List<Location> route/*, SelectedLocations selectedLocations*/)
        {
            routeDetails = "Type: Public Transport\n";

            string sourcePin = "ms-appx:///Assets/bluePin50.png";
            string middlePin = "ms-appx:///Assets/greenPin50.png";
            string destPin = "ms-appx:///Assets/redPin50.png";

            string pin;

            mapControl1.Children.Clear();


            // Pushpin for Source
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = route[0].latitude;
            startLocation.Longitude = route[0].longitude;
            Geopoint startPoint = new Geopoint(startLocation);
            
            pin = middlePin;
            AddPushpin(startPoint, route[0].name, pin);

            // Pushpin for Destination
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = route[route.Count - 1].latitude;
            endLocation.Longitude = route[route.Count - 1].longitude;
            Geopoint endPoint = new Geopoint(endLocation);

            pin = middlePin;
            AddPushpin(endPoint, route[route.Count - 1].name, pin);

            //The real source
            BasicGeoposition sourceBgp = new BasicGeoposition();
            sourceBgp.Latitude = selectedLocations.source.latitude;
            sourceBgp.Longitude = selectedLocations.source.longitude;
            Geopoint sourcePoint = new Geopoint(sourceBgp);

            AddPushpin(sourcePoint, selectedLocations.source.name, sourcePin);

            //The real destination
            BasicGeoposition destBgp = new BasicGeoposition();
            destBgp.Latitude = selectedLocations.destination.latitude;
            destBgp.Longitude = selectedLocations.destination.longitude;
            Geopoint destPoint = new Geopoint(destBgp);

            AddPushpin(destPoint, selectedLocations.destination.name, destPin);

            //The first bus stop
            Geopoint firstStopPoint, lastStopPoint;

            if (isReverseRoute[currentRouteIndex])
            {
                firstStopPoint = endPoint;
                lastStopPoint = startPoint;
            }
            else
            {
                firstStopPoint = startPoint;
                lastStopPoint = endPoint;
            }

            for (int i = 1; i < route.Count - 1; i++)
            {
                AddPushpin(new Geopoint(route[i].basicGeoPosition), route[i].name, middlePin);
            }

            double len1 = 0, len2 = 0, len3 = 0;
            int fare1 = 0, fare2 = 0, fare3 = 0;

            mapControl1.Routes.Clear();

            MapRouteFinderResult routeResult = await GetBusRouteAsync(route);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                len2 = routeResult.Route.LengthInMeters / 1000;

                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.ForestGreen;
                viewOfRoute.OutlineColor = Colors.ForestGreen;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Linear);
            }

            routeResult = await MapRouteFinder.GetWalkingRouteAsync(sourcePoint, firstStopPoint);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                len1 = routeResult.Route.LengthInMeters / 1000;

                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Red;
                viewOfRoute.OutlineColor = Colors.Red;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Linear);
            }

            routeResult = await MapRouteFinder.GetWalkingRouteAsync(destPoint, lastStopPoint);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                len3 = routeResult.Route.LengthInMeters / 1000;
                
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Red;
                viewOfRoute.OutlineColor = Colors.Red;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Linear);
            }

            routeDetails += "Distance: " + len1.ToString() + " + " + len2.ToString() + " + ";
            routeDetails += len2.ToString() + " = " + (len1 + len2 + len3).ToString() + " km\n";

            fare1 = (int)Math.Ceiling((len1 * rckshwFarePerKM));
            fare2 = (int)Math.Ceiling((len2 * busFarePerKM));
            fare3 = (int)Math.Ceiling((len3 * rckshwFarePerKM));

            routeDetails += "Estimated Fare: " + fare1.ToString() + " + " + fare2.ToString() + " + ";
            routeDetails += fare3.ToString() + " = " + (fare1 + fare2 + fare3).ToString() + " Tk\n";

            routeDetails += "Available buses: " + buses[currentRouteIndex] + "\n";
        }

        private async void ShowDirectRoute(string type)
        {
            string sourcePin = "ms-appx:///Assets/bluePin50.png";
            string destPin = "ms-appx:///Assets/redPin50.png";

            mapControl1.Children.Clear();

            BasicGeoposition sourceBgp = new BasicGeoposition();
            sourceBgp.Latitude = selectedLocations.source.latitude;
            sourceBgp.Longitude = selectedLocations.source.longitude;
            Geopoint startPoint = new Geopoint(sourceBgp);

            AddPushpin(startPoint, selectedLocations.source.name, sourcePin);

            BasicGeoposition destBgp = new BasicGeoposition();
            destBgp.Latitude = selectedLocations.destination.latitude;
            destBgp.Longitude = selectedLocations.destination.longitude;
            Geopoint destPoint = new Geopoint(destBgp);

            AddPushpin(destPoint, selectedLocations.destination.name, destPin);

            MapRouteFinderResult routeResult = null;
            mapControl1.Routes.Clear();

            Color routeColor =new Color();

            if (type == "d")
            {
                routeDetails = "Type: Car/CNG/Taxi\n";

                routeColor = Colors.Blue;
                routeResult = await MapRouteFinder.GetDrivingRouteAsync(startPoint, destPoint);
                double len1 = routeResult.Route.LengthInMeters / 1000;
                int fare1 = (int)Math.Ceiling((len1 * cngFarePerKM));

                routeDetails += "Distance: " + len1.ToString() + " km\n";
                
                routeDetails += "Estimated Fare: " + fare1.ToString() + " Tk\n";
            }
            else if (type == "w")
            {
                routeDetails = "Type: Walking Route\n";

                routeColor = Colors.Gray;
                routeResult = await MapRouteFinder.GetWalkingRouteAsync(startPoint, destPoint);

                double len1 = routeResult.Route.LengthInMeters / 1000;
                routeDetails += "Distance: " + len1.ToString() + " km\n";
            }


            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = routeColor;
                viewOfRoute.OutlineColor = routeColor;
                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                mapControl1.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await mapControl1.TrySetViewBoundsAsync(
                  routeResult.Route.BoundingBox,
                  null,
                  MapAnimationKind.Linear);
            }
        }

        private async Task<MapRouteFinderResult> GetBusRouteAsync(List<Location> route)
        {
            List<Geopoint> gp = new List<Geopoint>();

            // Start at Source
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = route[0].latitude;
            startLocation.Longitude = route[0].longitude;
            Geopoint startPoint = new Geopoint(startLocation);

            gp.Add(startPoint);

            //AddPushpin(startPoint, route[0].name, "ms-appx:///Assets/bluePin50.png");

            // End at Destination
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = route[route.Count - 1].latitude;
            endLocation.Longitude = route[route.Count - 1].longitude;
            Geopoint endPoint = new Geopoint(endLocation);

            //AddPushpin(endPoint, route[route.Count - 1].name, "ms-appx:///Assets/redPin50.png");

            

            for (int i = 1; i < route.Count - 1; i++)
            {
                gp.Add(new Geopoint(route[i].basicGeoPosition));

                //AddPushpin(new Geopoint(route[i].basicGeoPosition), route[i].name, "ms-appx:///Assets/greenPin50.png");
            }

            gp.Add(endPoint);

            // Get the route between the points.
            MapRouteFinderResult routeResult =
            await MapRouteFinder.GetDrivingRouteFromWaypointsAsync(gp, MapRouteOptimization.Distance);
            
            return routeResult;

        }

        private async Task<MapRouteFinderResult> GetCarRouteAsync(Location loc1, Location loc2)
        {
            // Start at Source
            BasicGeoposition startLocation = new BasicGeoposition();
            startLocation.Latitude = loc1.latitude;
            startLocation.Longitude = loc2.longitude;
            Geopoint point1 = new Geopoint(startLocation);

            //AddPushpin(point1, loc1.name, "ms-appx:///Assets/bluePin50.png");

            // End at Destination
            BasicGeoposition endLocation = new BasicGeoposition();
            endLocation.Latitude = loc2.latitude;
            endLocation.Longitude = loc2.longitude;
            Geopoint point2 = new Geopoint(endLocation);

            //AddPushpin(point2, loc2.name, "ms-appx:///Assets/redPin50.png");

            // Get the route between the points.
            MapRouteFinderResult routeResult =
            await MapRouteFinder.GetDrivingRouteAsync(
              point1,
              point2);


            return routeResult;
        }

        //private async Task<double> CheckDistance(Location loc1, Location loc2)
        //{
        //    MapRouteFinderResult routeResult = await GetCarRouteAsync(loc1, loc2);

        //    if (routeResult.Status == MapRouteFinderStatus.Success)
        //    {
        //        return routeResult.Route.LengthInMeters / 1000;
        //    }
        //    return -1;
        //}

        private double CheckDistanceFromCoordinate(Location loc1, Location loc2)
        {
            double lon1, lat1, lon2, lat2;
            lon1 = loc1.longitude;
            lon2 = loc2.longitude;
            lat1 = loc1.latitude;
            lat2 = loc2.latitude;
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            dist = dist * 1.609344;
            
            return dist;
        }

        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }
        
        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }

        private async void ShowCurrentLocationAsync(/*SelectedLocations selectedLocations*/)
        {
            BasicGeoposition bgp = new BasicGeoposition();
            bgp.Latitude = selectedLocations.source.latitude;
            bgp.Longitude = selectedLocations.source.longitude;
            Geopoint gp = new Geopoint(bgp);
            mapControl1.Center = gp;
            mapControl1.ZoomLevel = 15;

            MapIcon currLocationPin = new MapIcon();
            currLocationPin.Image = RandomAccessStreamReference.CreateFromUri(
              new Uri("ms-appx:///Assets/greenPin50.png"));
            currLocationPin.NormalizedAnchorPoint = new Point(0.25, 0.9);
            //currLocationPin.Location = geoposition.Coordinate.Point;
            currLocationPin.Location = gp;
            currLocationPin.Title = "You are here";
            mapControl1.MapElements.Add(currLocationPin);
            
        }

        private void AddPushpin(Geopoint gpoint, string text, string imageSource)
        {
            var pin = new Grid()
            {
                //Width = 50,
                //Height = 50,
                //Margin = new Windows.UI.Xaml.Thickness(-12)
            };
            pin.Children.Add(new Image()
            {
                Source = new BitmapImage(new Uri(imageSource)),
                Width = 50,
                Height = 50,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            });

            pin.Children.Add(new TextBlock()
            {
                Text = text,
                FontSize = 12,
                Foreground = new SolidColorBrush(Colors.DodgerBlue),
                HorizontalAlignment = Windows.UI.Xaml.HorizontalAlignment.Center,
                VerticalAlignment = Windows.UI.Xaml.VerticalAlignment.Center,
                Margin = new Thickness(0,0,0,60)
            });

            
            MapControl.SetLocation(pin, gpoint);
            MapControl.SetNormalizedAnchorPoint(pin, new Point(0.5, 0.5));
            mapControl1.Children.Add(pin);
        }

        private List<Location> MakePath(List<Location> route, int start, int end)
        {
            List<Location> path = new List<Location>();

            bool irr = false;

            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
                irr = true;
            }

            isReverseRoute.Add(irr);

            for (int i = start; i <= end; i++)
            {
                path.Add(route[i]);
            }
            return path;
        }

        private void ShowMap()
        {
            //Debug.WriteLine("index: " + currentRouteIndex + " number: " + numberOfRoutes);
            if(sourceIsBusStop && destIsBusStop)
            {
                //Debug.WriteLine("BBS");

                if (currentRouteIndex == numberOfRoutes + 2)
                    currentRouteIndex = 0;

                if (currentRouteIndex == -1)
                    currentRouteIndex = numberOfRoutes + 1;

                if (currentRouteIndex == numberOfRoutes)
                {
                    ShowDirectRoute("d");
                }
                else if (currentRouteIndex == numberOfRoutes + 1)
                {
                    ShowDirectRoute("w");
                }
                else if (currentRouteIndex >= 0 && currentRouteIndex < numberOfRoutes) ShowBothStopRouteAsync(allAlternateRoutes[currentRouteIndex]);
            }

            if (sourceIsBusStop && !destIsBusStop)
            {
                //Debug.WriteLine("SBS");

                if (currentRouteIndex == numberOfRoutes + 2)
                    currentRouteIndex = 0;

                if (currentRouteIndex == -1)
                    currentRouteIndex = numberOfRoutes + 1;

                if (currentRouteIndex == numberOfRoutes)
                {
                    ShowDirectRoute("d");
                }
                else if (currentRouteIndex == numberOfRoutes + 1)
                {
                    ShowDirectRoute("w");
                }
                else if (currentRouteIndex >= 0 && currentRouteIndex < numberOfRoutes) ShowSourceStopRouteAsync(allAlternateRoutes[currentRouteIndex]);
            }

            if (!sourceIsBusStop && destIsBusStop)
            {
                //Debug.WriteLine("DBS");

                if (currentRouteIndex == numberOfRoutes + 2)
                    currentRouteIndex = 0;

                if (currentRouteIndex == -1)
                    currentRouteIndex = numberOfRoutes + 1;

                if (currentRouteIndex == numberOfRoutes)
                {
                    ShowDirectRoute("d");
                }
                else if (currentRouteIndex == numberOfRoutes + 1)
                {
                    ShowDirectRoute("w");
                }
                else if (currentRouteIndex >= 0 && currentRouteIndex < numberOfRoutes) ShowDestStopRouteAsync(allAlternateRoutes[currentRouteIndex]);
            }

            if (!sourceIsBusStop && !destIsBusStop)
            {
                //Debug.WriteLine("NBS");

                if (currentRouteIndex == numberOfRoutes + 2)
                    currentRouteIndex = 0;

                if (currentRouteIndex == -1)
                    currentRouteIndex = numberOfRoutes + 1;

                if (currentRouteIndex == numberOfRoutes)
                {
                    ShowDirectRoute("d");
                }
                else if (currentRouteIndex == numberOfRoutes + 1)
                {
                    ShowDirectRoute("w");
                }
                else if (currentRouteIndex >= 0 && currentRouteIndex < numberOfRoutes) ShowNoneStopRouteAsync(allAlternateRoutes[currentRouteIndex]);
            }
        }

        private void nextRoute_Click(object sender, RoutedEventArgs e)
        {
            currentRouteIndex++;
            ShowMap();
        }

        private void prevRoute_Click(object sender, RoutedEventArgs e)
        {
            currentRouteIndex--;
            ShowMap();
        }

        private void info_Click(object sender, RoutedEventArgs e)
        {
            ShowMessage.PopupMessage(routeDetails);
        }
    }
}
