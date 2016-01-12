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

using Wosad.Steel.AISC.SteelEntities.Materials;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.AISC360_10.Compression;

namespace Wosad.Steel.AISC360_10.Connections.AffectedElements
{
    public class AffectedElementInTension : AffectedElement
    {
        public AffectedElementInTension(double F_y, double F_u): base( F_y,  F_u)
        {

        }

        public AffectedElementInTension(ISteelSection Section,ICalcLog CalcLog)
            : base(Section, CalcLog)
        {

        }

        public AffectedElementInTension(ISection Section, ISteelMaterial Material, ICalcLog CalcLog)
            :base(Section,Material, CalcLog)
        {

        }

        public double GetNetArea(double A_g, double N_bolts, double d_h, double s, List<double> g, double t_p, bool IsTensionSplicePlate  )
        {
            if (IsTensionSplicePlate==false)
            {
               return A_g - N_bolts * d_h - g.Sum(gage => Math.Pow(s, 2.0) / (4.0 * gage)); 
            }
            else
            {
                return 0.85 * A_g;
            }
            
        }

        /// <summary>
        /// The available strength of affected or connecting element in tension
        /// </summary>
        /// <param name="A_g">Gross area</param>
        /// <param name="A_e">Effective net area</param>
        /// <returns></returns>
        public double GetTensileCapacity(double A_g, double A_e)
        {
            double F_y = Section.Material.YieldStress;
            double F_u = Section.Material.UltimateStress;

            double phiR_nY = GetTensionYieldingStrength(F_y, A_e);
            double phiR_nU = GetTensileRuptureStrength(F_y, A_e);

            double phiR_n = Math.Min(phiR_nY, phiR_nU);
            return phiR_n;
        }

        private double GetTensileRuptureStrength(double F_u, double A_e)
        {

            double R_n = F_u * A_e; // (J4-2)
            double phi = 0.75;
            return phi * R_n;
        }

        private double GetTensionYieldingStrength(double F_y, double A_g)
        {
            double R_n = F_y * A_g; // (J4-1)
            double phi = 1.0;
            return phi * R_n;
        }

    }
}
