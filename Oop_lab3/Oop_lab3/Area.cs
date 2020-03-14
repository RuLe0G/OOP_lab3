using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsPresentation;

namespace Oop_lab3
{
    class Area :MapObject
    {
        public List<PointLatLng> Points;

        public Area(string title, List<PointLatLng> points) :base(title)
        {
            Points = points;
           
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
            throw new NotImplementedException();
        }
    }
}
