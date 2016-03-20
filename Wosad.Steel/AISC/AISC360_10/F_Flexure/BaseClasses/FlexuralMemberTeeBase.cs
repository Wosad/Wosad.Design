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
using Wosad.Steel.AISC.AISC360_10.B_General;





namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public abstract partial class FlexuralMemberTeeBase : FlexuralMember
    {
        public FlexuralMemberTeeBase(ISteelSection section, ICalcLog CalcLog)
            : base(section, CalcLog)
        {
            sectionTee = null;
            this.Section = section;

            ISectionTee sTee = section as ISectionTee;

            if (sTee == null)
            {
                ISectionDoubleAngle sDL = section as ISectionDoubleAngle;
                if (sDL ==null)
                {
                    throw new Exception("Section must be of type SectionTee or SectionDoubleAngle");
                }
                
            }
            else
            {
                sectionTee = sTee;
                Compactness = GetShapeCompactness();
            }
        }

        protected virtual IShapeCompactness GetShapeCompactness()
        {
            IShapeCompactness compactness = new ShapeCompactness.TeeMember(Section);
            return compactness;

        }

        IShapeCompactness Compactness { get; set; }


        private ISectionTee sectionTee;

        public ISectionTee SectionTee
        {
            get { return sectionTee; }
            set { sectionTee = value; }
        }

        protected virtual CompactnessClassFlexure GetFlangeCompactnessClass()
        {
            return Compactness.GetFlangeCompactnessFlexure();
        }

        public virtual CompactnessClassFlexure GetStemCompactnessClass()
        {
            return Compactness.GetWebCompactnessFlexure();
        }

        protected virtual double GetLambdaStem()
        {
            if (sectionTee != null)
            {
                double lambdaStem = Compactness.GetWebLambda();
                return lambdaStem;
            }
            else
            {
                throw new SectionNullException(typeof(ISectionTee));
            }
        }

        protected virtual double GetLambdaFlange()
        {
            if (sectionTee != null)
            {
                double lambdaFlange = Compactness.GetCompressionFlangeLambda();
                return lambdaFlange;
            }
            else
            {
                throw new SectionNullException(typeof(ISectionTee));
            }
        }

        protected virtual double GetLambdapf(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            return Compactness.GetFlangeLambda_p(StressType.Flexure);
        }

        protected virtual double GetLambdarf(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            return Compactness.GetFlangeLambda_r(StressType.Flexure);
        }
    }
}
