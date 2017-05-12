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
    class SubwayMapNavigation
    {
        Button[] activeStationButtons = new Button[2];
        public SubwayMapNavigation()
        {
            ClickCommand = new Command(ClickMethod);
        }
        public ICommand ClickCommand { get; set; }
        public IAnimationOperator RouteBuilder { get; set; }
        public List<SubwayStation> StationList { get; set; }
        private void DrawRoute()
        {
            Int32 firstStNum, secondStNum;
            
            if (RouteBuilder != null)
            {
                RouteBuilder.StopAnimation();
                if (activeStationButtons[0] != null && activeStationButtons[1] != null)
                {
                    SubwayStation startStation = StationList.Find(t => t.Name == activeStationButtons[0].Name);
                    SubwayStation endStation = StationList.Find(t => t.Name == activeStationButtons[1].Name);
                    if (startStation != null && endStation != null)
                    {
                        firstStNum = startStation.Number;
                        secondStNum = endStation.Number;
                        if (startStation.BrachLine == endStation.BrachLine)
                        {
                            if (startStation.Number > endStation.Number)
	                        {
		                        Int32 temp = firstStNum;
                                firstStNum = secondStNum;
                                secondStNum = temp;
	                        }
                            List<SubwayStation> stationsOnRoute = StationList.FindAll(t => t.Number >= firstStNum && t.Number <= secondStNum);
                            if (stationsOnRoute != null)
                            {
                                if (startStation.Number > endStation.Number)
	                            {
                                    stationsOnRoute.Reverse();
	                            }
                                Point[] points = new Point[stationsOnRoute.Count];
                                Int32 nextIndex = 0;
                                foreach (var station in stationsOnRoute)
                                {
                                    points[nextIndex++] = new Point(station.X, station.Y);
                                }
                                RouteBuilder.BeginAnimation(points);
                            }
                        }
                        else
                        {

                        }
                    }
                }
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
