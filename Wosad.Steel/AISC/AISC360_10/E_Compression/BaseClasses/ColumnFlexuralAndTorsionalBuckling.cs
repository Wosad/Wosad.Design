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
using Wosad.Steel.AISC.AISC360_10.Compression.BaseClasses;
using Wosad.Steel.AISC.Code;

namespace Wosad.Steel.AISC.AISC360_10.Compression
{
    public abstract partial class ColumnFlexuralAndTorsionalBuckling: ColumnFlexuralBuckling
    {
        public ColumnFlexuralAndTorsionalBuckling(ISteelSection Section, double L_x, double L_y, double K_x, double K_y, ICalcLog CalcLog)
            : base(Section,L_x,L_y,K_x,K_y, CalcLog)
        {
            if (Section is ISectionIAssymetrical)
            {

                ISectionIAssymetrical secAsym = (ISectionIAssymetrical)Section;
                xo = secAsym.ShearCenterDistanceToCentroidX;
                yo = secAsym.ShearCenterDistanceToCentroidY;
            }
            else
            {
                xo = 0;
                yo = 0;
            }
        }

       // public abstract double GetTorsionalElasticBucklingStressFe();

        double xo, yo;

        public double GetH()
        {
            double ro2 = Get_roSquare();
            double H = 1.0 - (xo * xo + yo * yo) / ro2; //(E4-10)
            return H;
        }

        public double Get_roSquare()
        {
            double Ix = Section.SectionBase.MomentOfInertiaX;
            double Iy = Section.SectionBase.MomentOfInertiaY;
            double Ag = Section.SectionBase.Area;

            double ro2 = xo*xo+yo*yo+(Ix+Iy)/Ag; //(E4-11)

            return ro2;
        }


        internal double GetFez()
        {
            double pi2 = Math.Pow(Math.PI, 2);
            double E = Section.Material.ModulusOfElasticity;
            double Cw = Section.SectionBase.WarpingConstant;
            double Kz = EffectiveLengthFactorZ;
            double Lz = UnbracedLengthZ;
            double G = 11200; //ksi
            double J = Section.SectionBase.TorsionalConstant;
            double Ag = Section.SectionBase.Area;
            double r0 = Math.Pow(Get_roSquare(), 0.5);


            //(E4-9)
            double Fez = (pi2 * E * Cw / Math.Pow(Kz * Lz, 2) + G * J) / (Ag * r0);
            return Fez;
        }

    }
}
