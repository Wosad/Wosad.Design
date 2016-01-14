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
using Wosad.Common.Mathematics;
using Wosad.Steel.AISC;

namespace Wosad.Steel.AISC360_10.Connections.AffectedElements
{
    public partial class AffectedElement
    {
        /// <summary>
        /// Determines if gusset plate corner has a compact configuration. 
        /// Refer to AISC Design Guide 29 Appendix C for
        /// additional information
        /// </summary>
        /// <param name="t_g">Gusset plate thickness</param>
        /// <param name="c_Gusset"> Shortest distance between closest bolt and beam flange</param>
        /// <param name="F_y">Yield  stress of gusset</param>
        /// <param name="E">Modulus of elasticity</param>
        /// <param name="l_1">Gusset plate distance from beam to nearest row of bolts</param>
        /// <returns></returns>
        public bool IsGussetPlateConfigurationCompact(double t_g,double c_Gusset,double F_y,double E,double l_1)
        {
            double t_beta = Get_t_beta(F_y,c_Gusset,E,l_1);
            if (t_beta<=t_g)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private double Get_t_beta(double F_y, double c_Gusset, double E, double l_1)
        {
            double t_beta;
            if (E*l_1!=0)
            {
                t_beta = 1.5 * Math.Sqrt(F_y * Math.Pow(c_Gusset, 3) / (E * l_1));
            }
            else
            {
                t_beta = double.PositiveInfinity;
            }
           
            return t_beta;
        }
    }
}
