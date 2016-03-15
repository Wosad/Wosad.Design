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


namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamISlenderWeb : FlexuralMemberIBase
    {

        //Tension Flange Yielding F5.4
        public double GetTensionFlangeYieldingCapacity(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            double Mn = 0.0;

            double Sxc = compressionFiberPosition == FlexuralCompressionFiberPosition.Top ? Sxtop : Sxbot;
            double Sxt = compressionFiberPosition == FlexuralCompressionFiberPosition.Top ? Sxbot : Sxtop;

                if (Sxt >= Sxc)
                {
                    Mn = double.PositiveInfinity;
                }
                else
                {
                    Mn = Fy * Sxt; //(F5-10)
                }


                double phiM_n = 0.9 * Mn;
                return phiM_n;
        } 

    }
                  

                   
}
