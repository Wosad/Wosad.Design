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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC.SteelEntities;

namespace Wosad.Steel.AISC.AISC360_10.D_Tension
{
    public partial class TensionMember : SteelDesignElement
    {
        public TensionMember(SteelDesignFormat DesignFormat, ICalcLog Log): base(DesignFormat, Log)
        {

        }

        public TensionMember(SteelDesignFormat DesignFormat)
            : base(DesignFormat)
        {

        }

       /// <summary>
        /// Calculates effective net area per AISC section D3
       /// </summary>
       /// <param name="An"></param>
       /// <param name="U"></param>
       /// <param name="Ag"></param>
       /// <param name="IsBoltedSplice"></param>
       /// <returns></returns>
        public double GetEffectiveNetArea(double An, double U, double Ag, bool IsBoltedSplice)
        {
            double Ae = An * U; //D3-1
            if (IsBoltedSplice!=true)
            {
                return Ae;
            }
            else
            {
                //For bolted splice plates Ae=An =0.85Ag, according to Section J4.1
                double AeMax = 0.85 * Ag;
                Ae = Ae < AeMax ? AeMax : Ae;
                return Ae;
            }

        }
    }
}
