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



namespace  Wosad.Steel.AISC360_10.HSS.ConcentratedForces
{
    public abstract class RhsTransversePlateTandXAxial:RhsToPlateConnection
    {
        public RhsTransversePlateTandXAxial(SteelRhsSection Hss, SteelPlateSection Plate,   ICalcLog CalcLog)
            : base(Hss, Plate, CalcLog)
        {

        }


        internal double GetLocalYieldingOfPlateLs(double B, double Bp, double t, double tp, double Fy, double Fyp)
        {
            //(K1-7)
            double R;
            double Rn = 0.0;
            Rn = 10.0/(B/t)*Fy*Bp;
            double RnMax = Fyp * tp * Bp;
            Rn = Rn > RnMax ? RnMax : Rn;

                R = Rn * 0.95;

            return R;
        }

        internal double GetHssPunchingLs(double Fy, double t, double tp, double B, double Bp)
        {
            double R=0.0;
            double Rn = 0.0;

            if (Bp<=(B-2.0*t)&& Bp>=0.85*B)
	        {
		        double Bep = this.GetEffectivePlateWidth();
                Rn=0.6*Fy*t*(2.0*tp+2.0*Bep); //(K1-8)
 
		            R = Rn*0.95;
      
	        }   
            else
	        {
                R = double.PositiveInfinity;
	        }

            return R;
        }

        internal double GetHssSideYieldingLs(double beta,double Fy,double t, double lb)
            {
            double R=0.0;
            double Rn =0.0;
                //assume that Lb is bearing width, generally same as tp
            if (beta == 1.0)
	            {
		            double k = 1.5*t; //outside radius
                    Rn = 2.0*Fy*t*(5.0*k+lb);  //(K1-9)
	            }
            else
	            {
                 R=double.PositiveInfinity;
	            }
  
	            R = Rn*1.0;


            return R;

            }


    }
}
