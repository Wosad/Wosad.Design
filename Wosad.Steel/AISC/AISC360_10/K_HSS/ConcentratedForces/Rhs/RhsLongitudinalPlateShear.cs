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
 
using Wosad.Steel.AISC360_10.Connections.AffectedElements;
 
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities.Sections;

 


namespace  Wosad.Steel.AISC360_10.HSS.ConcentratedForces
{
    public class RhsLongitudinalPlateShear : RhsToPlateConnection
    {
        ICalcLog CalcLog;
        public RhsLongitudinalPlateShear(SteelRhsSection Hss, SteelPlateSection Plate,   ICalcLog CalcLog)
            : base(Hss, Plate, CalcLog)
        {
            this.CalcLog = CalcLog;
        }

        public double GetAvailableStrength()
        {
            double R = 0.0;
            // Use equation (K1-3) to get maximum plate thickness
            double Fu = Hss.Material.UltimateStress;
            double Fyp = Plate.Material.YieldStress;
            double t = Hss.Section.t_des;
            double tmax = GetMaximumPlateThickness();
            Plate.Section.B = tmax;

            //Calculate plate shear capacity per Chapter J
            AffectedElementInFlexureAndShear ae = new AffectedElementInFlexureAndShear(Plate,  this.CalcLog);
            throw new NotImplementedException();
            //double ShearCapacity = ae.GetShearCapacity();
            //double MomentCapacity = ae.GetFlexuralCapacityMajorAxis( FlexuralCompressionFiberPosition.Top);
            ////note: it is assumed that the section is symmetrical and compression fiber location does not matter

            ////this returns design capacity check if need to divide by thi TODO:
            //double VmaxF = MomentCapacity / Plate.Section.Height;

            //R = Math.Min(ShearCapacity, VmaxF);



            return R;
        }

        public double GetMaximumPlateThickness()
        {
            double tpMax = 0.0;
            double t = Hss.Section.t_des;
            double Fu = Hss.Material.UltimateStress;
            double Fyp = Plate.Material.YieldStress;

            tpMax = Fu / Fyp * t; //(K1-3)

            return tpMax;
        }
    }
}
