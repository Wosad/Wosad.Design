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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.Exceptions;
using Wosad.Steel.AISC.SteelEntities;
using Wosad.Steel.AISC.SteelEntities.Sections;
using Wosad.Steel.AISC.Entities;

namespace Wosad.Steel.AISC.AISC360v10.HSS.TrussConnections
{
    public partial class ChsTrussXConnection : ChsTrussBranchConnection
    {
        public ChsTrussXConnection(SteelChsSection Chord, SteelChsSection MainBranch, double thetaMain, AxialForceType ForceTypeMain, 
        SteelRhsSection SecondBranch, double thetaSecond, AxialForceType ForceTypeSecond, bool IsTensionChord,
        double P_uChord, double M_uChord): base( Chord,  MainBranch,  thetaMain,  ForceTypeMain, 
        SecondBranch,  thetaSecond,  ForceTypeSecond,  IsTensionChord,
        P_uChord,  M_uChord)
        {
        }
        /// <summary>
        /// K2-3
        /// </summary>
        /// <returns></returns>
        public override SteelLimitStateValue GetChordWallPlastificationStrength(bool IsMainBranch)
        {

            double P_n = 0.0;
            double phi = 0.90;


            throw new NotImplementedException();

            double phiP_n = phi * P_n;
            return new SteelLimitStateValue(phiP_n, true);
        }

        protected override SteelChsSection getBranch()
        {
            return this.MainBranch;
        }

    }
}
