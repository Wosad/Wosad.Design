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
    public abstract class ColumnDoublySymmetric : ColumnFlexuralAndTorsionalBuckling
    {
        public ColumnDoublySymmetric(ISteelSection Section, double L_x, double L_y, double K_x, double K_y, ICalcLog CalcLog)
            : base(Section,L_x,L_y,K_x,K_y, CalcLog)
        {

        }

        public  double GetTorsionalElasticBucklingStressFe()
        {
            double pi2 = Math.Pow(Math.PI, 2);
            double E = Section.Material.ModulusOfElasticity;
            double Cw = Section.Shape.C_w;
            double Kz = EffectiveLengthFactorZ;
            double Lz = UnbracedLengthZ;

            //todo: change shear modulus to be the material property
            double G = Section.Material.ShearModulus; //ksi
            double J = Section.Shape.J;
            double Ix = Section.Shape.I_x;
            double Iy = Section.Shape.I_y;

            double Fe;
            if (Kz * Lz == 0)
            {
                return double.PositiveInfinity;
            }
            else
            {
                Fe = (pi2 * E * Cw / Math.Pow(Kz * Lz, 2) + G * J) * 1 / (Ix + Iy); //(E4-4)
                return Fe;
            }


        }
    }
}
