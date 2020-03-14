using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GMap.NET; using GMap.NET.MapProviders; using GMap.NET.WindowsPresentation; using GMap.NET.MapProviders; using System.Device.Location; 

namespace Oop_lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PointLatLng point = new PointLatLng(55.016511, 82.946152);
        PointLatLng point_ch;
        List<PointLatLng> points= new PointLatLng[] {
                new PointLatLng(55.016351, 82.950650),
                new PointLatLng(55.017021, 82.951484),
                new PointLatLng(55.015795, 82.954526),
                new PointLatLng(55.015129, 82.953586)
            }.ToList();
        List<PointLatLng> points_path = new PointLatLng[] {
                new PointLatLng(55.010637, 82.938550),
                new PointLatLng(55.012421, 82.940781),
                new PointLatLng(55.014613, 82.943497), 
                new PointLatLng(55.016214, 82.945469) 
            }.ToList(); 

        public MainWindow()
        {
            InitializeComponent();

            

           

            GMapMarker marker = new GMapPolygon(points)
            {
                Shape = new Path
                {
                    Stroke = Brushes.Black, // стиль обводки
                    Fill = Brushes. Violet, // стиль заливки
                    Opacity = 0.7 // прозрачность     
                } 
            };
            
            GMapMarker marker_path = new GMapRoute(points_path)
            {
                Shape = new Path()     
                {
                    Stroke = Brushes.DarkBlue, // цвет обводки
                    Fill = Brushes.DarkBlue, // цвет заливки
                    StrokeThickness = 4 // толщина обводки
                } 
            };
            
            Map.Markers.Add(marker);
            Map.Markers.Add(marker_path);

        }


        private void MapLoaded(object sender, RoutedEventArgs e)
        {   // настройка доступа к данным
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            // установка провайдера карт
            Map.MapProvider = OpenStreetMapProvider.Instance;
            // установка зума карты
            Map.MinZoom = 2;
            Map.MaxZoom = 17;
            Map.Zoom = 15;
            // установка фокуса карты
            Map.Position = new PointLatLng(55.012823, 82.950359);
            // настройка взаимодействия с картой

            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
        }

        private void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Mouset1.IsChecked == true)
            {
                Mouset2.IsChecked = false;
                PointLatLng point_ch = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);
                GMapMarker marker_chelik = new GMapMarker(point)
                {
                    Shape = new Image
                    {
                        Width = 40, // ширина маркера
                        Height = 40, // высота маркера                   
                        Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Chelik.png")) // картинка
                    }
                };
                marker_chelik.Position = point_ch;
                Map.Markers.Add(marker_chelik);
            }
            if (Mouset2.IsChecked == true)
            {
                Mouset1.IsChecked = false;
                

            }
            

        }

        private void Map_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            PointLatLng point_car = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);
            GMapMarker marker_car = new GMapMarker(point)
            {
                Shape = new Image
                {
                    Width = 64, // ширина маркера
                    Height = 64, // высота маркера
                    ToolTip = "Pontiac Firebird 1977", // всплывающая подсказка
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Car.png")) // картинка
                }
            };
            marker_car.Position = point_car;
            Map.Markers.Add(marker_car);
        }
    }
}
