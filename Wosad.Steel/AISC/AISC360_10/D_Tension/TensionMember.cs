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
using System.Threading.Tasks;
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC.SteelEntities;

namespace Wosad.Steel.AISC.AISC360_10.D_Tension
{
    public partial class TensionMember : SteelDesignElement
    {

        protected double GetDesignTensileCapacity(double YieldStress, double UltimateStress,
           double GrossArea, double EffectiveNetArea, SteelDesignFormat format)
        {
            double P1 = GetYieldingInGrossSectionStrength(YieldStress, GrossArea, DesignFormat);
            double P2 = GetYieldingInGrossSectionStrength(UltimateStress, EffectiveNetArea, DesignFormat);

            double P = Math.Min(P1, P2);

            return P;
        }

        /// <summary>
        /// Strength tensile yielding in the gross section
        /// </summary>
        /// <returns></returns>
        public double GetYieldingInGrossSectionStrength(double Fy, double Ag, SteelDesignFormat format)
        {
            double P=0.0;
            if (DesignFormat == SteelDesignFormat.LRFD)
            {
                P = 0.9 * Ag * Fy;
            }
            else
            {
                P = Ag * Fy /1.67;
            }

            return P;
        }

        /// <summary>
        /// Strength for tensile rupture in the net section
        /// </summary>
        /// <returns></returns>
        public double GetRuptureInNetSectionStrength(double Fu, double Ae, SteelDesignFormat format)
        {
            double P = 0.0;
            if (DesignFormat == SteelDesignFormat.LRFD)
            {
                P = 0.75* Ae * Fu;
            }
            else
            {
                P = Ae * Fu / 2.00;
            }

            return P;
        }
    }
}
