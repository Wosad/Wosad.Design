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
using Wosad.Common.CalculationLogger;

using Wosad.Steel.AISC.AISC360_10.General.Compactness;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.Exceptions;



namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamRhs : FlexuralMemberRhsBase, ISteelBeamFlexure
    {
        ICalcLog CalcLog;

        public BeamRhs(ISteelSection section,
            double UnbracedLength, double EffectiveLengthFactor, ICalcLog CalcLog)
            : base(section, UnbracedLength, EffectiveLengthFactor,CalcLog)
        {

            GetSectionValues();
        }

        public BeamRhs(ISteelSection section,ICalcLog CalcLog) :
            this(section,0.0,1.0,CalcLog)
        {

        }

        public CompactnessClassFlexure FlangeCompactness { get; set; }
        public CompactnessClassFlexure WebCompactness { get; set; }

//This section applies to square and rectangular HSS, and doubly symmetric boxshaped
//members bent about either axis, having compact or noncompact webs and
//compact, noncompact or slender flanges as defined in Section B4.1 for flexure.

        public override double GetFlexuralCapacityMajorAxis(FlexuralCompressionFiberPosition compressionFiberLocation)
        {
            MomentAxis MomentAxis = MomentAxis.XAxis;
            double MY = GetMajorPlasticMomentCapacity().Value;
            double MFfb = GetCompressionFlangeLocalBucklingCapacity(MomentAxis, compressionFiberLocation);
            double MWlb = GetWebLocalBucklingCapacity(MomentAxis, compressionFiberLocation);
            double[] limitStates = new double[3] { MY, MFfb, MWlb };
            double Mn = limitStates.Min();
            double Mr = GetFlexuralDesignValue(Mn);
            return Mr;
        }

        public override double GetFlexuralCapacityMinorAxis(FlexuralCompressionFiberPosition compressionFiberLocation = FlexuralCompressionFiberPosition.Top)
        {
            ISectionTube cloneWeakAxisTube = SectionTube.GetWeakAxisClone() as ISectionTube;
            ISteelMaterial steelMaterial = Section.Material;
            SteelRhsSection rhsWeakAxisSection = new SteelRhsSection(cloneWeakAxisTube, steelMaterial);
            BeamRhs WeakAxisBeam = new BeamRhs(rhsWeakAxisSection, 0, 1, CalcLog); // unbraced length does not matter for tubes (per AISC)

            FlexuralCompressionFiberPosition modifiedCompressionFiberPosition; 
            switch (compressionFiberLocation)
	        {
                case FlexuralCompressionFiberPosition.Left:
                    modifiedCompressionFiberPosition = FlexuralCompressionFiberPosition.Top;
                break;
                case FlexuralCompressionFiberPosition.Right:
                modifiedCompressionFiberPosition = FlexuralCompressionFiberPosition.Bottom;
                break;
                default:
                    throw new CompressionFiberPositionException();
	        }
            return WeakAxisBeam.GetFlexuralCapacityMajorAxis(modifiedCompressionFiberPosition);
        }

        //Yielding F7.1
        public  override SteelLimitStateValue GetMajorPlasticMomentCapacity()
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();
            double Mp = 0.0;


            double Fy = this.Section.Material.YieldStress;
            double Zx = Section.Shape.Z_x;

            double M = Fy * Zx;
            Mp = M / 12.0;

            
            #region Mp
            ICalcLogEntry MpEntry = new CalcLogEntry();
            MpEntry.ValueName = "Mp";
            MpEntry.AddDependencyValue("Fy", Math.Round(Fy, 3));
            MpEntry.AddDependencyValue("Zx", Math.Round(Zx, 3));
            MpEntry.AddDependencyValue("M", Math.Round(M, 3));
            MpEntry.Reference = "";
            MpEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F7_MpStrong.docx";
            MpEntry.FormulaID = null; //reference to formula from code
            MpEntry.VariableValue = Math.Round(Mp, 3).ToString();
            #endregion
            this.AddToLog(MpEntry);

            ls.IsApplicable = true;
            ls.Value = Mp;
            return ls;
        }

        public override SteelLimitStateValue GetMinorPlasticMomentCapacity()
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();
            double Mp = 0.0;


            double Fy = this.Section.Material.YieldStress;
            double Zy = Section.Shape.Z_y;

            Mp = Fy * Zy;
            double M = Mp / 1000.0;


            #region Mp
            ICalcLogEntry MpEntry = new CalcLogEntry();
            MpEntry.ValueName = "Mp";
            MpEntry.AddDependencyValue("Fy", Math.Round(Fy, 3));
            MpEntry.AddDependencyValue("Zy", Math.Round(Zy, 3));
            MpEntry.AddDependencyValue("M", Math.Round(M, 3));
            MpEntry.Reference = "";
            MpEntry.DescriptionReference = "/Templates/Steel/AISC360_10/Flexure/F7_MpWeak.docx";
            MpEntry.FormulaID = null; //reference to formula from code
            MpEntry.VariableValue = Math.Round(Mp, 3).ToString();
            #endregion
            this.AddToLog(MpEntry);

            ls.IsApplicable = true;
            ls.Value = Mp;
            return ls;
        }

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

    }
}
