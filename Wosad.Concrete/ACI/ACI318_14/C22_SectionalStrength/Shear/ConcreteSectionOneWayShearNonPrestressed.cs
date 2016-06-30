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
using Wosad.Common.Entities;
using Wosad.Concrete.ACI;

namespace Wosad.Concrete.ACI318_14
{
    /// <summary>
    ///  This class encpsulates sectional shear provisions per ACI.
    /// </summary>
    public partial class ConcreteSectionOneWayShearNonPrestressed : AnalyticalElement
    {
        public ConcreteSectionOneWayShearNonPrestressed(double d, IConcreteSection Section, IRebarMaterial RebarMaterial)
            : this(d, Section, RebarMaterial, 0, 0)
        {

        }

        public ConcreteSectionOneWayShearNonPrestressed(double d, IConcreteSection Section, IRebarMaterial TransverseRebarMaterial, double A_v, double s)
        {

            this.section = Section;
            this.d = d;
            this.b_w = Section.b_w;
            this.A_v = A_v;
            this.s = s;
        }

                double d; 
                double b_w; 
                double A_v; 
                double s; 
                
                double A_g; 
                double N_u;
                double rho_w;

                private IRebarMaterial rebarMaterial;

                public IRebarMaterial RebarMaterial
                {
                    get { return rebarMaterial; }
                    set { rebarMaterial = value; }
                }
                

                private IConcreteSection  section;

                public IConcreteSection  Section
                {
                    get { return section; }
                    set { section = value; }
                }
                
         public double GetConcreteShearStrength()
                {
                    return this.GetConcreteShearStrength(0, 0, 0, 0, 0, 0);
                }
         public double GetConcreteShearStrength(double A_g, double N_u,double h, double rho_w, double M_u, double V_u)
        {
            this.A_g  =A_g  ;
            this.N_u  =N_u  ;
            this.rho_w = rho_w;
            double V_c;
            double f_c = Section.Material.SpecifiedCompressiveStrength;

            double lambda = Section.Material.Lambda;

            if (N_u==0)
            {
                if (rho_w==0 || A_g ==0|| N_u==0  || M_u ==0 || V_u==0)
                {
                    V_c = 2 * lambda * Section.Material.Sqrt_f_c_prime * b_w * d; // (22.5.5.1)
                }
                else
                {
                    //use detailed formula
                    //Table 22.5.5.1
                    double V_c_a = (1.9 * lambda * Section.Material.Sqrt_f_c_prime + 2500.0 * rho_w * ((V_u * d) / (M_u))) * b_w * d;
                    double V_c_b = (1.9 * lambda * Section.Material.Sqrt_f_c_prime + 2500.0 * rho_w) * b_w * d;
                    double V_c_c = 3.5 * lambda *  Section.Material.Sqrt_f_c_prime * b_w * d;
                    List<double> V_cList = new List<double>() { V_c_a, V_c_b, V_c_c };
                    V_c = V_cList.Min();
                }
               
            }
            else
            {
                if (N_u>0) //compression
                {
                    if (rho_w ==0 || h==0 )
                    {
                        //Use simplified formula
                        V_c = 2.0 * (1 + ((N_u) / (2000.0 * A_g))) * lambda*Section.Material.Sqrt_f_c_prime * b_w * d;
                    }
                    else
                    {
                        //Table 22.5.6.1

                        double   V_c_b = 3.5 * lambda* Section.Material.Sqrt_f_c_prime * b_w * d * Math.Sqrt(1 + ((N_u) / (500.0 * A_g)));
                       
                        if (M_u-N_u*(((4.0*h-d) / (8.0)))<=0)
                        {
                            V_c = V_c_b;
                        }
                        else
                        {
                            double V_c_a = (1.9 * lambda*Section.Material.Sqrt_f_c_prime + 2500.0 * rho_w * ((V_u * d) / (M_u - N_u * (((4.0 * h - d) / (8.0)))))) * b_w * d;
                            V_c = Math.Min(V_c_a, V_c_b);
                        }
                    }
                }
                else //tension 
                {
                    V_c = 2 * (1 + ((N_u) / (500.0 * A_g))) * lambda * Section.Material.Sqrt_f_c_prime * b_w * d;  //(22.5.7.1)
                }
            }

            V_c = V_c < 0 ? 0 : V_c;
            StrengthReductionFactorFactory f = new StrengthReductionFactorFactory();
            double phi = f.Get_phi_ShearReinforced();
            return phi*V_c;
        }


        public double GetSteelShearStrength()
        {
            double f_yt = rebarMaterial.YieldStress;
            double V_s = ((A_v*f_yt * d) / (s));
            StrengthReductionFactorFactory f = new StrengthReductionFactorFactory();
            double phi = f.Get_phi_ShearReinforced();
            return phi * V_s;
        }

        public double GetTotalShearStrength(double phiV_c,double phiV_s)
        {
            double V_t1 = phiV_c+phiV_s;
            double V_t2 = GetMaximumShearStrength( phiV_c);
            return Math.Min(Math.Abs(V_t1),Math.Abs(V_t2));
        }

        public double GetMaximumShearStrength(double phiV_c)
        {
            StrengthReductionFactorFactory f = new StrengthReductionFactorFactory();
            double phi = f.Get_phi_ShearReinforced();

            //Section 22.5.1.2 
            double phiV_nMax = phiV_c+phi*8*Section.Material.Sqrt_f_c_prime*b_w*d;
            return phiV_nMax;
        }
    }
}
