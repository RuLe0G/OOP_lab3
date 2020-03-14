using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsPresentation;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Oop_lab3
{
    class Location : MapObject
    {

        public PointLatLng Point;

        public Location(string title, PointLatLng point) : base(title)
        {
            Point = point;
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
            GMapMarker marker_chelik = new GMapMarker(Point)
            {
                Shape = new Image
                {
                    Width = 40, // ширина маркера
                    Height = 40, // высота маркера                   
                    Source = new BitmapImage(new Uri("pack://application:,,,/Resources/marker.png")) // картинка
                }
            };
            marker_chelik.Position = Point;
            return marker_chelik;
        }
    }
}
