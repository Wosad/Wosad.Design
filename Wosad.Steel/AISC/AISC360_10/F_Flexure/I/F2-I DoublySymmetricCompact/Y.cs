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
using Wosad.Common.CalculationLogger;
using Wosad.Steel.AISC.SteelEntities;
 using Wosad.Common.CalculationLogger;

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamIDoublySymmetricCompact : BeamIDoublySymmetricBase, ISteelBeamFlexure
    {
        //Yielding F2.1
        public double GetYieldingMoment()
        {

            double Mn = GetMajorPlasticMomentCapacity().Value;
            double M = GetFlexuralDesignValue(Mn);


            return M;
       
        }


        public override SteelLimitStateValue GetMajorPlasticMomentCapacity()
        {

            SteelLimitStateValue ls = new SteelLimitStateValue();

            double Fy = this.Section.Material.YieldStress;
            double Zx = Section.Shape.Z_x;

            double M_p = Fy * Zx;

            double phiM_n = 0.9 * M_p;
            
            ls.IsApplicable = true;
            ls.Value = phiM_n;
            return ls;
            

        }
        public override SteelLimitStateValue GetMinorPlasticMomentCapacity()
        {
            double Mp = 0.0;
            SteelLimitStateValue ls = new SteelLimitStateValue();

            double Fy = this.Section.Material.YieldStress;
            double Zy = Section.Shape.Z_y;
            double M_p = Fy * Zy;
 
            ls.IsApplicable = true;
            ls.Value = M_p;
            return ls;
        }


    }
}
