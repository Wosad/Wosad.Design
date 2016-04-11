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
using Wosad.Steel.AISC.SteelEntities;



namespace Wosad.Steel.AISC.AISC360v10.Flexure
{
    public partial class BeamTee : FlexuralMemberTeeBase
    {
        public BeamTee(ISteelSection section, ICalcLog CalcLog)
            : base(section, CalcLog)
        {

            if (section is ISectionTee )
            {
                    SectionTee = section as ISectionTee;

            }
            else
            {
                throw new SectionWrongTypeException(typeof(ISectionTee));
            }
            GetSectionValues();
        }

        ISectionTee SectionTee;
        ISectionDoubleAngle SectionDoubleAngle;



        #region Limit States

        public virtual SteelLimitStateValue GetFlexuralYieldingStrength(FlexuralCompressionFiberPosition CompressionLocation)
        {
            double phiM_n = GetYieldingMomentCapacity(CompressionLocation);
            SteelLimitStateValue ls = new SteelLimitStateValue(phiM_n, true);
            return ls;
        }

        public virtual SteelLimitStateValue GetFlexuralLateralTorsionalBucklingStrength(double C_b, double L_b, FlexuralCompressionFiberPosition CompressionLocation,
            FlexuralAndTorsionalBracingType BracingType, MomentAxis MomentAxis)
        {
            double phiM_n = GetFlexuralTorsionalBucklingMomentCapacity(CompressionLocation, L_b);
            SteelLimitStateValue ls = new SteelLimitStateValue(-1, false);
            return ls;
        }

        public virtual SteelLimitStateValue GetFlexuralFlangeLocalBucklingStrength(FlexuralCompressionFiberPosition CompressionLocation)
        {
            SteelLimitStateValue ls = new SteelLimitStateValue(-1, false);
            return ls;
        }



        public virtual SteelLimitStateValue GetFlexuralLegOrStemBucklingStrength(FlexuralCompressionFiberPosition CompressionLocation,
            FlexuralAndTorsionalBracingType BracingType)
        {
            SteelLimitStateValue ls = new SteelLimitStateValue(-1, false);
            return ls;
        }

        public virtual SteelLimitStateValue GetLimitingLengthForInelasticLTB_Lr(FlexuralCompressionFiberPosition CompressionLocation)
        {
            SteelLimitStateValue ls = new SteelLimitStateValue(-1, false);
            return ls;
        }

        public virtual SteelLimitStateValue GetLimitingLengthForFullYielding_Lp(FlexuralCompressionFiberPosition CompressionLocation)
        {
            SteelLimitStateValue ls = new SteelLimitStateValue(-1, false);
            return ls;
        }


        #endregion



        internal void GetSectionValues()
        {

            E = Section.Material.ModulusOfElasticity;
            Fy = Section.Material.YieldStress;

        }


        double E;
        double Fy;



    }
}
