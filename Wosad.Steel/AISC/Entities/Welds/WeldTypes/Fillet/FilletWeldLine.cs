#region Copyright
   /*Copyright (C) 2015 Wosad Inc

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
   */
#endregion
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using Wosad.Common.Mathematics;

namespace Wosad.Steel.AISC.SteelEntities.Welds
{
    /// <summary>
    /// Weld line is a straight line implementation of weld segment base class.
    /// </summary>
    public class FilletWeldLine : WeldSegmentBase
    {
        private Point2D p1;
        private Point2D p2;

        private double length;

        public double Length
        {
            get 
            {
                length = GetLength();
                return length; 
            }

        }

        private double GetLength()
        {
            double dx2 = Math.Pow(p2.X-p1.X,2);
            double dy2 = Math.Pow(p2.Y-p1.Y,2);
            double L = Math.Sqrt(dx2 + dy2);
            return L;
        }

        public FilletWeldLine(Point2D p1, Point2D p2, double leg, double ElectrodeStrength, int NumberOfSubdivisions)
        {
            // TODO: Complete member initialization
            this.p1 = p1;
            this.p2 = p2;
            this.Leg = leg;
            this.ElectrodeStrength = ElectrodeStrength;
            this.NumberOfSubdivisions = NumberOfSubdivisions;
            
        }


        protected override void CalculateElements()
        {
            WeldElements = new List<IWeldElement>();
            double dx = p2.X - p1.X;
            double dy = p2.Y - p1.Y;
            
            Vector seg = new Vector(dx, dy);
            int N = NumberOfSubdivisions;

            double segDx;
            double segDy;

            segDx = dx / N;
            segDy = dy / N;

            for (int i = 0; i < NumberOfSubdivisions; i++)
            {
                Point2D stPt = new Point2D(p1.X + i * segDx, p1.Y + i * segDy);
                Point2D enPt = new Point2D(p1.X + (i + 1) * segDx, p1.Y + (i + 1) * segDy);
                //Need to change this to be independent of fillet weld type
                FilletWeldElement weld = new FilletWeldElement(stPt, enPt, Leg, ElectrodeStrength);
                WeldElements.Add(weld);
            }
        }



        public override bool Equals(object obj)
        {
            // If parameter is null return false.
            if (obj == null)
            {
                return false;
            }

            // If parameter cannot be cast to Point return false.
            FilletWeldLine line = obj as FilletWeldLine;
            if ((System.Object)line == null)
            {
                return false;
            }

            // Return true if the fields match:
            return (p1 == line.p1) && (p2 == line.p2);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }


        public override double GetInertiaYAroundPoint(Point2D center)
        {
            double Iy = 0;
            foreach (IWeldElement el in this.WeldElements)
            {
                double IyThis =el.GetLineMomentOfInertiaYAroundPoint(center);
                Iy = Iy + IyThis;
            }
            return Iy;
        }

        public override double GetInertiaXAroundPoint(Point2D center)
        {
            double Ix = 0;
            foreach (FilletWeldElement el in this.WeldElements)
            {
                double IxThis = el.GetLineMomentOfInertiaXAroundPoint(center);
                Ix = Ix + IxThis;
            }
            return Ix;
        }

        public override double GetPolarMomentOfInetriaAroundPoint(Point2D center)
        {
            throw new NotImplementedException();
        }



        public override double GetArea()
        {
            double A = 0;
            A = this.Length * this.Leg;

            return A;
        }


        public override double GetSumAreaDistanceX(Point2D refPoint)
        {

            double A = GetArea();
            double dx = this.Centroid.X - refPoint.X;
            return A*dx;
        }

        public override double GetSumAreaDistanceY(Point2D refPoint)
        {
            double A = GetArea();
            double dy = this.Centroid.Y - refPoint.Y;
            return A * dy;
        }


        public Point2D Centroid
        {
            get { return GetCentroid(); }
        }
        

        public Point2D GetCentroid()
        {
            double X = (p1.X + p2.X) / 2.0;
            double Y = (p1.Y + p2.Y) / 2.0;
            return new Point2D(X, Y);
        }
    }
}
