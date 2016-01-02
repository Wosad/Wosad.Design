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
using Wosad.Steel.AISC.Exceptions;


namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamIDoublySymmetricNoncompactWeb : FlexuralMemberIBase
    {
        double Cb;
        double Lp; double Lr;
        double rt;
        double FL;
        double Sxc;

        // Lateral-Torsional Buckling F4.2
        public double GetLateralTorsionalBucklingCapacity(FlexuralCompressionFiberPosition compressionFiberPosition, double Cb)
        {
            double Mn = 0.0;

            double Rpc = GetRpc(compressionFiberPosition);
            double Myc = GetCompressionFiberYieldMomentMyc(compressionFiberPosition);
            FL = GetStressFL(compressionFiberPosition);
   
            switch (compressionFiberPosition)
            {
                case FlexuralCompressionFiberPosition.Top:
                    Sxc = Section.Shape.SectionModulusXTop;
                    break;
                case FlexuralCompressionFiberPosition.Bottom:
                    Sxc = Section.Shape.SectionModulusXBot;
                    break;
                default:
                    throw new CompressionFiberPositionException();
            }
            double rt = GetEffectiveRadiusOfGyrationrt(compressionFiberPosition);
            double ho = this.SectionI.FlangeCentroidDistance; 
            double Lr = GetLr();

            LateralTorsionalBucklingType BucklingType = GetLateralTorsionalBucklingType(Lb, Lp, Lr);

            switch (BucklingType)
            {
                case LateralTorsionalBucklingType.NotApplicable:
                    Mn = double.PositiveInfinity;
                    break;
                case LateralTorsionalBucklingType.Inelastic:
                    Mn = Cb * (Rpc * Myc - (Rpc * Myc - FL * Sxc) * ((Lb - Lp) / (Lr - Lp))); //(F4-2)
                    Mn = Mn > Rpc * Myc ? Rpc * Myc : Mn;
                    break;
                case LateralTorsionalBucklingType.Elastic:
                    double Iyc =  GetIyc(compressionFiberPosition);
                    double Iy = SectionI.MomentOfInertiaX;
                    J = Iyc / Iy <= 0.23 ? 0.0 : J;
                    double Fcr = GetFcr();
                    break;
                default:
                    break;
            }


            return Mn;
        }

        //(F4-7)
        internal double GetLp(double rt)
        {
            double Lp = 1.1 * rt * Math.Sqrt(E / Fy); //(F4-7)
            return Lp;
        }

        //(F4-8)
        internal double GetLr()
        {
            double Lr = 1.95 * rt * E / FL * Math.Sqrt((J / (Sxc * ho)) + Math.Sqrt(Math.Pow(J / (Sxc * ho), 2.0) + 6.76 * Math.Pow(FL / E, 2.0)));  // (F4-8)
            return Lr;
        }

        //(F4-5)

        internal double GetFcr()
        {
            double Fcr;
            double pi2 = Math.Pow(Math.PI, 2);
            Fcr = Cb * pi2 * E / (Math.Pow(Lb / rt, 2)) * Math.Sqrt(1.0 + 0.078 * J / (Sxc * ho) * Math.Pow(Lb / rt, 2)); //(F4-5)

            return Fcr;
        }
    }
}
