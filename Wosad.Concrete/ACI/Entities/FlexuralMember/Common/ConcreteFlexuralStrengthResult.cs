using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ConcreteFlexuralStrengthResult(double a, double phiM_n, FlexuralFailureModeClassification FlexuralFailureModeClassification, double epsilon_t) 
        {
                this.a                    =a                   ;
                this.phiM_n               =phiM_n              ;
                this.FlexuralFailureModeClassification = FlexuralFailureModeClassification;
                this.epsilon_t            = epsilon_t;

        }

 
        public double a { get; set; }

        public double phiM_n { get; set; }

        public FlexuralFailureModeClassification FlexuralFailureModeClassification { get; set; }

        public double epsilon_t { get; set; }

    }
}
