using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Mathematics;

namespace Wosad.Concrete.ACI.ACI318_14.C22_SectionalStrength.Shear.TwoWay
{
    public class PerimeterLineSegment
    {
        Point2D PointI { get; set; }
        Point2D PointJ { get; set; }

        public PerimeterLineSegment(Point2D PointI, Point2D PointJ)
        {
            this.PointI = PointI;
            this.PointJ = PointJ;
        }

        private double length;

        public double Length
        {
            get {
                length = Math.Sqrt(Math.Pow(PointJ.X - PointI.X, 2.0) + Math.Pow(PointJ.Y - PointI.Y, 2.0));
                return length; }
            set { length = value; }
        }
        
    }
}
