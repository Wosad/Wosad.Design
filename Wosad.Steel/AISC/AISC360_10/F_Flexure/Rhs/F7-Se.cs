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
using Wosad.Steel.AISC.Interfaces;



namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamRhs : FlexuralMemberRhsBase, ISteelBeamFlexure
    {
        private double GetEffectiveSectionModulusX()
        {
            double Se=0.0;
            double be = GetEffectiveFlangeWidth_beff();
            double b = GetCompressionFlangeWidth_b();
            double AOriginal = SectionTube.A;
                
            //Find I reduced
            double IOriginal = SectionTube.I_x;
            double tdes = SectionTube.t_des;
            double bRemoved = (b - be) ;
            double ADeducted = bRemoved* tdes;
            double  h = SectionTube.H;
            double yDeducted = (h-tdes)/2.0;
            double Ideducted = bRemoved * Math.Pow(tdes, 3) / 12.0;
            //Use parallel axis theorem:
            double IFinal = IOriginal -(ADeducted * Math.Pow(yDeducted, 2) + Ideducted);


            // since the section is symmetric, assume compression fiber is at the top
            // this does not matter anyways
		     double yCentroidModifiedFromBottom = h / 2 - ADeducted / AOriginal;
             double yCentroidModifiedFromTop = h / 2 + ADeducted / AOriginal;

             Se = IFinal / yCentroidModifiedFromTop;
            

            return Se;
        }

        protected virtual double GetCompressionFlangeWidth_b()
        {
            // since section is symmetrical the location of compression fiber
            // does not matter

            double b = 0;

            if (SectionTube.CornerRadiusOutside ==-1.0)
	        {
                b = SectionTube.B - 3.0 * SectionTube.t_des;
	        }
            else
            {
                b = SectionTube.B - 2.0 * SectionTube.CornerRadiusOutside;
            }
            
            return b;

        }

        protected double GetEffectiveFlangeWidth_beff()
        {
            double b = GetCompressionFlangeWidth_b();
            double tf = SectionTube.t_des;
            double be = 1.92*tf*SqrtE_Fy()*(1.0-0.38/(b/tf)*SqrtE_Fy()-0.738); //(F7-4)
            be = be > b? b :be;
            be= be<0? 0 : be;

            return be;
        }

        
    }
}
