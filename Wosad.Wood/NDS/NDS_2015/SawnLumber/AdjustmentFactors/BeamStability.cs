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

namespace Wosad.Wood.NDS.NDS_2015
{
    public partial class SawnLumberMember : WoodMember
    {


        public double GetStabilityFactor(double b, 
            double d,
            double F_b,
            double E_min,
            double l_e,
            double C_M ,
            double C_t ,
            double C_F ,
            double C_i ,
            double C_r ,
            double C_T,
            double lambda
            )
        {
            this.d  = d;
            this.F_b= F_b;
            this.E_min = E_min;
            this.l_e = l_e;
            this.C_M= C_M;
            this.C_t= C_t;
            this.C_F= C_F;
            this.C_i= C_i;
            this.C_r= C_r;
            this.C_T= C_T;
            this.lambda = lambda;
            double C_L = base.GetC_L(b, d, l_e);

            return C_L;
        }

        protected override double GetF_b_AdjustedForBeamStability()
        {
            double K_F = 2.54;
            double phi = 0.85;
            return F_b * C_M * C_t * C_F * C_i * C_r*K_F*phi*lambda; //from Table 4.3.1
            return F_b;
        }

        protected override double GetModulusOfElasticityForBeamAndColumnStability()
        {
            double K_F = 1.76;
            double phi = 0.85;
            return E_min*C_M*C_t*C_i*C_T*1.76*0.85*lambda; //from Table 4.3.1
        }
    }
}
