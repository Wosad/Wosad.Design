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
using Wosad.Common.Entities;
using Wosad.Steel.AISC.AISC360_10.General.Compactness;



namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public abstract partial class FlexuralMemberIBase : FlexuralMember
    {

        public static double Getaw(double hc, double tw, double bfc, double tfc)
        {
            // aw = the ratio of two times the web area in compression due to application of
            // major axis bending moment alone to the area of the compression flange
            // components

            //todo: add alternative calc
            // For I-shapes with a channel cap or a cover plate attached to the compression flange are not covered

            double aw = 0.0;

            aw = hc * tw / (bfc * tfc); //(F4-12)

            return aw;
        }

        public double Getaw(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            double aw = 0.0;
            double hc = 0.0;
            double tw = 0.0;
            double bfc = 0.0;
            double tfc = 0.0;


            tw = SectionI.t_w;

            bfc = GetCompressionFlangeWidthbfc(compressionFiberPosition);
            hc = Gethc(compressionFiberPosition);
            tfc = GetCompressionFlangeThicknesstfc(compressionFiberPosition);

            aw = Getaw(hc, tw, bfc, tfc);

            return aw;
        }
    }
}
