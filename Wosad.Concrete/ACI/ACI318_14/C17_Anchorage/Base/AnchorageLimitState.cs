using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage.Base
{
    public abstract class AnchorageLimitState
    {
        /// <summary>
        /// Number of anchors
        /// </summary>
        public int n { get; set; }

        /// <summary>
        /// Cast-in or post-installed anchor
        /// </summary>
        public AnchorInstallationType AnchorType { get; set; }


        public AnchorageLimitState(
            int n, AnchorInstallationType AnchorType
            )
        {
            this.n = n;
            this.AnchorType = AnchorType;
        }


        public abstract double GetNominalStrength();
    }
}
