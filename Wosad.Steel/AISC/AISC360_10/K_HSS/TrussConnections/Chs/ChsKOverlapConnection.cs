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


namespace  Wosad.Steel.AISC.AISC360_10.HSS.TrussConnections
{
    public class ChsKOverlapConnection : ChsKConnection
    {
        public ChsKOverlapConnection(HssTrussConnectionChord chord, double Gap, List<HssTrussConnectionBranch> branches,  ICalcLog CalcLog)
            : base(chord, Gap, branches, CalcLog)
        {

        }

        public void CalculateAvailableStrengthForBranches()
        {
            foreach (var loadCase in LoadCases)
            {

                double Pro = 0.0, Mro = 0.0;
                double U = GetUtilizationRatio(loadCase.Value, out Pro, out Mro);
                double D = GetChordDiameter();
                bool ChordIsInTension = DetermineIfChordIsInTension();
                double Qf = GetChordStressInteractionFactorQf(ChordIsInTension, Pro, Mro);
                double Qg = GetConnectionFactorQg();

                //check limit states for branches
                double Pcomp = GetCompressionBranchCapacity(loadCase.Value, D, Qg, Qf);
                double Ptens = GetTensionBranchCapacity(loadCase.Value, D, Qg, Qf);
            }
        }

        internal double GetCompressionBranchCapacity(HssKConnectionLoadCaseData loadCase, double D, double Qg, double Qf)
        {
            HssTrussConnectionBranch compressionBranch = loadCase.CompressionBranch;
            double P_ChordPlastification = CheckChordPlastificationForCompressionBranch(loadCase.LoadCaseName, D, Qf, Qg);
            double P = Math.Abs(P_ChordPlastification);
            //add capacity to branch info here
            compressionBranch.AddStrengthValue(P, loadCase.LoadCaseName);
            return P;
        }

        internal double GetTensionBranchCapacity(HssKConnectionLoadCaseData loadCase, double D, double Qg, double Qf)
        {
            HssTrussConnectionBranch tensionBranch = loadCase.TensionBranch;
            double P_ChordPlastification = CheckChordPlastificationForTensionBranch(loadCase.LoadCaseName, D, Qf, Qg);
            double P =Math.Abs(P_ChordPlastification);
            //add capacity to branch info
            tensionBranch.AddStrengthValue(P, loadCase.LoadCaseName);
            return P;
        }

        internal override double GetBranchShearYielding(HssTrussConnectionBranch branch)
        {
            //this limit state does not apply to overlap connections
            return double.PositiveInfinity;
        }
    }
}
