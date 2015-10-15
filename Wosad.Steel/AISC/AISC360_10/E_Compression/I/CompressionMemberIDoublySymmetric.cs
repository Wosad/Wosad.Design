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
using Wosad.Steel.AISC.Code;

namespace Wosad.Steel.AISC.AISC360_10.Compression
{
    public partial class CompressionMemberIDoublySymmetric : ColumnDoublySymmetric
    {
        public bool IsRolled { get; set; }

        public override double CalculateDesignCapacity()
        {
            double Pr = 0.0;
            double Fcr = CalculateCriticalStress();
            Pr = GetDesignAxialCapacity(Fcr);
            return Pr;
        }
        //this method is overriden for members with slender elements
        public override double CalculateCriticalStress()
        {
            double Fcr = 0.0;

            //Flexural

            double FeFlexuralBuckling = GetElasticBucklingStressFe(); //this does not apply to unsymmetric sections
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

        public CompressionMemberIDoublySymmetric(ISteelSection Section, bool IsRolled, ICalcLog CalcLog)
            : base(Section, CalcLog)
        {
            if (Section.SectionBase is ISectionI)
            {
            SectionI = Section.SectionBase as ISectionI;
            }
            else
            {
                throw new Exception("Section of wrong type: Need ISectionI");
            }

            this.IsRolled = IsRolled;
        }



        ISectionI SectionI; // update to Interface
    }
}
