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
using Wosad.Steel.AISC.AISC360_10;

 
 

namespace  Wosad.Steel.AISC360_10
{
    public abstract  class ColumnFlexuralBucklingNonSlender: SteelColumn
    {
        public ColumnFlexuralBucklingNonSlender(ISteelSection Section, double L_x, double L_y, double K_x, double K_y, ICalcLog CalcLog)
            : base(Section,L_x,L_y,K_x,K_y, CalcLog) //, Material)
        {
            double E       = this.Section.Material.ModulusOfElasticity;
            K_major = this.EffectiveLengthFactorY;
            K_minor = this.EffectiveLengthFactorZ;
            L_major=this.UnbracedLengthY ;
            L_minor=this.UnbracedLengthZ ;
            r_major=this.Section.Shape.r_x;
            r_minor=this.Section.Shape.r_y;

        }

        double E        ;
        double K_major  ;
        double K_minor  ;
        double L_major;
        double L_minor;
        double r_major;
        double r_minor;

        private double GetFeFlexuralBucklingNoSlenderElements(double E, double K, double L, double r)
        {

            return Math.Pow(Math.PI, 2) * E / Math.Pow(K * L / r, 2);
        }


        public override double CalculateCriticalStress()
        {
            double Fe_Major = GetFeFlexuralBucklingNoSlenderElements(E, K_major, L_major, r_major);
            double Fe_Minor = GetFeFlexuralBucklingNoSlenderElements(E, K_minor, L_minor, r_minor);
            return Math.Min(Fe_Major, Fe_Minor);
        }
    }
}
