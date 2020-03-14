using System;
using System.Collections.Generic;
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
            throw new NotImplementedException();
        }

        public override PointLatLng GetFocus()
        {
            throw new NotImplementedException();
        }

        public override GMapMarker GetMarker()
        {
            GMapMarker marker_car = new GMapMarker(Point)
            {
                Shape = new Image
                {
                    Width = 64, // ширина маркера
                    Height = 64, // высота маркера
                    ToolTip = Title, // всплывающая подсказка
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/marker.png")) // картинка
                }
            };
            marker_car.Position = Point;
            return marker_car;
        }
    }
}

