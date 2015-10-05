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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Concrete.ACI.Infrastructure.Entities.Section.Strains;
 

namespace Wosad.Concrete.ACI
{
    public abstract partial class ConcreteFlexuralSectionBase : ConcreteSectionBase, IConcreteFlexuralMember
    {

        public ConcreteFlexuralSectionBase(IConcreteSection Section, List<RebarPoint> LongitudinalBars, ICalcLog log, double ConvergenceToleranceStrain = 0.000002)
            : base(Section,LongitudinalBars,log)
        {
            this.ConvergenceToleranceStrain = ConvergenceToleranceStrain;
        }

        //        public SectionFlexuralAnalysisResult GetFlexuralCapacity
        //    (CompressionLocation CompressionFiberPosition, FlexuralAnalysisType AnalysisType, double ConvergenceToleranceStrain = 0.000002, bool IsSpiral =false)
        //{

        double ConvergenceToleranceStrain;

        public SectionFlexuralAnalysisResult GetNominalFlexuralCapacity
            (FlexuralCompressionFiberPosition CompressionFiberPosition, FlexuralAnalysisType AnalysisType)
        {
            //note: FlexuralAnalysisType AnalysisType is by default strain compatibility
            //this parameter is overriden for cases like prestressed beams


            //Step 1: Assume strain distribution with all bars below section centroid yielding
            LinearStrainDistribution TrialStrainDistribution =  GetInitialStrainEstimate(CompressionFiberPosition);
            SectionAnalysisResult TrialSectionResult = GetSectionResult(TrialStrainDistribution, CompressionFiberPosition, AnalysisType);
            double Mn = 0;

                //check id T and C force are equal

                //if T<>C
                if (GetAnalysisResultConverged(TrialSectionResult, ConvergenceToleranceStrain) == false)
                {
                    SectionAnalysisResult IteratedResult = null;
                    try
                    {
                        TCIterationBound bound = GetSolutionBoundaries(TrialSectionResult, TrialStrainDistribution); //make sure solution exists
                        IteratedResult = FindResultByVaryingSteelStrain(CompressionFiberPosition,bound, ConvergenceToleranceStrain);
                        double MaxSteelTensionStrain = GetMaxSteelStrain(IteratedResult.TensionRebarResults);
                        Mn = IteratedResult.Moment;
                        return new SectionFlexuralAnalysisResult(Mn, IteratedResult.StrainDistribution);
                    }
                    catch (SectionAnalysisFailedToConvergeException)
                    {
                        throw new SectionFailedToConvergeException();
                    }
                    
                }
                    //if T=C
                else
                {
                    double MaxSteelTensionStrain = GetMaxSteelStrain(TrialSectionResult.TensionRebarResults);
                    Mn = TrialSectionResult.Moment;
                    SectionFlexuralAnalysisResult result = new SectionFlexuralAnalysisResult(Mn, TrialSectionResult.StrainDistribution);
                    return result;
                }

                    
        }

        private double GetMaxSteelStrain(List<RebarPointResult> rebarResult)
        {
            if (rebarResult!=null)
            {
                return rebarResult.Max(e => e.Strain);
            }
            else
            {
                return 0.0;
            }
        }





        public SectionFlexuralAnalysisResult GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition CompressionFiberPosition)
        {
            throw new NotImplementedException();
        }
    }
}
