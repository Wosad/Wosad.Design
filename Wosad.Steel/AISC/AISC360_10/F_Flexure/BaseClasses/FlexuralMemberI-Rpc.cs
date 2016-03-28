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
using Wosad.Steel.AISC.AISC360_10.General.Compactness;
using Wosad.Steel.AISC.AISC360_10.Flexure;



namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public abstract partial class FlexuralMemberIBase : FlexuralMember
    {

        internal double GetRpc(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            double Rpc = 0;
            double Mp = GetMajorNominalPlasticMoment();
            double Myc = GetCompressionFiberYieldMomentMyc(compressionFiberPosition);

            double hc = Gethc(compressionFiberPosition);
            double tw = this.Gettw();

            ShapeCompactness.IShapeMember compactness = new ShapeCompactness.IShapeMember(Section, IsRolledMember, compressionFiberPosition);
            double lambdaWeb = compactness.GetWebLambda();
            double lambdapw = compactness.GetWebLambda_p(StressType.Flexure);
            double lambdarw = compactness.GetWebLambda_r(StressType.Flexure);
            double Iyc = GetIyc(compressionFiberPosition);
            double Iy = Section.Shape.I_y;

            if (Iyc / Iy > 0.23)
            {

                if (hc / tw > lambdapw)
                {
                    Rpc = Mp / Myc; //(F4-9a)
                }
                else
                {
                    Rpc = Mp / Myc - (Mp / Myc - 1.0) * ((lambdaWeb - lambdapw) / (lambdarw - lambdapw));  //(F4-9b)
                    Rpc = Rpc > Mp / Myc ? Mp / Myc : Rpc;
                }
            }
            else
            {
                Rpc = 1.0; //(F4-10)
            }

            return Rpc;

        }


    }
}
