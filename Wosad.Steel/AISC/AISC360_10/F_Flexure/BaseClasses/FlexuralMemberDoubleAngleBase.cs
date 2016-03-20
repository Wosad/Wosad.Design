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
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Exceptions;
using Wosad.Common.Section.SectionTypes;
using Wosad.Common.Section.Predefined;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Common;





namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public abstract partial class FlexuralMemberDoubleAngleBase : FlexuralMember
    {

        public ISectionAngle Angle { get; set; }
        AngleOrientation AngleOrientation { get; set; }

        public FlexuralMemberDoubleAngleBase(ISteelSection section, ICalcLog CalcLog, AngleOrientation AngleOrientation)
            : base(section, CalcLog)
        {
            sectionDoubleAngle = null;
            ISectionDoubleAngle s = Section as ISectionDoubleAngle;
            this.AngleOrientation = AngleOrientation;

            if (s == null)
            {
                throw new SectionWrongTypeException(typeof(ISectionDoubleAngle));
            }
            else
            {
                sectionDoubleAngle = s;
                Angle = s.Angle;
                compactness = new ShapeCompactness.AngleMember(Angle, section.Material, AngleOrientation);
            }
        }


        ShapeCompactness.AngleMember compactness;

        private ISectionDoubleAngle sectionDoubleAngle;

        public ISectionDoubleAngle ISectionDoubleAngle
        {
            get { return sectionDoubleAngle; }
            set { sectionDoubleAngle = value; }
        }

        protected virtual CompactnessClassFlexure GetFlangeCompactnessClass()
        {
            if (Angle!=null)
            {
                //if (Angle.AngleOrientation== LongLegVertical)
                //{
                //return compactness.HorizontalLegCompactness.GetCompactnessFlexure();
                //}
                //else
                //{
                //return compactness.VerticalLegCompactness.GetCompactnessFlexure();
                //}
            }
            throw new NotImplementedException();
        }

        public virtual CompactnessClassFlexure GetStemCompactnessClass()
        {
           // return compactness.GetWebCompactnessFlexure();
            throw new NotImplementedException();
        }

        protected virtual double GetLambdaStem()
        {
            if (sectionDoubleAngle != null)
            {
                //double lambdaStem = compactness.GetWebLambda();
                //return lambdaStem;
                throw new NotImplementedException();
            }
            else
            {
                throw new SectionNullException(typeof(ISectionTee));
            }
        }

        protected virtual double GetLambdaFlange()
        {
            if (sectionDoubleAngle != null)
            {
                //double lambdaFlange = compactness.GetCompressionFlangeLambda();
                //return lambdaFlange;
                throw new NotImplementedException();
            }
            else
            {
                throw new SectionNullException(typeof(ISectionTee));
            }
        }

        protected virtual double GetLambdapf(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            //return compactness.GetFlangeLambda_p(StressType.Flexure);
            throw new NotImplementedException();
        }

        protected virtual double GetLambdarf(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            //return compactness.GetFlangeLambda_r(StressType.Flexure);
            throw new NotImplementedException();
        }
    }
}
