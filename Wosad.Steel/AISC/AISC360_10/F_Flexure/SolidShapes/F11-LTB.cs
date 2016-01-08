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
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities;
 using Wosad.Common.CalculationLogger;
 
 

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class SolidShape : FlexuralMember
    {



        public double GetLr(double d, double E, double Fy, double t)
        {
           double Lr=  ((1.9 * E * t * t) / (Fy * d));

           return Lr;
        }

        public double GetLp(double d, double E, double Fy, double t)
        {
            double L_p = ((0.08 * E * t * t) / (Fy * d));

            return L_p;
        }

        public override SteelLimitStateValue GetLimitingLengthForInelasticLTB_Lr()
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();

            if (this.Section.Shape is ISectionRectangular)
            {
                ISectionRectangular rectSection = this.Section.Shape as ISectionRectangular;
                double E = this.Section.Material.ModulusOfElasticity;
                double Fy = this.Section.Material.YieldStress;
                double d = rectSection.H;
                double t = rectSection.B;
                double Lr = GetLr(d, E, Fy, t);
                ls.Value = Lr; ls.IsApplicable = true;
                return ls;
            }
            else
            {
                ls.Value = double.PositiveInfinity; ls.IsApplicable = false;
               
            }
            return ls;
        }

        public override SteelLimitStateValue GetLimitingLengthForFullYielding_Lp()
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();

            if (this.Section.Shape is ISectionRectangular)
            {
                ISectionRectangular rectSection = this.Section.Shape as ISectionRectangular;
                double E = this.Section.Material.ModulusOfElasticity;
                double Fy = this.Section.Material.YieldStress;
                double d = rectSection.H;
                double t = rectSection.B;
                double Lp = GetLp(d, E, Fy, t);
                ls.IsApplicable = true; ls.Value = Lp;
            }
            else
            {
                ls.Value = double.PositiveInfinity; ls.IsApplicable = false;
            }
            return ls;
        }
    }
}
