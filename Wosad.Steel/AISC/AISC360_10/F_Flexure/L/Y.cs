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

 

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamAngleCompact : FlexuralMemberAngleBase
    {
        //Yielding F10.1
        public double GetYieldingMomentCapacityGeometric(FlexuralCompressionFiberPosition compressionFiberLocation)
        {
            double Mn = 0;
            double My;

            if (compressionFiberLocation== FlexuralCompressionFiberPosition.Top ||compressionFiberLocation== FlexuralCompressionFiberPosition.Bottom )
            {
                My = GetYieldingMomentGeometricXCapacity(compressionFiberLocation);
            }
            else
            {
                My = GetYieldingMomentGeometricYCapacity(compressionFiberLocation);
            }

            Mn = 1.5 * My; //(F10-1)

            return Mn;
        }

        

        private double GetYieldingMomentGeometricXCapacity(FlexuralCompressionFiberPosition compressionFiberLocation)
        {
            double Sxt = GetSectionModulusTensionSxt(compressionFiberLocation);
            double Sxc = GetSectionModulusCompressionSxc(compressionFiberLocation);
            double Sx = Math.Min(Sxc, Sxt);
            double Fy = Section.Material.YieldStress;
            double My = Sx * Fy;
            return My;
        }

        private double GetYieldingMomentGeometricYCapacity(FlexuralCompressionFiberPosition compressionFiberLocation)
        {
            double Syt = GetSectionModulusTensionSyt(compressionFiberLocation);
            double Syc = GetSectionModulusCompressionSyc(compressionFiberLocation);
            double Sy = Math.Min(Syc, Syt);
            double Fy = Section.Material.YieldStress;
            double My = Sy * Fy;
            return My;
        }


    }
}
