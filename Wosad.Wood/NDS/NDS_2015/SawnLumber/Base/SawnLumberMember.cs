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
using Wosad.Common.CalculationLogger;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;

namespace Wosad.Wood.NDS.NDS2015
{
    public abstract partial class SawnLumberMember : WoodMember
    {

        public SawnLumberMember(ICalcLog CalcLog)
            : base(CalcLog)
        {

        }

        public SawnLumberMember()
            : this(new CalcLog())
        {

        }

        protected override bool DetermineIfMemberIsLaterallyBraced()
        {
            //determine lateral bracing requirements to skip stability checks
            //4.4.1 Stability of Bending Members
            throw new NotImplementedException();
        }

        double d;
        double F_b;
        double F_c;
        double E_min;
        double l_e;
        double C_M_Fb;
        double C_M_Ft;
        double C_M_Fv;
        double C_M_Fc;
        double C_M_E;
        double C_t;
        double C_F;
        double C_i;
        double C_r;
        double C_P;
        double C_b;
        double C_T;
        double lambda;
    }
}
