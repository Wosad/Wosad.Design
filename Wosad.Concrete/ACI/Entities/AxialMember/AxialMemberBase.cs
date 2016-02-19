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
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;
using Wosad.Common.Mathematics;
using Wosad.Common.Section.Interfaces;
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Concrete.ACI.Infrastructure.Entities.Section.Strains;
 

namespace Wosad.Concrete.ACI
{
    public class AxialMemberBase: ConcreteFlexuralSectionBase
    {

        public AxialMemberBase(IConcreteSectionRectangular Section,
            List<RebarPoint> LongitudinalBars, ICalcLog log)
            : base(Section, LongitudinalBars, log)
        {

        }

        public List<SectionAnalysisResult> GetInteractionDiagram(double AngleOfNeutralAxis,
            FlexuralCompressionFiberPosition CompressionFiberPosition )
        {
            //Assume number of iteration points as 30
            int NumberOfConcreteIterationPoints = 30;

            List<SectionAnalysisResult> result = null;
            double ecu = Section.Material.UltimateCompressiveStrain;
            double esu = CalculateMaximumSteelStrain(CompressionFiberPosition);

            double StrainDelta, StrainIncrement;

            double StrainDistributionHeight = GetStrainDistributionHeight(CompressionFiberPosition);

            double CompressionFaceStrain,TensionFaceStrain,topFiberStrain,botFiberStrain;


            //Loop 1: upper loop => from 0.003 to steel 
            for (int i = 0; i <= NumberOfConcreteIterationPoints; i++)
            {
                StrainDelta = ecu + esu;
                StrainIncrement = StrainDelta / NumberOfConcreteIterationPoints;

                CompressionFaceStrain = ecu;
                TensionFaceStrain = ecu - StrainIncrement;


                topFiberStrain = CompressionFiberPosition == FlexuralCompressionFiberPosition.Top ? CompressionFaceStrain : TensionFaceStrain;
                botFiberStrain = CompressionFiberPosition == FlexuralCompressionFiberPosition.Top ? TensionFaceStrain : CompressionFaceStrain;

                LinearStrainDistribution sd = new LinearStrainDistribution(StrainDistributionHeight, topFiberStrain, botFiberStrain);
                SectionAnalysisResult resultPoint = GetSectionResult(sd, CompressionFiberPosition);

                if (resultPoint !=null)
                {
                    if (result == null)
                    {
                        result = new List<SectionAnalysisResult>();
                    }
                    result.Add(resultPoint);
                }
            }
            //Loop 2: points between last point of loop  and zero concrete compression
            int NumberOfSteelIterationPoints = 4;
            StrainDelta = ecu / NumberOfSteelIterationPoints;

            for (int i = 1; i <= NumberOfSteelIterationPoints; i++)
            {
                CompressionFaceStrain = ecu - i*StrainDelta;
                TensionFaceStrain = esu;

                topFiberStrain = CompressionFiberPosition == FlexuralCompressionFiberPosition.Top ? CompressionFaceStrain : TensionFaceStrain;
                botFiberStrain = CompressionFiberPosition == FlexuralCompressionFiberPosition.Top ? TensionFaceStrain : CompressionFaceStrain;

                LinearStrainDistribution sd = new LinearStrainDistribution(StrainDistributionHeight, topFiberStrain, botFiberStrain);
                SectionAnalysisResult resultPoint = GetSectionResult(sd, CompressionFiberPosition);

                if (resultPoint != null)
                {
                    if (result == null)
                    {
                        result = new List<SectionAnalysisResult>();
                    }
                    result.Add(resultPoint);
                }
            }
            //Last point: Point of all steel tension
            Point2D rebarCentroid = FindRebarGeometricCentroid(LongitudinalBars);

            //
            //double TCDeltaMax = bound.MaxStrain;
            //double TCDeltaMin = bound.MinStrain;
            //double targetTCDelta = 0;

            //LinearStrainDistribution finalStrainDistribution = null;

            //SteelStrain = RootFinding.Brent(new FunctionOfOneVariable(GetTandCDeltaForSteelOnlyStrainIteration), TCDeltaMin, TCDeltaMax, StrainConvergenceTolerance, targetTCDelta);

            //
            return result;
        }

        //private double GetTandCDeltaForSteelOnlyStrainIteration(double SteelStrain)
        //{
        //    //function of CurrentCompressionFiberPosition
        //    //1. create trial strain
        //    LinearStrainDistribution iteratedStrainDistribution = null;
        //    switch (currentCompressionFiberPosition)
        //    {
        //        case CompressionLocation.Top:
        //            iteratedStrainDistribution = new LinearStrainDistribution(StrainHeight, ConcreteUltimateStrain.Value, SteelStrain);
        //            break;
        //        case CompressionLocation.Bottom:
        //            iteratedStrainDistribution = new LinearStrainDistribution(StrainHeight, SteelStrain, ConcreteUltimateStrain.Value);
        //            break;
        //        default:
        //            throw new CompressionFiberPositionException();
        //    }
        //    //2. Calculate result
        //    SectionAnalysisResult iteratedResult = GetSectionResult(iteratedStrainDistribution, currentCompressionFiberPosition);
        //    //3. calculate difference between T and C
        //    double T = iteratedResult.TForce;
        //    double C = iteratedResult.CForce;

        //    return Math.Abs(T) - Math.Abs(C);
        //}
    }
}
