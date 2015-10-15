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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.CalculationLogger;


namespace Wosad.Wood.NDS.NDS_2015
{
    public partial class SawnLumberMember : WoodMember
    {

        double d;
        double F_b;
        double F_c;
        double E_min;
        double l_e;
        double C_M;
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
