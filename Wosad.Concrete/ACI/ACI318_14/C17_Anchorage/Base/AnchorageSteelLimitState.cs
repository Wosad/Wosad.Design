using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage.Base
{
    public abstract class AnchorageSteelLimitState : AnchorageLimitState
    {

        public AnchorageSteelLimitState(
            int n,
            double futa,
            double fya,
            double A_se_N,
            AnchorSteelElementFailureType SteelFailureType, AnchorInstallationType AnchorType
            )
            : base(n, AnchorType)
        {

            this.n=     n;
            this.futa=  futa;
            this.fya=   fya;
            this.A_se_N = A_se_N;

        }

        /// <summary>
        /// Ductile versus Non-ductile steel element. 
        /// </summary>
        public AnchorSteelElementFailureType SteelFailureType { get; set; }

        /// <summary>
        /// Ultimate stress of anchor steel
        /// </summary>
            public double futa { get; set; }

        /// <summary>
            /// Yield stress of anchor steel
        /// </summary>
            public double fya { get; set; }

        /// <summary>
            /// Effective cs area of an anchor in tension
        /// </summary>
            public double A_se_N { get; set; }

    }
}
