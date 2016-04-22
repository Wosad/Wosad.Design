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
using Wosad.Steel.AISC.SteelEntities;


namespace Wosad.Steel.AISC.AISC360v10.Compression
{
    public partial class IShapeCompact : ColumnDoublySymmetric
    {
        public bool IsRolled { get; set; }




        public override double CalculateCriticalStress()
        {
            double Fcr = 0.0;

            //Flexural

            double FeFlexuralBuckling = GetFlexuralElasticBucklingStressFe(); //this does not apply to unsymmetric sections
            double FcrFlexuralBuckling = GetCriticalStressFcr(FeFlexuralBuckling, 1.0);
            double Qflex = GetReductionFactorQ(FcrFlexuralBuckling);
            double FcrFlex = GetCriticalStressFcr(FeFlexuralBuckling, Qflex);
            
            double FeTorsionalBuckling = GetTorsionalElasticBucklingStressFe();
            double FcrTorsionalBuckling = GetCriticalStressFcr(FeTorsionalBuckling, 1.0);
            double Qtors = GetReductionFactorQ(FcrTorsionalBuckling);
            double FcrTors = GetCriticalStressFcr(FeTorsionalBuckling, Qtors);


            Fcr = Math.Min(FcrFlex, FcrTors);
            return Fcr;

        }

        public IShapeCompact(ISteelSection Section, bool IsRolled, double L_x, double L_y, double L_z, ICalcLog CalcLog)
            : base(Section,L_x,L_y, L_z, CalcLog)
        {
            if (Section.Shape is ISectionI)
            {
            SectionI = Section.Shape as ISectionI;
            }
            else
            {
                throw new Exception("Section of wrong type: Need ISectionI");
            }

            this.IsRolled = IsRolled;
        }



        protected ISectionI SectionI;

        public override SteelLimitStateValue GetFlexuralBucklingStrength()
        {
            
            double FeFlexuralBuckling = GetFlexuralElasticBucklingStressFe(); //this does not apply to unsymmetric sections
            double FcrFlexuralBuckling = GetCriticalStressFcr(FeFlexuralBuckling, 1.0);
            double Qflex = GetReductionFactorQ(FcrFlexuralBuckling);
            double FcrFlex = GetCriticalStressFcr(FeFlexuralBuckling, Qflex);

            double phiP_n = GetDesignAxialStrength(FcrFlex);

            SteelLimitStateValue ls = new SteelLimitStateValue(phiP_n, true);
            return ls;
        }

        public override SteelLimitStateValue GetTorsionalAndFlexuralTorsionalBucklingStrength()
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();

            if (this.UnbracedLengthZ <= UnbracedLengthX && this.UnbracedLengthZ <= UnbracedLengthY)
            {
                ls.Value = -1;
                ls.IsApplicable = false;
            }
            else
            {
                double FeTorsionalBuckling = GetTorsionalElasticBucklingStressFe();
                double FcrTorsionalBuckling = GetCriticalStressFcr(FeTorsionalBuckling, 1.0);
                double Qtors = GetReductionFactorQ(FcrTorsionalBuckling);
                double FcrTors = GetCriticalStressFcr(FeTorsionalBuckling, Qtors);

                double phiP_n = GetDesignAxialStrength(FcrTors);
                ls.Value = phiP_n;
                ls.IsApplicable = true;

            }
            return ls;

        }
    }
}
