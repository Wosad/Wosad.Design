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
using Wosad.Steel.AISC.AISC360_10.General.Compactness;


namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamISlenderWeb : FlexuralMemberIBase
    {

        double Cb;
        double Lp; double Lr;
        double rt;
        double Rpg;

        // Lateral-Torsional Buckling F5.2
        public double GetLateralTorsionalBucklingCapacity(FlexuralCompressionFiberPosition compressionFiberPosition, double Cb)
        {
            double Mn = 0.0;
            rt = GetEffectiveRadiusOfGyrationrt(compressionFiberPosition);
            Lp = GetLp();
            Lr = GetLr();
            Rpg = GetRpg(compressionFiberPosition);
            double Sxc = compressionFiberPosition == FlexuralCompressionFiberPosition.Top ? Sxtop : Sxbot;


            LateralTorsionalBucklingType BucklingType = GetLateralTorsionalBucklingType(Lb, Lp, Lr);
            double Fcr = 0.0;

            switch (BucklingType)
            {
                case LateralTorsionalBucklingType.NotApplicable:
                    Mn = double.PositiveInfinity;
                    break;
                case LateralTorsionalBucklingType.Inelastic:
                    Fcr = GetFcrLateralTorsionalBucklingInelastic();

                    break;
                case LateralTorsionalBucklingType.Elastic:
                    Fcr = GetFcrLateralTorsionalBucklingElastic();

                    break;

            }
            if (Mn != double.PositiveInfinity)
            {

                Mn = Rpg * Fcr * Sxc; //(F5-2)
            }
            return Mn;

        }


        public  double GetLr()
        {
            double pi = Math.PI;
            double Lr = pi * rt * Math.Sqrt(E / 0.7 * Fy); //(F5-5)
            return Lr;
        }

        //(F4-7)
        internal  double GetLp()
        {
            double Lp = 1.1 * rt * Math.Sqrt(E / Fy); //(F4-7)
            return Lp;
        }

        public  double GetFcrLateralTorsionalBucklingInelastic()
        {
            double Fcr = 0.0;

            Fcr = Cb * (Fy - (0.3 * Fy) * ((Lb - Lp) / (Lr - Lp))); //(F5-3)
            Fcr = Fcr > Fy ? Fy : Fcr;
            return Fcr;
        }

        public  double GetFcrLateralTorsionalBucklingElastic()
        {
            double Fcr = 0.0;
            double pi2 = Math.Pow(Math.PI, 2);
            Fcr = (Cb * pi2 * E) / Math.Pow(Lb / rt, 2); //(F5-4)
            Fcr = Fcr > Fy ? Fy : Fcr;
            return Fcr;
        }

    }
}
