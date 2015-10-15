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
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.SteelEntities;

namespace  Wosad.Analytics.Steel.AISC360_10.HSS.ConcentratedForces
{
    public class RhsCapPlateAxial : RhsToPlateConnection
    {
        public RhsCapPlateAxial(SteelRhsSection Hss, SteelPlateSection Plate, ICalcLog CalcLog)
            : base(Hss, Plate, CalcLog)
        {

        }

        public double GetAvailableStrength()
        {
            double R = 0.0;
            double RLocalYielding = GetLocalYieldingOnSidewalls();
            double RSideWallCrippling = GetLocalCripplingOfSideWalls();

            R = Math.Min(RLocalYielding, RSideWallCrippling);

            return R;
        }

        internal double GetLocalYieldingOnSidewalls()
        {
            double R = 0;
            double tp = Plate.Section.Width;
            ISectionTube tube = Hss.Section as ISectionTube;
            if (tube == null)
            {
                throw new Exception("Member must be of type SectionTube");
            }

            double B = tube.Width;
            double Fy = Hss.Material.YieldStress;
            double lb = tp; //TODO: add diferentiation between lb and tp
            double A = tube.Area;

            double Rn = 0.0;
            if ((5.0*tp+lb)<B)
            {
                //(K1-14a)
                Rn = 2.0 * Fy * tp * (5.0 * tp + lb);
            }
            else
            {
                //(K1-14b)
                Rn = Fy * A;
            }

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

        internal double GetLocalCripplingOfSideWalls()
        {
            double R = 0.0;
            double Rn = 0.0;
            double tp = Plate.Section.Width;
            double lb = tp;
            ISectionTube tube = Hss.Section as ISectionTube;
            if (tube == null)
            {
                throw new Exception("Member must be of type SectionTube");
            }

            double B = tube.Width;
            double t = tube.DesignWallThickness;
            double E = SteelConstants.ModulusOfElasticity;
            double Fy = Hss.Material.YieldStress;

            if (5.0*tp+lb<B)
            {
                //(K1-15)
                Rn=1.6*Math.Pow(t,2)*(1.0+6.0*lb/B*Math.Pow(t/tp,1.5))*Math.Sqrt(E*Fy*tp/t);
            }
            if (DesignFormat == SteelDesignFormat.LRFD)
            {
                R = 0.75 * Rn;
            }
            else
            {
                R = Rn / 2.0;
            }

            return R;
        }
    }
}
