using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SubwayNavigation
{
    class RouteNavigation
    {
        Button[] activeStationButtons = new Button[2];
        public RouteNavigation()
        {
            ClickCommand = new Command(ClickMethod);
        }
        public ICommand ClickCommand { get; set; }
        public List<SubwayStation> StationList { get; set; }
        private void DrawRoute()
        {
            if (activeStationButtons[0] != null && activeStationButtons[1] != null)
            {
                Polyline route = new Polyline();
            }
        }
        private void ClickMethod(object sender)
        {
            Button clickedButton, activeButton;

            if (sender != null && sender is Button)
            {
                clickedButton = (sender as Button);
                if (activeStationButtons.Length == 2)
                {
                    if (activeStationButtons[0] != null && activeStationButtons[0].Name == clickedButton.Name)
                    {
                        activeStationButtons[0] = null;
                        clickedButton.Margin = new Thickness(clickedButton.Margin.Left + 4, clickedButton.Margin.Top + 4, 0, 0);
                        clickedButton.Height = clickedButton.Height - 8;
                        clickedButton.Width = clickedButton.Width - 8;
                    }
                    else if (activeStationButtons[1] != null && activeStationButtons[1].Name == clickedButton.Name)
                    {
                        activeStationButtons[1] = null;
                        clickedButton.Margin = new Thickness(clickedButton.Margin.Left + 4, clickedButton.Margin.Top + 4, 0, 0);
                        clickedButton.Height = clickedButton.Height - 8;
                        clickedButton.Width = clickedButton.Width - 8;
                    }
                    else if (activeStationButtons[0] == null)
                    {
                        activeStationButtons[0] = clickedButton;
                        clickedButton.Margin = new Thickness(clickedButton.Margin.Left - 4, clickedButton.Margin.Top - 4, 0, 0);
                        clickedButton.Height = clickedButton.Height + 8;
                        clickedButton.Width = clickedButton.Width + 8;
                    }
                    else if (activeStationButtons[1] == null)
                    {
                        activeStationButtons[1] = clickedButton;
                        clickedButton.Margin = new Thickness(clickedButton.Margin.Left - 4, clickedButton.Margin.Top - 4, 0, 0);
                        clickedButton.Height = clickedButton.Height + 8;
                        clickedButton.Width = clickedButton.Width + 8;
                    }
                    else
                    {
                        activeButton = activeStationButtons[1];
                        if (activeButton != null)
                        {
                            activeButton.Margin = new Thickness(activeButton.Margin.Left + 4, activeButton.Margin.Top + 4, 0, 0);
                            activeButton.Height = activeButton.Height - 8;
                            activeButton.Width = activeButton.Width - 8;
                            activeStationButtons[1] = null;
                        }
                        activeStationButtons[1] = clickedButton;
                        clickedButton.Margin = new Thickness(clickedButton.Margin.Left - 4, clickedButton.Margin.Top - 4, 0, 0);
                        clickedButton.Height = clickedButton.Height + 8;
                        clickedButton.Width = clickedButton.Width + 8;
                    }
                    DrawRoute();
                }
            }
            else
                MessageBox.Show("Unknown station");
        }
    }
}
