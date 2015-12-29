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

namespace  Wosad.Steel.AISC360_10.HSS.ConcentratedForces
{
    public class ChsCapPlate: ChsToPlateConnection
    {
        public ChsCapPlate(SteelChsSection Hss, SteelPlateSection Plate, ICalcLog CalcLog)
            : base(Hss, Plate, CalcLog)
        {
           
        }

        double GetAvailableStrength()
        {
            double R = 0.0;

            double Fy = 0.0; double t = 0.0; double Bp = 0.0; double D = 0.0; double tp = 0.0;
            this.GetTypicalParameters(ref Fy, ref t, ref Bp, ref D, ref tp);

            double lb = tp; //TODO: differentiate between lb and tp here in the future

            //(K1-4)
            double Rn = 2.0 * Fy * t * (5.0 * tp + lb);
            double A = Hss.Section.Area;
            Rn = Rn < Fy * A ? Rn : Fy * A;

                R = 1.0 * Rn;


            return R;
        }
    
    }
}
