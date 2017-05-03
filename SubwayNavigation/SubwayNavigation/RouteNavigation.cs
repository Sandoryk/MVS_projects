using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SubwayNavigation
{
    class RouteNavigation
    {
        SubwayStation[] activeStations = new SubwayStation[2];
        public RouteNavigation()
        {
            ClickCommand = new Command(ClickMethod);
        }
        public ICommand ClickCommand { get; set; }
        public List<SubwayStation> StationList { get; set; }

        private void HandleActiveStations(SubwayStation clickedStation)
        {
            SubwayStation originalStation;

            if (clickedStation != null && activeStations.Length == 2)
            {
                if (activeStations[0] != null && activeStations[0].Name == clickedStation.Name)
                {
                    activeStations[0] = null;
                    originalStation = StationList.Find(t => t.Name == clickedStation.Name);
                    if (originalStation != null)
                    {
                        clickedStation.X = originalStation.X;
                        clickedStation.Y = originalStation.Y;
                    }
                }
                else if (activeStations[0] == null)
                {
                    activeStations[0] = clickedStation;
                }
                else if (activeStations[1] == null)
                {
                    activeStations[1] = clickedStation;
                }
            }
            return;
        }

        private void ClickMethod(object sender)
        {
            if (sender != null && sender is Button)
            {
                Button clickedButton = (sender as Button);
                clickedButton.Margin = new Thickness(clickedButton.Margin.Left - 4, clickedButton.Margin.Top - 4, 0, 0);
                clickedButton.Height = clickedButton.Height + 8;
                clickedButton.Width = clickedButton.Width + 8;
                HandleActiveStations(StationList.Find(t => t.Name == clickedButton.Name));
            }
            else
                MessageBox.Show("Unknown station");
        }
    }
}
