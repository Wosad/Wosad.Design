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
using Wosad.Common.Interfaces;
using Wosad.Common.Mathematics;

namespace Wosad.Steel.AISC.SteelEntities.Bolts
{
    public class BoltGroup:InstantaneousCenterGroup
    {
        List<ILocationArrayElement>  bolts;


        public BoltGroup(List<ILocationArrayElement> Bolts)
        {
           this.bolts =Bolts;
        }



        protected override double GetElementForce(ILocationArrayElement el, Point2D Center, ILocationArrayElement furthestBolt, double angle)
        {
            double Delta_u = furthestBolt.LimitDeformation;
            double LiMax = furthestBolt.GetDistanceToPoint(Center);
            double xi = el.Location.X - Center.X;
            double yi = el.Location.Y - Center.Y;
            double ri = Math.Sqrt(xi * xi + yi * yi); //radial distance from center to this element
            double Delta = Delta_u * ri / LiMax; //this bolt deformation
            double iRn = Math.Pow(1 - Math.Exp(-10.0 * Delta), 0.55); //force developed by this element
            return iRn;
        }

        protected override List<ILocationArrayElement> GetICElements()
        {
            return bolts;
        }

        private double FindLargestElementDistanceFromCenter(Point2D Center)
        {
            var MaxDistance = Elements.Max(b => Math.Sqrt(Math.Pow(b.Location.X + Center.X, 2) + Math.Pow(b.Location.Y + Center.Y, 2)));
            return MaxDistance;
        }

        protected override ILocationArrayElement FindUltimateDeformationElement(Point2D Center)
        {
            double LiMax  = FindLargestElementDistanceFromCenter(Center);
            double DeltaMax = 0.34;
            var ControllingBolt = bolts.Where(b => b.GetDistanceToPoint(Center) == LiMax).FirstOrDefault();
            ControllingBolt.LimitDeformation = DeltaMax;
            return ControllingBolt;
        }
    }
}
