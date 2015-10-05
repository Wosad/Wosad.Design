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
using Wosad.Common.Mathematics;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC.SteelEntities;

namespace  Wosad.Analytics.Steel.AISC360_10.HSS.ConcentratedForces
{
    public class RhsTransversePlateCrossAxial: RhsTransversePlateTandXAxial
    //Note: the difference between RhsTransversePlateTeeAxial and RhsTransversePlateCrossAxial
    //Local Crippling of HSS Sidewalls limit state
    {
        public RhsTransversePlateCrossAxial(SteelRhsSection Hss, SteelPlateSection Plate, SteelDesignFormat DesignFormat, ICalcLog CalcLog)
            : base(Hss, Plate,DesignFormat, CalcLog)
        {

        }

        public double GetAvailableStrength(SteelDesignFormat format, double RequiredAxialStrenghPro, double RequiredMomentStrengthMro)
        {
            ISteelSection s = GetHssSteelSection();
            double U = GetUtilizationRatio(s,RequiredAxialStrenghPro, RequiredMomentStrengthMro);
            return this.GetAvailableStrength(format, U);
        }       
        public double GetAvailableStrength(SteelDesignFormat format, double ChordUtilizationRatio)
        {
            double R = 0.0;
            this.DesignFormat = format;
            //TABLE K1.2 case 1
            double Fy = Hss.Material.YieldStress;
            ISectionTube tube = Hss.Section as ISectionTube;
            if (tube == null)
            {
                throw new Exception("Rectangular Hss member must implement ISectionTube interface.");
            }
            double B = tube.Width;
            double H = tube.Height;
            double E = SteelConstants.ModulusOfElasticity;
            double Bp = Plate.Section.Height;
            double Fyp = Plate.Material.YieldStress;
            double t = tube.DesignWallThickness;
            double tp = Plate.Section.Width;
            double beta = Bp / B;
            double lb = tp; // may need to add differentiation here TODO:
            double Qf = GetChordStressInteractionQf(PlateOrientation.Transverse, ChordUtilizationRatio);

            //Limit States:
            double LocalYieldingOfPlateLs = GetLocalYieldingOfPlateLs(B, Bp, t, tp, Fy, Fyp); //K1-7
            double HssPunchingLs = GetHssPunchingLs(Fy, t, tp, B, Bp);
            double LocalYieldingOfSideWalls = GetHssSideYieldingLs(beta, Fy, t, lb);
            double LocalCripplingOfHssSidewalls = GetLocalCripplingOfSideWallsLs(t, H, E, Fy, Qf);
            double[] LimitStates = new double[] { LocalYieldingOfPlateLs, HssPunchingLs, LocalYieldingOfSideWalls };
            R = LimitStates.Min();
            return R;
        }


        internal double GetLocalCripplingOfSideWallsLs(double t, double H, double E, double Fy, double Qf)
        {
            double R = 0.0;
            double Rn = 0.0;
            Rn =(48.0*Math.Pow(t,3)/(H-3.0*t))*Math.Sqrt(E*Fy)*Qf;
            return R;
        }
    }
}
