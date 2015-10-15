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
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.Code;

namespace  Wosad.Analytics.Steel.AISC360_10.HSS.ConcentratedForces
{
    public class RhsTransversePlateTeeAxial: RhsTransversePlateTandXAxial
    //Note: the difference between RhsTransversePlateTeeAxial and RhsTransversePlateCrossAxial
    //Local Crippling of HSS Sidewalls limit state
    {
        public RhsTransversePlateTeeAxial(SteelRhsSection Hss, SteelPlateSection Plate,  ICalcLog CalcLog)
            : base(Hss, Plate,CalcLog)
        {

        }

        public double GetAvailableStrength( )
        {
            double R = 0.0;
            //TABLE K1.2 case 1
            double Fy = Hss.Material.YieldStress;
            ISectionTube tube = Hss.Section as ISectionTube;
            if (tube == null)
            {
		        throw new Exception ("Rectangular Hss member must implement ISectionTube interface.");
            }
            double B = tube.Width;
            double Bp = Plate.Section.Height;
            double Fyp = Plate.Material.YieldStress;
            double t = tube.DesignWallThickness;
            double tp = Plate.Section.Width;
            double beta = Bp/B;

            //Limit States:
            double LocalYieldingOfPlateLs = GetLocalYieldingOfPlateLs(B,Bp,t,tp,Fy,Fyp); //K1-7
            double HssPunchingLs = GetHssPunchingLs(Fy,t,tp,B,Bp);

            return R;
        }
       internal double GetLocalCripplingOfSideWallsLs(double t, double lb, double H, double E, double Fy, double Qf)
       {
           double R = 0.0;
           double Rn = 0.0;
           Rn=1.6* Math.Pow(t,2)*(1+3.0*lb/(H-3.0*t))*Math.Sqrt(E*Fy)*Qf;

                R = Rn * 1.0;


           return R;
       }

                

    }
}
