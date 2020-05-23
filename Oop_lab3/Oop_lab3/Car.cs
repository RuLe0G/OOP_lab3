using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace Oop_lab3
{
    class Car : MapObject
    {
        public PointLatLng Point;

        public Car(string title, PointLatLng startPoint) : base(title)
        {
            Point = startPoint;
        }

        public override double GetDistance(PointLatLng point)
        {
            GeoCoordinate c1 = new GeoCoordinate(point.Lat, point.Lng);
            GeoCoordinate c2 = new GeoCoordinate(Point.Lat, Point.Lng);

            return c1.GetDistanceTo(c2);
        }

        public override PointLatLng GetFocus()
        {
            return Point;
        }

        public override GMapMarker GetMarker()
        {
            GMapMarker marker_car = new GMapMarker(Point)
            {
                Shape = new Image
                {
                    Width = 40, // ширина маркера
                    Height = 40, // высота маркера
                    ToolTip = Title, // всплывающая подсказка
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/Car.png")) // картинка
                }
            };
            marker_car.Position = Point;
            return marker_car;
        }
    }
}

