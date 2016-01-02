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


namespace Wosad.Steel.AISC.AISC360_10.Compression
{
    public abstract class ColumnFlexuralBuckling: SteelColumn
    {
        public ColumnFlexuralBuckling(ISteelSection Section, double L_x, double L_y, double K_x, double K_y, ICalcLog CalcLog)
            : base(Section,L_x,L_y,K_x,K_y, CalcLog)
        {

        }

        internal double GetElasticBucklingStressFe()
        {

            double FeMinor = GetFeSingleAxis(false);
            double FeMajor = GetFeSingleAxis(true);

            return Math.Min(FeMinor, FeMajor);
        }

        protected virtual double GetSlendernessKLr(bool IsMajorAxis)
        {
            double K;
            double L;
            double r;
            if (IsMajorAxis==true)
            {
                L = UnbracedLengthX;
                K = EffectiveLengthFactorX;
                r = Section.Shape.RadiusOfGyrationX;
                
            }
            else
            {
                L = UnbracedLengthY;
                K = EffectiveLengthFactorY;
                r = Section.Shape.RadiusOfGyrationY;

            }

            double Slenderness = K * L / r;
            return Slenderness;
        }

        private double GetFeSingleAxis(bool IsMajorAxis)
        {
            double E = Section.Material.ModulusOfElasticity;
            
            double Slenderness = GetSlendernessKLr(IsMajorAxis);

            double Fe;

            if (Slenderness == 0)
            {
                Fe = double.PositiveInfinity;
            }
            else
            {
                Fe = GetFlexuralElasticBucklingStressFe(E,Slenderness);
            }

            return Fe;

        }

        private double GetFlexuralElasticBucklingStressFe(double E, double SlendernessKLr)
        {
            double Fe=0;
            double Slenderness = SlendernessKLr;

            if (Slenderness != 0)
            {
                //(E3-4)
                Fe = Math.Pow(Math.PI, 2) * E / Math.Pow(Slenderness, 2);
            }
            else
            {
                Fe = double.PositiveInfinity;
            }

            return Fe;
            
        }


        protected virtual double GetCriticalStressFcr(double Fe, double Q=1.0)
        {
            

            double Fcr = 0.0;
            double Fy = Section.Material.YieldStress;


                double stressRatio = Q * Fy / Fe;

                if (stressRatio > 2.25)
                {
                    Fcr = 0.877 * Fe; // (E3-3) if Q<1 then (E7-3)
                }
                else
                {
                    Fcr = Q * Math.Pow(0.658, stressRatio) * Fy; //(E3-2)  if Q<1 then (E7-2)
                } 


            

            return Fcr;
        }

        public double GetCriticalStressFcr_Y()
        {
            double Fcr = GetFeSingleAxis(false);
            return Fcr;
        }

        public double GetCriticalStressFcr_X()
        {
            double Fcr = GetFeSingleAxis(true);
            return Fcr;
        }

        public double GetReductionFactorQ(double Fcr)
        {
            double Qs = GetReductionFactorForUnstiffenedElementQs();
            double Qa = GetReductionFactorForStiffenedElementQa(Fcr);


            double Q = Qs * Qa; //User Note for E7
            return Q;
        }

        public abstract double GetReductionFactorForStiffenedElementQa(double Fcr);

        public abstract double GetReductionFactorForUnstiffenedElementQs();

    }
}
