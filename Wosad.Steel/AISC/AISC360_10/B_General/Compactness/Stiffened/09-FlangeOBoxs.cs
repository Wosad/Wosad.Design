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
using System.Text; using Wosad.Common.Entities; using Wosad.Common.Section.Interfaces; using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Exceptions;
using Wosad.Steel.AISC.Interfaces;
 
 
 

namespace Wosad.Steel.AISC.AISC360_10.General.Compactness
{
    public class FlangeOfBox : StiffenedElementCompactness
    {
        ISectionBox SectionBox;
        bool IsUniformThickness=false;

        public FlangeOfBox(ISteelMaterial Material, double Depth, double Thickness)
            :base(Material,Depth,Thickness)
        {
            
        }

        public FlangeOfBox(ISteelMaterial Material, ISectionBox SectionBox)
            :base(Material)
        {
            this.SectionBox = SectionBox;
            ISectionBox s = SectionBox;
            
            IsUniformThickness = s.FlangeThickness == s.WebThickness ? true : false;
            this.Thickness = s.FlangeThickness; //why need  this?
        }

        public override double GetLambda_r(StressType stress)
        {
            if (IsUniformThickness == true)
            {
                if (stress == StressType.Flexure)
                {
                    return 1.40 * SqrtE_Fy();
                }
                else
                {
                    return 1.40 * SqrtE_Fy();
                } 
            }
            else
            {
                //this is not in the code, however the code only
                //addresses the uniform thickness case
                //these are the values for the I-Beam

                if (stress == StressType.Flexure)
                {
                    return 1.0 * SqrtE_Fy();
                }
                else
                {
                    return 0.56 * SqrtE_Fy();
                }
            }
        }

        public override double GetLambda_p(StressType stress)
        {
            if (IsUniformThickness==true)
            {
                if (stress == StressType.Flexure)
                {
                    return 1.12 * SqrtE_Fy();
                }
                else
                {
                    throw new ShapeParameterNotApplicableException("Lambda_p");
                } 
            }
            else
            {
                //this is not in the code, however the code only
                //addresses the uniform thickness case
                //these are the values for the I-Beam
                if (stress == StressType.Flexure)
                {
                    return 0.38 * SqrtE_Fy();
                }
                else
                {
                    throw new ShapeParameterNotApplicableException("Lambda_p");
                }

            }

        }

    }
}
