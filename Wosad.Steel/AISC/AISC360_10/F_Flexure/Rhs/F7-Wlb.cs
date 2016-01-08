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





namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamRhs : FlexuralMemberRhsBase, ISteelBeamFlexure
    {

        public double GetWebLocalBucklingCapacity(MomentAxis MomentAxis,
            FlexuralCompressionFiberPosition CompressionLocation)
        {
            double Mn = 0.0;

           // ShapeCompactness.HollowMember compactness = new ShapeCompactness.HollowMember(Section, CompressionLocation.Top);
            Compactness = new ShapeCompactness.HollowMember(Section, CompressionLocation, MomentAxis);
            CompactnessClassFlexure cClass = Compactness.GetWebCompactnessFlexure();

            if (cClass == CompactnessClassFlexure.Compact)
            {
                return double.PositiveInfinity;
            }
            else
            {
                double Mp = GetMajorPlasticMomentCapacity().Value;
                double Sx = Math.Min(SectionTube.S_xTop, SectionTube.S_xBot);
                double h = GetWebWallHeight_h();
                double lambdaWeb = GetLambdaWeb(MomentAxis);
                Mn = Mp - (Mp - Fy * Sx) * (0.305 * lambdaWeb * Math.Sqrt(Fy / E) - 0.738); //(F7-5)
                Mn = Mn > Mp ? Mp : Mn;
            }

            return Mn;
        }

    }
}
