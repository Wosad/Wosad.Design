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

using Wosad.Steel.AISC.SteelEntities;
 using Wosad.Common.CalculationLogger;


namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamIDoublySymmetricCompact : BeamIDoublySymmetricBase, ISteelBeamFlexure
    {
        public BeamIDoublySymmetricCompact(ISteelSection section, bool IsRolledMember, ICalcLog CalcLog)
            : base(section, IsRolledMember, CalcLog)
        {
            SectionValuesWereCalculated = false;
            //GetSectionValues();
        }



        #region Limit States

        public override SteelLimitStateValue GetFlexuralYieldingStrength(FlexuralCompressionFiberPosition CompressionLocation)
        {
            SteelLimitStateValue plasticStrength = GetMajorPlasticMomentCapacity();
            double phiM_n = 0.9*plasticStrength.Value;
            return new SteelLimitStateValue(phiM_n, true);
        }

        public override SteelLimitStateValue GetFlexuralLateralTorsionalBucklingStrength(double C_b, double L_b, FlexuralCompressionFiberPosition CompressionLocation,
            FlexuralAndTorsionalBracingType BracingType, MomentAxis MomentAxis)
        {
            SteelLimitStateValue ls;
            if (BracingType == FlexuralAndTorsionalBracingType.FullLateralBracing)
            {
                ls = new SteelLimitStateValue(-1, false);
            }
            else
            {
                double phiM_n = GetFlexuralTorsionalBucklingMomentCapacity(L_b, C_b);
                ls = new SteelLimitStateValue(phiM_n, true);
            }
            return ls;
        }


        public override SteelLimitStateValue GetLimitingLengthForInelasticLTB_Lr(FlexuralCompressionFiberPosition CompressionLocation)
        {
            if (SectionValuesWereCalculated == false)
            {
                GetSectionValues();
            }
            //double rts = Getrts(Iy, Cw, Sx);
            double Lr = GetLr(rts, E, Fy, Sx, J, c, ho);  // (F2-6)
            SteelLimitStateValue ls = new SteelLimitStateValue();
            ls.IsApplicable = true;
            ls.Value = Lr;

            return ls;
        }

        public override SteelLimitStateValue GetLimitingLengthForFullYielding_Lp(FlexuralCompressionFiberPosition CompressionLocation)
        {
            if (SectionValuesWereCalculated == false)
            {
                GetSectionValues();
            }
            double Lp = GetLp(ry, E, Fy); //(F2-5)
            SteelLimitStateValue ls = new SteelLimitStateValue();
            ls.IsApplicable = true;
            ls.Value = Lp;


            return ls;

        }


        #endregion


        bool SectionValuesWereCalculated;


        internal void GetSectionValues()
        {
            if (SectionValuesWereCalculated == false)
            {
                E = Section.Material.ModulusOfElasticity;
                Fy = Section.Material.YieldStress;

                Iy = Section.Shape.I_y;

                Sxbot = Section.Shape.S_xBot;
                Sxtop = Section.Shape.S_xTop;

                Sx = Math.Min(Sxbot, Sxtop);

                Zx = Section.Shape.Z_x;

                ry = Section.Shape.r_y;

                Cw = Section.Shape.C_w;

                J = Section.Shape.J;

                c = Get_c();

                ho = Get_ho();

                rts = this.GetEffectiveRadiusOfGyration();
                SectionValuesWereCalculated = true;
            }
        }

        private double GetEffectiveRadiusOfGyration()
        {
            throw new NotImplementedException();
        }


        double E;
        double Fy;


        double Iy;

        double Sxbot;
        double Sxtop;

        double Sx;
        double Zx;
        double ry;
        double Cw;
        double J;
        double c;
        double ho;

        double rts;



    }
}
