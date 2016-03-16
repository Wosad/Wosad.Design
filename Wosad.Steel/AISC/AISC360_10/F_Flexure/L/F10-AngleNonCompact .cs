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
 using Wosad.Common.CalculationLogger;
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities;

 

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class  BeamAngleNonCompact : BeamAngleCompact
    {

        public BeamAngleNonCompact(ISteelSection section, ICalcLog CalcLog, AngleRotation AngleRotation)
            : base(section, CalcLog, AngleRotation)
        {
            
            GetSectionValues();
        }


        public override SteelLimitStateValue GetFlexuralLegOrStemBucklingStrength(FlexuralCompressionFiberPosition CompressionLocation, 
            FlexuralAndTorsionalBracingType BracingType)
        {

            double S_c = GetSectionModulus(CompressionLocation, true, BracingType);
            //F10-7
            double  M_n=F_y*S_c*(2.43-1.72*(((b) / (t)))*Math.Sqrt(((F_y) / (E))));
            double phiM_n = 0.9*M_n;
            SteelLimitStateValue ls = new SteelLimitStateValue(phiM_n, true);
            return ls;
        }



    }
}