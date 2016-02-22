﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Concrete.ACI;

namespace Wosad.Concrete.ACI
{
    public class ConcreteFlexuralStrengthResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="a">Depth of compression block</param>
        /// <param name="phiM_n" Moment strength</param>
        /// <param name="FlexuralFailureModeClassification"> Identifies if section is tension-controlled, transitional or compression-controlled</param>
        /// <param name="epsilon_t">Controlling tensile strain</param>
        /// <param name="epsilon_t">Controlling bar tensile yield strain</param>
        public ConcreteFlexuralStrengthResult(double a, double phiM_n, 
            FlexuralFailureModeClassification FlexuralFailureModeClassification,
            double epsilon_t, double epsilon_ty) 
        {
                this.a                    =a                   ;
                this.phiM_n               =phiM_n              ;
                this.FlexuralFailureModeClassification = FlexuralFailureModeClassification;
                this.epsilon_t            = epsilon_t;
                this.epsilon_ty = epsilon_ty;
        }

        public ConcreteFlexuralStrengthResult(IStrainCompatibilityAnalysisResult nominalResult, 
            FlexuralCompressionFiberPosition FlexuralCompressionFiberPosition, double beta1)
        {
            UpdateValuesFromResult(nominalResult, FlexuralCompressionFiberPosition,beta1);
        }

        private void UpdateValuesFromResult(IStrainCompatibilityAnalysisResult nominalResult, 
            FlexuralCompressionFiberPosition FlexuralCompressionFiberPosition, double beta1)
        {
            LinearStrainDistribution strainDistribution = nominalResult.StrainDistribution;
            double a;
            double d = strainDistribution.Height;

            if (FlexuralCompressionFiberPosition == FlexuralCompressionFiberPosition.Top)
            {
                a = strainDistribution.NeutralAxisTopDistance * beta1;
                epsilon_t = strainDistribution.BottomFiberStrain;
            }
            else
            {
                a = (d - strainDistribution.NeutralAxisTopDistance) * beta1;
                epsilon_t = strainDistribution.TopFiberStrain;
            }

            IRebarMaterial controllingBarMaterial = nominalResult.ControllingTensionBar.Point.Rebar.Material;
            double epsilon_ty = controllingBarMaterial.YieldStrain;
        }
 
        public double a { get; set; }

        public double phiM_n { get; set; }

        public FlexuralFailureModeClassification FlexuralFailureModeClassification { get; set; }

        public double epsilon_t { get; set; }

        public double epsilon_ty { get; set; }
    }
}