using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SQLite;
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
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsPresentation;
using GMap.NET.MapProviders;
using System.Device.Location; 

namespace Oop_lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        private Dictionary<PointLatLng, GMapMarker> PointCol = new Dictionary<PointLatLng, GMapMarker>(); //тут все нажатия хранятся
        private List<MapObject> MOCollection = new List<MapObject>(); //Лист всех объектов 

        private List<MapObject> Found = new List<MapObject>();//Лист для поиска

        public MainWindow()
        {
            InitializeComponent();
        }


        private void MapLoaded(object sender, RoutedEventArgs e)
        {   // настройка доступа к данным
            GMaps.Instance.Mode = AccessMode.ServerAndCache;
            // установка провайдера карт
            Map.MapProvider = OpenStreetMapProvider.Instance;
            // установка зума карты
            Map.MinZoom = 2;
            Map.MaxZoom = 20;
            Map.Zoom = 15;
            // установка фокуса карты
            Map.Position = new PointLatLng(55.012823, 82.950359);
            // настройка взаимодействия с картой

            Map.MouseWheelZoomType = MouseWheelZoomType.MousePositionAndCenter;
            Map.CanDragMap = true;
            Map.DragButton = MouseButton.Left;
        }

       
        private void btn_ok_Click(object sender, RoutedEventArgs e)
        {
            if (PointCol.Count == 0)
            {
                MessageBox.Show("Добавьте точки");
                goto end;
            }

            MapObject mapObject = null;
            switch (cb_add.SelectedIndex)
            {
                case 0:
                    mapObject = new Human(tb_name.Text, PointCol.Keys.Last());
                    break;
                case 1:
                    mapObject = new Car(tb_name.Text, PointCol.Keys.Last());
                    break;
                case 2:
                    mapObject = new Location(tb_name.Text, PointCol.Keys.Last());
                    break;
                case 3:
                    mapObject = new Area(tb_name.Text, PointCol.Keys.ToList());
                    break;
                case 4:
                    mapObject = new Route(tb_name.Text, PointCol.Keys.ToList());
                    break;
                default:
                    break;
            }

            bool isNewTitle = true;
            foreach (MapObject objname in MOCollection)
            {
                if (objname.Title == tb_name.Text) isNewTitle = false;
            }
            if (isNewTitle == true)
            {
                MOCollection.Add(mapObject);
                Map.Markers.Add(mapObject.GetMarker());
            }
            else MessageBox.Show("Введите другое название. Это уже используется");
        end:
            foreach (GMapMarker pointMarker in PointCol.Values)
                Map.Markers.Remove(pointMarker);

            PointCol.Clear();

        }



        private void Map_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PointLatLng point = Map.FromLocalToLatLng((int)e.GetPosition(Map).X, (int)e.GetPosition(Map).Y);

            if (Mouset1.IsChecked == true)
            {
                var pointMarker = new GMapMarker(point) {
                    Shape = new Image
                    {
                        Width = 8,
                        Height = 8,
                        Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Mark.png"))
                    }
                };
                Map.Markers.Add(pointMarker);
                PointCol.Add(point, pointMarker);
            }
            else if (Mouset2.IsChecked == true)
            {
                //поиск ближайших
                lb_search.Items.Clear();
                MOCollection.Sort((x, y) => x.GetDistance(point).CompareTo(y.GetDistance(point)));

                foreach (MapObject MO in MOCollection)
                {
                    if (MO.Title.Contains(tb_search.Text))
                        lb_search.Items.Add((int)MO.GetDistance(point) + "Метров до " + MO.Title + " ");
                }

            }
        }

        private void btn_ok_Copy_Click(object sender, RoutedEventArgs e)
        {
            Found.Clear(); lb_search.Items.Clear();
            foreach (MapObject mapObject in MOCollection)
            {
                if (mapObject.Title.Contains(tb_search.Text))
                {
                    Found.Add(mapObject);
                }
            }
            if (Found.Count == 0)
            {
                MessageBox.Show("Объекты не найдены");
            }else
            {
                foreach (MapObject mapObject in Found)
                {
                    lb_search.Items.Add(mapObject.Title);
                }
            }
        }


        //.....................................................................................
        private void Secret_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        switch (Secret.SelectedIndex)
            {
                case 0:
                    Map.MapProvider = OpenStreetMapProvider.Instance;
                    break;
                case 1:
                    Map.MapProvider = GMapProviders.GoogleMap;
                    break;
                case 2:
                    Map.MapProvider = GMapProviders.GoogleSatelliteMap;
                    break;
                case 3:
                    Map.MapProvider = GMapProviders.GoogleHybridMap;
                    break;
                case 4:
                    Map.MapProvider = GMapProviders.BingMap;
                    break;
                case 5:                    
                    Map.MapProvider = GMapProviders.YandexMap;
                    break;
                case 6:
                    Map.MapProvider = GMapProviders.WikiMapiaMap;
                    break;
                default:
                    break;
            }
        }

        private void tb_name_GotFocus(object sender, RoutedEventArgs e)
        {

            if (tb_name.Text == "no name ")
            {
                tb_name.Text = "";
                tb_name.Opacity = 1;
            }
        }

        private void tb_name_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text == "")
            {
                tb_name.Opacity = 0.5;
                tb_name.Text = "no name ";
            }
        }

        private void tb_search_GotFocus(object sender, RoutedEventArgs e)
        {
            if (tb_search.Text == "Поиск ")
            {
                tb_search.Text = "";
                tb_search.Opacity = 1;
            }
        }

        private void Mouset1_Checked(object sender, RoutedEventArgs e)
        {
            Mouset2.IsChecked = false;
        }

        private void Mouset2_Checked(object sender, RoutedEventArgs e)
        {
            Mouset1.IsChecked = false;
        }

        private void tb_search_LostFocus(object sender, RoutedEventArgs e)
        {
            if (tb_search.Text == "")
            {
                tb_search.Opacity = 0.5;
                tb_search.Text = "Поиск ";
            }
        }

        private void btn_res_Click(object sender, RoutedEventArgs e)
        {
            foreach(GMapMarker pointMarker in PointCol.Values)
            Map.Markers.Remove(pointMarker);

            PointCol.Clear();

        }

        private void lb_search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lb_search.SelectedIndex != -1)
            {
                Map.Position = Found[lb_search.SelectedIndex].GetFocus();
                lb_search.SelectedIndex = -1;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Map.Markers.Clear();
            MOCollection.Clear();
            Found.Clear();
            lb_search.Items.Clear();
        }
    }

}

