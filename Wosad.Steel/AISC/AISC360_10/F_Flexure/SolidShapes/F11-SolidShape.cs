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
using Wosad.Steel.AISC.AISC360_10.Flexure.SolidShapes;
using Wosad.Steel.AISC.Exceptions;

using Wosad.Steel.AISC.SteelEntities;
 
 
 

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class SolidShape : FlexuralMember, ISteelBeamFlexure
    {

        public SolidShape(ISteelSection section,
            double UnbracedLength, double EffectiveLengthFactor, ICalcLog CalcLog) : 
            base(section, UnbracedLength, EffectiveLengthFactor, CalcLog)
        {
                GetSectionValues();
                sectionTee = null;
                ISectionSolid s = Section as ISectionSolid;

                if (s == null)
                {
                    throw new SectionWrongTypeException(typeof(ISectionSolid));
                }

        }

        public SolidShape(ISteelSection section, ICalcLog CalcLog)
            : base(section, 0.0, 1.0, CalcLog)
        {
            
        }

        ISectionSolid sectionTee;

        internal void GetSectionValues()
        {

            E = Section.Material.ModulusOfElasticity;
            Fy = Section.Material.YieldStress;

            L = this.UnbracedLengthFlexure;
            K = this.EffectiveLengthFactorFlexure;

            Lb = this.EffectiveLengthFactorFlexure * this.UnbracedLengthFlexure;


        }

        double Lb;
        double E;
        double Fy;

        double L;
        double K;


        //public override double GetFlexuralCapacityMajorAxis( FlexuralCompressionFiberPosition compressionFiberLocation)
        //{
        //    throw new NotImplementedException();
        //}



        #region Steel Flexural Member Limit States

        //STANDARD YIELDING LIMIT STATE IS APPLICABLE
        //public override SteelLimitStateValue GetYieldingLimitState(SharedProjects.Data.General.MomentAxis MomentAxis, SharedProjects.Data.General.CompressionLocation CompressionLocation){}

        public override SteelLimitStateValue GetFlexuralLateralTorsionalBucklingStrength(double C_b, FlexuralCompressionFiberPosition CompressionLocation)
        {
            throw new NotImplementedException();
        }

        public override SteelLimitStateValue GetFlexuralFlangeLocalBucklingStrength( FlexuralCompressionFiberPosition CompressionLocation)
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();
            ls.IsApplicable = false;
            return ls;
        }

        public override SteelLimitStateValue GetFlexuralTensionFlangeYieldingStrength(FlexuralCompressionFiberPosition CompressionLocation)
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();
            ls.IsApplicable = false;
            return ls;
        } 
        #endregion
    }
}
