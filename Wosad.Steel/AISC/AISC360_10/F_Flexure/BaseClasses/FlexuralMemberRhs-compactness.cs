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
using Wosad.Steel.AISC.AISC360v10.General.Compactness;




namespace Wosad.Steel.AISC.AISC360v10.Flexure
{
    public abstract partial class FlexuralMemberRhsBase : FlexuralMember
    {
        private ShapeCompactness.HollowMember compactness;

        public ShapeCompactness.HollowMember Compactness
        {
            get { return compactness; }
            set { compactness = value; }
        }

        protected virtual double GetLambdapf(FlexuralCompressionFiberPosition compressionFiberPosition, MomentAxis MomentAxis)
        {
            compactness = new ShapeCompactness.HollowMember(this.Section, compressionFiberPosition, MomentAxis);
            return compactness.GetFlangeLambda_p(StressType.Flexure);
        }

        protected virtual double GetLambdarf(FlexuralCompressionFiberPosition compressionFiberPosition, MomentAxis MomentAxis)
        {
            compactness = new ShapeCompactness.HollowMember(this.Section, compressionFiberPosition, MomentAxis);
            return compactness.GetFlangeLambda_r(StressType.Flexure);
        }

        public double GetLambdaCompressionFlange(FlexuralCompressionFiberPosition compressionFiberPosition, MomentAxis MomentAxis)
        {
            ShapeCompactness.HollowMember compactness = new ShapeCompactness.HollowMember(this.Section, compressionFiberPosition, MomentAxis);
            return compactness.GetCompressionFlangeLambda();
        }

        public double GetLambdaWeb(MomentAxis MomentAxis)
        {
            ShapeCompactness.HollowMember compactness = new ShapeCompactness.HollowMember(this.Section, FlexuralCompressionFiberPosition.Top, MomentAxis);
            return compactness.GetWebLambda();
        }

        public CompactnessClassFlexure GetFlangeCompactness()
        {
           return compactness.GetFlangeCompactnessFlexure();
        }
    }
}
