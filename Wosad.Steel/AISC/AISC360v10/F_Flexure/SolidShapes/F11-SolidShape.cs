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
using Wosad.Steel.AISC.AISC360v10.Flexure;
using Wosad.Steel.AISC.Exceptions;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Common.Exceptions;
using Wosad.Steel.AISC.Steel.Entities;
 
 
 

namespace Wosad.Steel.AISC.AISC360v10.Flexure
{
    public partial class BeamSolid : FlexuralMember, ISteelBeamFlexure
    {

        public BeamSolid(ISteelSection section, ICalcLog CalcLog, MomentAxis MomentAxis) : 
            base(section, CalcLog)
        {
            this.MomentAxis = MomentAxis;
                GetSectionValues();
                sectionSolid = null;
                ISolidShape s = Section.Shape as ISolidShape;

                if (s == null)
                {
                    throw new SectionWrongTypeException(typeof(ISolidShape));
                }

        }


        ISolidShape sectionSolid;
        MomentAxis MomentAxis;

        internal void GetSectionValues()
        {

            E = Section.Material.ModulusOfElasticity;
            F_y = Section.Material.YieldStress;


        }

        double E;
        double F_y;


        double get_d( )
        {
            double d = 0;
            ISectionRectangular rect = sectionSolid as ISectionRectangular;
            if (MomentAxis == Common.Entities.MomentAxis.XAxis)
            {
                if (rect!=null)
                {
                    d = rect.H;
                }

            }
            else if (MomentAxis ==  Common.Entities.MomentAxis.YAxis)
            {
                if (rect != null)
                {
                    d = rect.B;
                }

            }
            else
            {
                throw new FlexuralBendingAxisException();
            }
            return d;
        }

        double get_t()
        {
            double t = 0;
            ISectionRectangular rect = sectionSolid as ISectionRectangular;
            if (MomentAxis == Common.Entities.MomentAxis.XAxis)
            {
                if (rect != null)
                {
                    t = rect.B;
                }

            }
            else if (MomentAxis == Common.Entities.MomentAxis.YAxis)
            {
                if (rect != null)
                {
                    t = rect.H;
                }

            }
            else
            {
                throw new FlexuralBendingAxisException();
            }
            return t;
        }

        #region Steel Flexural Member Limit States


        public override SteelLimitStateValue GetFlexuralLateralTorsionalBucklingStrength(double C_b, double L_b, FlexuralCompressionFiberPosition CompressionLocation,
            FlexuralAndTorsionalBracingType BracingType)
        {
            SteelLimitStateValue ls = null;
            double b, h;

            if (sectionSolid is ISectionRound)
            {
                ls = new SteelLimitStateValue();
                ls.IsApplicable = false;
                ls.Value = -1;
                return ls;
            }
            else if (sectionSolid is ISectionRectangular)
            {
                ISectionRectangular recangularShape = sectionSolid as ISectionRectangular;

                if (MomentAxis == Common.Entities.MomentAxis.XAxis)
                {
                    
                    b = recangularShape.B ;
                    h = recangularShape.H;


                }
                else if (MomentAxis == Common.Entities.MomentAxis.YAxis)
                {
                    b = recangularShape.H;
                    h = recangularShape.B;
                }

                else
                {
                    throw new FlexuralBendingAxisException();
                }

                
                if (b >= h)
                {
                    ls.IsApplicable = false;
                    ls.Value = -1;
                }
                else
                {
                    ls = GetLateralTorsionalBucklingStrength(L_b, C_b);
                }
            }
            else
            {
                throw new ShapeTypeNotSupportedException(" flexural calculation of solid-shape beam");
            }
            return ls;
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


        public override SteelLimitStateValue GetLimitingLengthForInelasticLTB_Lr(FlexuralCompressionFiberPosition CompressionLocation)
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();

            if (this.Section.Shape is ISectionRectangular)
            {
                ISectionRectangular rectSection = this.Section.Shape as ISectionRectangular;
                double E = this.Section.Material.ModulusOfElasticity;
                double Fy = this.Section.Material.YieldStress;
                double d = rectSection.H;
                double t = rectSection.B;
                double Lr = GetL_r();
                ls.Value = Lr; ls.IsApplicable = true;
                return ls;
            }
            else
            {
                ls.Value = double.PositiveInfinity; ls.IsApplicable = false;

            }
            return ls;
        }

        public override SteelLimitStateValue GetLimitingLengthForFullYielding_Lp(FlexuralCompressionFiberPosition CompressionLocation)
        {
            SteelLimitStateValue ls = new SteelLimitStateValue();

            if (this.Section.Shape is ISectionRectangular)
            {
                ISectionRectangular rectSection = this.Section.Shape as ISectionRectangular;
                double E = this.Section.Material.ModulusOfElasticity;
                double Fy = this.Section.Material.YieldStress;
                double d = rectSection.H;
                double t = rectSection.B;
                double Lp = GetL_p();
                ls.IsApplicable = true; ls.Value = Lp;
            }
            else
            {
                ls.Value = double.PositiveInfinity; ls.IsApplicable = false;
            }
            return ls;
        }



        #endregion
    }
}
