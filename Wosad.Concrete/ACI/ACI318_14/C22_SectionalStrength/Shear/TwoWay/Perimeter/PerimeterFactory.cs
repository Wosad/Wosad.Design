using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Mathematics;

namespace Wosad.Concrete.ACI.ACI318_14.C22_SectionalStrength.Shear.TwoWay
{
    public class PerimeterFactory
    {
        /// <summary>
        /// Returns the list of lines for calculation of punching shear perimeter properties
        /// </summary>
        /// <param name="ColumnType">Configuration of punching perimeter</param>
        /// <param name="c_1">Column dimension perpendicular  to free edge</param>
        /// <param name="c_2">Column dimension parallel to free edge</param>
        /// <param name="d"> Effective depth of punching shear perimeter</param>
        /// <returns></returns>
        public List<PerimeterLineSegment> GetPerimeterSegments(PunchingPerimeterColumnType ColumnType, double c1, double c2, double d)
        {
            double b1; // punching perimeter dimension perpendicular to free edge
            double b2; // punching perimeter dimension parallel to free edge 
            Point2D p1  =null;
            Point2D p2  =null;
            Point2D p3  =null;
            Point2D p4 = null;

            switch (ColumnType)
            {
                case PunchingPerimeterColumnType.Interior:
                   
                    b1 = c1 + d;
                    b2 = c2 + d;

                    p1 = new Point2D(-b2/2.0,-b1/2.0 ) ;
                    p2 = new Point2D(-b2/2.0,b1/2.0 ) ;
                    p3 = new Point2D(b2/2.0,b1/2.0 ) ;
                    p4 = new Point2D(b2/2.0,-b1/2.0 ) ;

                    return new List<PerimeterLineSegment>()
                    {
                        new PerimeterLineSegment(p1,p2 ),
                        new PerimeterLineSegment(p2,p3 ),
                        new PerimeterLineSegment(p3,p4 ),
                        new PerimeterLineSegment(p4,p1 )
                    };

                    break;
                case PunchingPerimeterColumnType.Edge:

                    b1 = c1 + d/2.0;
                    b2 = c2 + d;

                    p1 = new Point2D(-b2/2.0,b1/2.0 ) ;
                    p2 = new Point2D(-b2/2.0,-b1/2.0 ) ;
                    p3 = new Point2D(b2/2.0,-b1/2.0 ) ;
                    p4 = new Point2D(b2 / 2.0, b1 / 2.0);

                    return new List<PerimeterLineSegment>()
                    {
                        new PerimeterLineSegment(p1,p2 ),
                        new PerimeterLineSegment(p2,p3 ),
                        new PerimeterLineSegment(p3,p4 ),
                    };


                    break;
                case PunchingPerimeterColumnType.Corner:

                    b1 = c1 + d/2.0;
                    b2 = c2 + d/2.0;

                    p1 = new Point2D(-b2/2.0,-b1/2.0 ) ;
                    p2 = new Point2D(b2/2.0,-b1/2.0 ) ;
                    p3 = new Point2D(b2 / 2.0, b1 / 2.0);

                    return new List<PerimeterLineSegment>()
                    {
                        new PerimeterLineSegment(p1,p2 ),
                        new PerimeterLineSegment(p2,p3 ),
                    };

                    break;
                default:
                    throw new Exception("Unrecognized punching perimeter column type");
                    break;
            }

        }
    }
}
