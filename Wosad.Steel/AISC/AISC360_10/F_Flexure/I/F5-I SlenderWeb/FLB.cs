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
using Wosad.Steel.AISC.Exceptions;



namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamISlenderWeb : FlexuralMemberIBase
    {

        //Compression Flange Local Buckling F5.3
        public  double GetCompressionFlangeLocalBucklingCapacity(FlexuralCompressionFiberPosition compressionFiberPosition)
        {
            double Fcr = 0.0;

                double bf = GetCompressionFlangeWidthbfc(compressionFiberPosition);
                double tf = GetCompressionFlangeThicknesstfc(compressionFiberPosition);

                ShapeCompactness.IShapeMember compactness = new ShapeCompactness.IShapeMember(Section, IsRolledMember, compressionFiberPosition);
                CompactnessClassFlexure flangeCompactness = compactness.GetFlangeCompactnessFlexure();

                double lambda = compactness.GetCompressionFlangeLambda();
                double Sxc = GetSectionModulusCompressionSxc(compressionFiberPosition);
                Rpg = GetRpg(compressionFiberPosition);

                switch (flangeCompactness)
                {
                    case CompactnessClassFlexure.Compact:

                        throw new LimitStateNotApplicableException("Compression Flange Local buckling");

                    case CompactnessClassFlexure.Noncompact:

                        double lambdapf = compactness.GetFlangeLambda_p(StressType.Flexure);
                        double lambdarf = compactness.GetFlangeLambda_r(StressType.Flexure);

                        Fcr = Fy - (0.3 * Fy) * (lambda - lambdapf) / (lambdarf - lambdapf); //(F5-8)
                        break;
                    case CompactnessClassFlexure.Slender:

                        double kc = Getkc();
                        double E = this.Section.Material.ModulusOfElasticity;

                        Fcr = 0.9 * E * kc  / Math.Pow(lambda, 2); //(F5-9)

                        break;

                }
                double Mn = Rpg * Fcr * Sxc;

                double phiM_n = 0.9 * Mn;
                return phiM_n;
        }

    }
}
