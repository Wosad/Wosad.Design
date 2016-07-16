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
 
using Wosad.Common.Entities;
namespace Wosad.Wood.NDS.NDS2015
{
    public partial class SawnLumberMember : WoodMember
    {
        public double GetColumnStabilityFactor( double d,
                                                double F_c,
                                                double E_min,
                                                double l_e,
                                                double C_M_Fc,
                                                double C_M_E,
                                                double C_t,
                                                double C_F,
                                                double C_i,                                                
                                                double C_T,
                                                double lambda
                                                )
        {
            this.d = d;
            this.F_c = F_c;
            this.E_min = E_min;
            this.l_e = l_e;
            this.C_M_Fc = C_M_Fc;
            this.C_M_E = C_M_E;
            this.C_t = C_t;
            this.C_F = C_F;
            this.C_i = C_i;
            this.C_T = C_T;
            this.lambda = lambda;
            double FcStar = Get_FcStar();
            double E_minPrime = GetAdjustedMinimumModulusOfElasticityForStability(E_min,C_M_E,C_t,C_i,C_T);
            C_P = base.GetC_P(FcStar, E_minPrime, l_e,d);

            return C_P;
        }

        private double Get_FcStar()
        {
            double K_F = 2.4;
            double phi = 0.9;
            double FcStar = F_c * C_M_Fc * C_t * C_F * C_i *K_F * phi * lambda; //from Table 4.3.1
            return FcStar;
        }



        /// <summary>
        /// Factor from NDS 2105 section 3.7.1.5 
        /// </summary>
        /// <returns></returns>

        protected override double Get_c()
        {
            return 0.8;
        }
    }
}
