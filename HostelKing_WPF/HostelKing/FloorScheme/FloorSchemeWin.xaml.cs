using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace HostelKing
{
    /// <summary>
    /// Interaction logic for FloorSchemeWin.xaml
    /// </summary>
    public partial class FloorSchemeView : Window
    {
        List<IRoomFurniture> FurnitureList;
        List<IRoom> RoomList;
        List<ISettledList> SettleList;
        public FloorSchemeView()
        {
            StringReader stringReader;
            XmlReader xmlReader;

            InitializeComponent();

            string viewBoxXaml = XamlWriter.Save(this.FlatViewBox);
            int flatCnt = 2;
            int columnsCount = this.FlatsGrid.ColumnDefinitions.Count;
            for (int i = 1; i < columnsCount-1; i++)
            {
                if (i==3)
                {
                    continue;
                }
                using (stringReader = new StringReader(viewBoxXaml))
                {
                    xmlReader = XmlReader.Create(stringReader);
                    Viewbox newViewBox = (Viewbox)XamlReader.Load(xmlReader);
                    Canvas cv = GetChildOfType<Canvas>(newViewBox);
                    if (cv != null)
                        cv.Name = "Flat" + flatCnt;
                    this.FlatsGrid.Children.Add(newViewBox);
                    Grid.SetColumn(newViewBox, i);
                }
                flatCnt++;
            }
            for (int i = 0; i < columnsCount-1; i++)
            {
                if (i == 3)
                {
                    continue;
                }
                using (stringReader = new StringReader(viewBoxXaml))
                {
                    xmlReader = XmlReader.Create(stringReader);
                    Viewbox newViewBox = (Viewbox)XamlReader.Load(xmlReader);
                    newViewBox.RenderTransformOrigin = new Point(0.5,0.5);
                    newViewBox.RenderTransform = new RotateTransform(180);
                    this.FlatsGrid.Children.Add(newViewBox);
                    Canvas cv = GetChildOfType<Canvas>(newViewBox);
                    if (cv != null)
                        cv.Name = "Flat" + flatCnt;
                    Grid.SetColumn(newViewBox, i);
                    Grid.SetRow(newViewBox, 2);
                }
                flatCnt++;
            }
            using (DataBaseConnector dbService = new DataBaseConnector())
            {
                FurnitureList = dbService.GetAllRecords<IRoomFurniture>();
                RoomList = dbService.GetAllRecords<IRoom>();
                SettleList = dbService.GetAllRecords<ISettledList>();
            }
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).Value = Math.Round(e.NewValue,0);
        }

        private void OpenPopUp(object sender, MouseButtonEventArgs e)
        {
            if (e.Source.GetType() == typeof(Canvas))
            {
                popup1.IsOpen = true;
                Canvas cv = ((Canvas)e.Source);
                //MessageBox.Show(cv.Name);
                string roomNumber = "";
                string roomUUID = "";
                if (cv.Name.Contains("Flat"))
                {
                    roomNumber = (FlatSlider.Value + 1) + "-" + cv.Name.Substring(4);
                    FlatNumber.Content = roomNumber;
                    IRoom r = RoomList.Where(t => t.RoomNumber == roomNumber).FirstOrDefault();
                    if (r != null)
                        roomUUID = r.UUID;
                    else
                        MessageBox.Show("Невозможно определить комнату");
                }

                HabitantsAmount.Content = SettleList.Where(t => t.RoomUUID == roomUUID).Count();
                PopUpGrid.ItemsSource = FurnitureList.Where(t=> t.RoomUUID==roomUUID).ToList();
            }
        }
        public static T GetChildOfType<T>(DependencyObject depObj)
    where T : DependencyObject
        {
            if (depObj == null) return null;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                var child = VisualTreeHelper.GetChild(depObj, i);

                var result = (child as T) ?? GetChildOfType<T>(child);
                if (result != null) return result;
            }
            return null;
        }
    }
}
