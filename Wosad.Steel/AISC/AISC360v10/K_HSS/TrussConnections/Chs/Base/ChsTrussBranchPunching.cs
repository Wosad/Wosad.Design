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
using Wosad.Steel.AISC.Exceptions;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.Entities;
using Wosad.Steel.AISC.AISC360v10.K_HSS.TrussConnections;


namespace Wosad.Steel.AISC.AISC360v10.HSS.TrussConnections
{

    public abstract partial class ChsTrussBranchConnection : HssTrussConnection, IHssTrussBranchConnection
    {

        /// <summary>
        /// K2-1
        /// </summary>
        /// <returns></returns>
        protected virtual SteelLimitStateValue GetBranchPunchingShearStrength()
        {

            double P_n = 0.0;
            double phi = 0.90;

            P_n = 0.6 * F_y * t * Math.PI * D_b * (((1.0+ sin_theta) / (2.0 *Math.Pow(sin_theta, 2))));

            double phiP_n = phi * P_n;
            return new SteelLimitStateValue(phiP_n, true);
        }


    }

}