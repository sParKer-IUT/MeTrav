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
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=391641

namespace MeTrav
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool searchingCurrLocation = false;
        bool hasCurrLocation = false;

        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;

            MeTravUtility.FillAllLocationList();

            MeTravUtility.FillAllRoutes();

            MeTravUtility.FillAllBuses();

            //CurrentLocationAsync();
        }
        
        //async void CurrentLocationAsync()
        //{
        //    searchingCurrLocation = true;
        //    hasCurrLocation = await CurrentLocationFinder.GetCurrentLocationAsync();
        //    if (!hasCurrLocation)
        //    {
        //        MessageDialog msg = new MessageDialog("Couldn't get current location");
        //        await msg.ShowAsync();
        //        if (!hasCurrLocation)
        //        locationcheckBox.IsChecked = false; 
        //    }
        //    searchingCurrLocation = false;
        //}

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // TODO: Prepare page for display here.

            // TODO: If your application contains multiple pages, ensure that you are
            // handling the hardware Back button by registering for the
            // Windows.Phone.UI.Input.HardwareButtons.BackPressed event.
            // If you are using the NavigationHelper provided by some templates,
            // this event is handled for you.
        }

        private async void submitButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchingCurrLocation)
            {
                await ShowMessage.PopupMessage("Finding your location. Please Wait");
                return;
            }
            bool foundS = false, foundD = false;
            Location s = null, d = null;

            /*if (locationcheckBox.IsChecked == true)
            {
                if (!hasCurrLocation)
                {
                    await ShowMessage.PopupMessage("Couldn't get current location");
                    return;
                }

                Geopoint sourcepoint = new Geopoint(CurrentLocationFinder.currGeoposition.Coordinate.Point.Position);
                MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(sourcepoint);
                if (result.Status == MapLocationFinderStatus.Success)
                {
                    if (result.Locations.Count > 0)
                    {
                        sourceASBox.Text = result.Locations[0].Address.Town.ToString();
                        //SearchLocations sln = new SearchLocations(sourceASBox.Text, sourcepoint.Position.Latitude, sourcepoint.Position.Longitude);
                        Location sln = new Location(sourceASBox.Text, result.Locations[0].Point.Position.Latitude, result.Locations[0].Point.Position.Longitude);
                        s = sln;
                        foundS = true;
                    }
                }
            }*/
            if(sourceASBox.Text != "" && destASBox.Text != "")
            {
                foreach (Location sl in MeTravUtility.AllLocations)
                {
                    if (!foundS && sl.name.Equals(sourceASBox.Text))
                    {
                        foundS = true;
                        s = sl;
                    }
                    if (!foundD && sl.name.Equals(destASBox.Text))
                    {
                        foundD = true;
                        d = sl;
                    }

                    if (foundS && foundD) break;
                }

                if (s == null || d == null)
                {
                    await ShowMessage.PopupMessage("Invalid Source or Destination");
                }
                else
                {
                    SelectedLocations selectedlocations = new SelectedLocations(s, d);
                    this.Frame.Navigate(typeof(Map), selectedlocations);
                }
            }
            else
            {
                await ShowMessage.PopupMessage("Invalid Source or Destination");
            }
            
        }

        private void sourceASBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            List<string> searchSuggestions = new List<string>();

            foreach (Location sl in MeTravUtility.AllLocations)
            {
                if(sl.name.Contains(sourceASBox.Text))searchSuggestions.Add(sl.name);
            }

            sourceASBox.ItemsSource = searchSuggestions;
        }

        private void sourceASBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void destASBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {

        }

        private void destASBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            List<string> searchSuggestions = new List<string>();

            foreach (Location sl in MeTravUtility.AllLocations)
            {
                if (sl.name.Contains(destASBox.Text)) searchSuggestions.Add(sl.name);
            }

            destASBox.ItemsSource = searchSuggestions;
        }

        /*private void locationcheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CurrentLocationAsync();
            
            sourceASBox.IsEnabled = false;
        }*/

        /*private void locationcheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            sourceASBox.IsEnabled = true;
        }*/
    }
}
