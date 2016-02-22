using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Concrete.ACI
{
    public class ConcreteCompressionStrengthResult: ConcreteFlexuralStrengthResult
    {
        public ConcreteCompressionStrengthResult(double a, double phiM_n, 
            FlexuralFailureModeClassification FlexuralFailureModeClassification,
            double epsilon_t, double epsilon_ty)
            : base(a, phiM_n, FlexuralFailureModeClassification, epsilon_t, epsilon_ty) 
        {

        }
                public ConcreteCompressionStrengthResult(IStrainCompatibilityAnalysisResult nominalResult, 
            FlexuralCompressionFiberPosition FlexuralCompressionFiberPosition, double beta1)
                    :base( nominalResult, FlexuralCompressionFiberPosition,  beta1)
        {

        }
    }
}
