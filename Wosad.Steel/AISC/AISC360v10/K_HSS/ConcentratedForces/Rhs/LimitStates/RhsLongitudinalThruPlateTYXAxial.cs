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
using Wosad.Common.Entities; 
using Wosad.Common.Section.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
//using Wosad.Analytics.Section;
 
 
using Wosad.Common.Mathematics;
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Sections;



namespace  Wosad.Steel.AISC360v10.HSS.ConcentratedForces
{
    public partial class RhsLongitudinalThroughPlate : RhsToPlateConnection
    {


        internal double GetHSSWallPlastification()
        {

            //(K1-13)
            double R = 0.0;
            double Rn;
            double theta = Angle;
            double sinTheta = Math.Sin(theta.ToRadians());

            double Fy = Hss.Material.YieldStress;
            double t = 0.0;

            ISectionHollow hollowMember = Hss.Section as ISectionHollow;
            if (hollowMember != null)
            {
                t = hollowMember.t_des;
            }
            else
            {
                throw new Exception("Member must be of type IHollowMember");
            }

            
            ISectionTube tube = Hss.Section as ISectionTube;
            if (tube == null)
            {
                throw new Exception("Member must be of type SectionTube");
            }

            double B = tube.B;
            double tp = Plate.Section.B;
            double lb = Plate.Section.H/sinTheta; 
            double Qf = RhsStressInteractionQf(HssPlateOrientation.Longitudinal);

            //(K1-13)
            Rn = 2.0*Fy*Math.Pow(t,2)/(1.0-(tp/B))*(2.0*lb/B+4.0*Math.Sqrt(1.0-tp/B)*Qf)/sinTheta;

            R = Rn * 1.0;


            return R;
        }
    }
}
