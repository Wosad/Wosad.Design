using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage.Base
{
    public abstract class AnchorageConcreteLimitState : AnchorageLimitState
    {
        public AnchorageConcreteLimitState(
            int n, double h_eff, AnchorInstallationType AnchorType
            )
            : base(n, AnchorType)
        {

        }

        /// <summary>
        /// Concrete strength
        /// </summary>
        public double  fc { get; set; }

        /// <summary>
        /// Lightweight Concrete Factor
        /// </summary>
        public double lambda { get; set; }
        
        
        /// <summary>
        /// Embedment of anchors
        /// </summary>
        public double h_eff { get; set; }


    }
}
