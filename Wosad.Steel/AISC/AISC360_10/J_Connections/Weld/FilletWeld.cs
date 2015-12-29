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

namespace Wosad.Steel.AISC.AISC360_10.Connections.Weld
{
    public class FilletWeld : FilletWeldBase
    {
        public FilletWeld(double F_y, double F_u, double F_EXX, double Leg, ICalcLog Log)
            : base(F_y, F_u, F_EXX, Leg,  Log)
            {

            }
        public FilletWeld(double F_y, double F_u, double F_EXX, double Leg)
            : this(F_y, F_u, F_EXX, Leg, null)
        {

        }

        public FilletWeld(double F_EXX, double Leg)
            : this(0, 0, F_EXX, Leg, null)
        {

        }


   }
}
