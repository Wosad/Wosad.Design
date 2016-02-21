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
using Wosad.Concrete.ACI;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Concrete.ACI318_14
{
    /// <summary>
    ///  This class encpsulates  flexural analysis per ACI.
    /// </summary>
    public partial class ConcreteSectionFlexure: ConcreteFlexuralSectionBase
    {

        /// <summary>
        ///  Constructor used for flexure and axial load analysis.
        /// </summary>
        public ConcreteSectionFlexure(IConcreteSection Section,
            List<RebarPoint> LongitudinalBars, ICalcLog log)
            : base(Section, LongitudinalBars, log)
        {
            
        }

        public ConcreteFlexuralStrengthResult GetDesignFlexuralStrength(FlexuralCompressionFiberPosition FlexuralCompressionFiberPosition, ConfinementReinforcementType ConfinementReinforcementType)
        {
            ConcreteSectionFlexuralAnalysisResult nominalResult = this.GetNominalFlexuralCapacity(FlexuralCompressionFiberPosition);
            LinearStrainDistribution strainDistribution = nominalResult.StrainDistribution;
            double a;
            double d = strainDistribution.Height;
            double epsilon_t;

            if (FlexuralCompressionFiberPosition == FlexuralCompressionFiberPosition.Top)
            {
                a = strainDistribution.NeutralAxisTopDistance * this.Section.Material.beta1;
                epsilon_t = strainDistribution.BottomFiberStrain;
            }
            else
            {
                a = (d-strainDistribution.NeutralAxisTopDistance) * this.Section.Material.beta1;
                epsilon_t = strainDistribution.TopFiberStrain;
            }

            IRebarMaterial controllingBarMaterial = nominalResult.ControllingTensionBar.Point.Rebar.Material;
            double epsilon_ty = controllingBarMaterial.YieldStrain;

            FlexuralFailureModeClassification failureMode = GetFailureMode(epsilon_t, epsilon_ty);
            double phi = Get_phiFlexure(failureMode, ConfinementReinforcementType, epsilon_t, epsilon_ty);
            double phiM_n = phi* nominalResult.Moment;

            ConcreteFlexuralStrengthResult result = new ConcreteFlexuralStrengthResult(a, phiM_n, failureMode,epsilon_t);
            return result;
            
        }

        /// <summary>
        /// Strength reduction factor per Table 21.2.2
        /// </summary>
        /// <param name="failureMode">Compression, tension-controlled or transitional</param>
        /// <param name="ConfinementReinforcementType"></param>
        /// <param name="epsilon_t">Actual calculated tensile strain</param>
        /// <param name="epsilon_ty">Yield strain</param>
        /// <returns></returns>
        private double Get_phiFlexure(FlexuralFailureModeClassification failureMode, 
            ConfinementReinforcementType ConfinementReinforcementType, double epsilon_t, double epsilon_ty)
        {
            switch (failureMode)
            {
                case FlexuralFailureModeClassification.CompressionControlled:
                    if (ConfinementReinforcementType == ACI.ConfinementReinforcementType.Spiral)
                    {
                        return 0.75;
                    }
                    else
                    {
                        return 0.65;
                    }
                    break;
                case FlexuralFailureModeClassification.Transition:
                    if (ConfinementReinforcementType == ACI.ConfinementReinforcementType.Spiral)
                    {
                        return 0.75+0.15*(epsilon_t-epsilon_ty)/(0.005-epsilon_ty);
                    }
                    else
                    {
                        return 0.65 + 0.25 * (epsilon_t - epsilon_ty) / (0.005 - epsilon_ty);
                    }
                    break;
                case FlexuralFailureModeClassification.TensionControlled:
                    return 0.9;
                    break;
                default:
                    return 0.65;
                    break;
            }

        }

        /// <summary>
        /// Failure mode per Table 21.2.2
        /// </summary>
        /// <param name="epsilon_t">Actual calculated tensile strain</param>
        /// <param name="epsilon_ty">Yield strain</param>
        /// <returns></returns>
        private FlexuralFailureModeClassification GetFailureMode(double epsilon_t, double epsilon_ty)
        {
            if (epsilon_t<=epsilon_ty)
            {
                return FlexuralFailureModeClassification.CompressionControlled;
            }
            else if (epsilon_t>epsilon_ty && epsilon_t <0.005)
            {
                return FlexuralFailureModeClassification.Transition;
            }
            else
            {
                return FlexuralFailureModeClassification.TensionControlled;
            }
        }

    }
}
