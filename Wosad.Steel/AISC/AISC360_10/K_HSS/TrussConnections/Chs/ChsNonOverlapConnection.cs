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
using Wosad.Common.Mathematics;
 
using Wosad.Common.CalculationLogger.Interfaces; 
using Wosad.Steel.AISC.Interfaces;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Code;

namespace  Wosad.Steel.AISC.AISC360_10.HSS.TrussConnections
{
    public class ChsNonOverlapConnection: ChsTrussConnection
    {
        public ChsNonOverlapConnection(HssTrussConnectionChord chord, List<HssTrussConnectionBranch> branches, ICalcLog CalcLog)
            : base(chord, branches, CalcLog)
 
        {

        }



        internal virtual double GetBranchShearYielding(HssTrussConnectionBranch branch)
        {
            double P = 0;
            double Pn;
            double theta = branch.Angle;
            double sinTheta = Math.Sin(theta.ToRadians());
            double pi = Math.PI;
            ISectionPipe section = GetBranchSection(branch);
            double Fy = branch.Section.Material.YieldStress;
            double t = section.DesignWallThickness;
            double Db = section.Diameter;

            if (Db<(Db-2.0*t))
            {
                //(K2-1)
                Pn = 0.6 * Fy * t * pi * Db * (1.0 + sinTheta / (2.0 * Math.Pow(sinTheta, 2)));

                P = Pn * 0.95;

            }
            else
            {
                P = double.PositiveInfinity;
            }

            return P;

        }
    }
}
