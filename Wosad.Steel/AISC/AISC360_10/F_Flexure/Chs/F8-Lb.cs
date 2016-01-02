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
using Wosad.Steel.AISC.AISC360_10.General.Compactness;
using Wosad.Common.Section.SectionTypes;
using Wosad.Common.Section;
 

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamChs : FlexuralMemberChsBase
    {
      public double GetLocalBucklingCapacity()
        {
            double Mn = 0.0;

            if (this.SectionPipe != null)
            {
                CompactnessClassFlexure cClass = GetCompactnessClass();
                if (cClass == CompactnessClassFlexure.Noncompact || cClass == CompactnessClassFlexure.Slender)
                {
                    double Sx = Section.Shape.SectionModulusXTop;
                    if (cClass == CompactnessClassFlexure.Noncompact)
                    {
                        //noncompact
                        Mn = (0.021 * E / (D / t) + Fy) * Sx; //(F8-2)
                    }
                    else
                    {
                        //slender
                        double Fcr = 0.33 * E / (D / t); //(F8-4)
                        Mn = Fcr * Sx;  // (F8-3)
                    }
                }
                else
                {
                    Mn = double.PositiveInfinity;
                }
            }
            return Mn;
      }
    }
}
