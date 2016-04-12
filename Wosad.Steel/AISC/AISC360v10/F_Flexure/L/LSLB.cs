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
using Wosad.Steel.AISC.AISC360v10.General.Compactness;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities;

 

namespace Wosad.Steel.AISC.AISC360v10.Flexure
{
    public partial class BeamAngle : FlexuralMemberAngleBase
    {

        public override SteelLimitStateValue GetFlexuralLegOrStemBucklingStrength(FlexuralCompressionFiberPosition CompressionLocation, 
            FlexuralAndTorsionalBracingType BracingType)
        {
            CompactnessClassFlexure StemCompactness = this.Compactness.GetWebCompactnessFlexure() ;
            // for compact angles this limit state is not applicable

            SteelLimitStateValue ls = null;

            if (StemCompactness == General.Compactness.CompactnessClassFlexure.Compact)
            {
                ls = new SteelLimitStateValue(-1, false);
                return ls;
            }
            else if (StemCompactness == CompactnessClassFlexure.Noncompact)
            {
                double S_c = GetSectionModulus(CompressionLocation, true, BracingType);
                //F10-7
                double M_n = F_y * S_c * (2.43 - 1.72 * (((b) / (t))) * Math.Sqrt(((F_y) / (E))));
                double phiM_n = 0.9 * M_n;
                ls = new SteelLimitStateValue(phiM_n, true);
            }
            else
            {
                double F_cr = ((0.71 * E) / (Math.Pow((((b) / (t))), 2)));
                double S_c = GetSectionModulus(CompressionLocation, true, BracingType);
                //(F10-8)
                double M_n = F_cr * S_c;
                double phiM_n = 0.9 * M_n;
                ls = new SteelLimitStateValue(phiM_n, true);
            }

            return ls;
        }

    }
}
