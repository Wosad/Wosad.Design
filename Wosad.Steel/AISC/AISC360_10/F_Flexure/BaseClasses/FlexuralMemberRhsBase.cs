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
using Wosad.Steel.AISC.Code;


namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public abstract partial class FlexuralMemberRhsBase : FlexuralMember
    {
        public FlexuralMemberRhsBase(ISteelSection section, 
            double UnbracedLength, double EffectiveLengthFactor, SteelDesignFormat DesignFormat, ICalcLog CalcLog)
            : base(section, UnbracedLength, EffectiveLengthFactor, DesignFormat, CalcLog)
        {
            sectionTube = null;
            ISectionTube s = Section.SectionBase as ISectionTube;

            if (s == null)
            {
                throw new SectionWrongTypeException(typeof(ISectionTube));
            }
            else
            {
                sectionTube = s;
                //compactness = new ShapeCompactness.HollowMember(section, CompressionLocation.Top);
            }
        }

        private ISectionTube sectionTube;

        public ISectionTube SectionTube
        {
            get { return sectionTube; }
            set { sectionTube = value; }
        }
        

        protected virtual double GetFlangeWidth_bf()
        {
            double b;

            if (sectionTube.CornerRadiusOutside == -1.0)
            {
                b = SectionTube.Width - 3.0 * sectionTube.DesignWallThickness;
            }
            else
            {
                b = sectionTube.Width - 2.0 * sectionTube.CornerRadiusOutside;
            }

            return b;
        }
        protected virtual double GetFlangeThickness_tf()
        {
            return sectionTube.DesignWallThickness;
        }
        protected virtual double GetWebWallHeight_h()
        {
            //B4-1b. Stiffened Elements
            double tdes = sectionTube.DesignWallThickness;
            return sectionTube.DesignWallThickness-3.0*tdes;
        }
    }
}
