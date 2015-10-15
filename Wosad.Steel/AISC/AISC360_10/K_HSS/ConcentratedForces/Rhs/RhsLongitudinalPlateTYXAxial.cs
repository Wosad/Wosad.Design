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
////using Wosad.Analytics.Section;
 
using Wosad.Common.Mathematics;
using Wosad.Analytics.Steel.AISC360_10.HSS;
 
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.Code;

namespace  Wosad.Analytics.Steel.AISC360_10.HSS.ConcentratedForces
{
    public  class RhsLongitudinalPlateTYXAxial: RhsToPlateConnection
    {
        private double angle;

        public double Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public RhsLongitudinalPlateTYXAxial(SteelRhsSection Hss, SteelPlateSection Plate, double Angle, ICalcLog CalcLog)
            : base(Hss, Plate,  CalcLog)
        {
            this.angle = Angle;
        }

        public double GetAvailableStrength(double RequiredAxialStrenghPro, double RequiredMomentStrengthMro)
        {
            ISteelSection s = GetHssSteelSection();
            double U = GetUtilizationRatio(s, RequiredAxialStrenghPro, RequiredMomentStrengthMro);
            return this.GetAvailableStrength( U);
        }
        public double GetAvailableStrength(double UtilizationRatio)
        {
          
            return LsHSSPlastification(UtilizationRatio);
        }
        
        internal double LsHSSPlastification(double UtilizationRatio)
        {
        //(K1-12)
            double R=0.0;
            double Rn;
            double theta = Angle;
            double sinTheta = Math.Sin(theta.ToRadians());

            double Fy = Hss.Material.YieldStress;
            double t = 0.0;

            ISectionHollow hollowMember = Hss.Section as ISectionHollow;
            if (hollowMember !=null)
	        {
		        t = hollowMember.DesignWallThickness;
	        }
            else
	            {
                    throw new Exception ("Member must be of type IHollowMember");
	            }

            double tp = Plate.Section.Height;
            ISectionTube tube = Hss.Section as ISectionTube;
            if (tube ==null)
            {
                throw new Exception("Member must be of type SectionTube");
            }

            double B = tube.Width;
            double lb = tp; //Can add functionality to distinguish between lb and tp TODo:
            double Qf = GetChordStressInteractionQf(PlateOrientation.Longitudinal, UtilizationRatio);

            //(K1-12)
            Rn=Fy*Math.Pow(t,2)/(1.0-tp/B)*(2.0*lb/B+4.0*Math.Sqrt(1.0-tp/B)*Qf)/sinTheta;

            if (DesignFormat == SteelDesignFormat.LRFD)
            {
                R = 1.0 * Rn;
            }
            else
            {
                R = Rn / 1.5;
            }

            return R;
        }
    }
}
