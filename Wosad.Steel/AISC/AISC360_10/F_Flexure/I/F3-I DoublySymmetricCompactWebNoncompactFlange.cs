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

using Wosad.Steel.AISC.Exceptions;

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{

    public partial class BeamIDoublySymmetricCompactWebNoncompactFlange : BeamIDoublySymmetricCompact
    {
        //This section applies to doubly symmetric I-shaped members bent about their major
        //axis having compact webs and noncompact or slender flanges as defined in Section
        //B4.1 for flexure.



        public BeamIDoublySymmetricCompactWebNoncompactFlange(ISteelSection section, bool IsRolled,
            double UnbracedLength, double EffectiveLengthFactor, ICalcLog CalcLog)
            : base(section,IsRolled, UnbracedLength, EffectiveLengthFactor,CalcLog)
        {
            GetSectionValues();
        }

        public  double GetFlexuralCapacityMajor()
        {
 
            GeneralFlexuralMember gm = new GeneralFlexuralMember(this.Log);
            GeneralFlexuralMember.CbData momentData = gm.GetCbData(this);
            double Cb = gm.GetCb(momentData);
            double MLtb = GetFlexuralTorsionalBucklingMomentCapacity(Cb);
            double MFlb = GetCompressionFlangeLocalBucklingCapacity();
            double Mn = Math.Min(MFlb, MLtb);
            return GetFlexuralDesignValue(Mn);
        }

        //Yielding limit state does not apply
        public double GetYieldingMomentCapacity()
        {
            throw new LimitStateNotApplicableException("Section Yielding");
        }
    }
}
