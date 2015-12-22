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
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;



namespace Wosad.Analytics.Steel.AISC360_10.Connections.AffectedElements
{
    public class AffectedElementInTension : AffectedElementBase
    {

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
    }
}
