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
using Wosad.Steel.AISC.Exceptions;


namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamTee : FlexuralMemberTeeBase
    {

        public double GetFlexuralTorsionalBucklingMomentCapacity(FlexuralCompressionFiberPosition compressionFiberLocation)
        {
            double Mn;
            double pi = Math.PI;
            double Iy = Section.Shape.MomentOfInertiaY;
            double G = Section.Material.ShearModulus;
            double J = sectionTee.TorsionalConstant;
            double B = GetB(compressionFiberLocation);
            double B2 = Math.Pow(B,2);

            Mn = pi * Math.Sqrt(E * Iy * G * J) / (Lb) * (B + Math.Sqrt(1.0 + B2)); //(F9-4)
            return Mn;

        }

        private double GetB(FlexuralCompressionFiberPosition compressionFiberLocation)
        {
            double B;
            double d = sectionTee.Height;
            double Iy = sectionTee.MomentOfInertiaY;
            double J = sectionTee.TorsionalConstant;

            double sign;

            switch (compressionFiberLocation)
            {
                case FlexuralCompressionFiberPosition.Top:
                    //For stems in tension
                    sign = 1;
                    break;
                case FlexuralCompressionFiberPosition.Bottom:
                    //For stems in compression
                    sign = -1;
                    break;
                default:
                    throw new CompressionFiberPositionException();
            }

            B=sign*2.3*(d/Lb)*Math.Sqrt(Iy/J); //(F9-5)

            return B;
        }


    }
}
