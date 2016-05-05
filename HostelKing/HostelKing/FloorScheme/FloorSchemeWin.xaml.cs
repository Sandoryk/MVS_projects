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
        public FloorSchemeView()
        {
            StringReader stringReader;
            XmlReader xmlReader;

            InitializeComponent();

            string viewBoxXaml = XamlWriter.Save(this.FlatViewBox);
            int columnsCount = this.FlatsGrid.ColumnDefinitions.Count;
            for (int i = 0; i < columnsCount-1; i++)
            {
                if (i==3)
                {
                    continue;
                }
                using (stringReader = new StringReader(viewBoxXaml))
                {
                    xmlReader = XmlReader.Create(stringReader);
                    Viewbox newViewBox = (Viewbox)XamlReader.Load(xmlReader);
                    this.FlatsGrid.Children.Add(newViewBox);
                    Grid.SetColumn(newViewBox, i);
                }
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
                    Grid.SetColumn(newViewBox, i);
                    Grid.SetRow(newViewBox, 2);
                }
            }
        }
        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ((Slider)sender).Value = Math.Round(e.NewValue,0);
        }
    }
}
