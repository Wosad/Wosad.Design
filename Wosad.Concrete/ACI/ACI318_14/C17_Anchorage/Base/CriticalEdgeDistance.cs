using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage
{
    public partial class ConcreteAnchorageElement
    {
        /// <summary>
        /// Critical edge distance required to develop the basic strength as controlled by concrete breakout or bond of a post-installed anchor in tension in uncracked concrete without supplementary reinforcement to control splitting, per 17.7.6
        /// </summary>
        /// <param name="AnchorageType"></param>
        /// <param name="h_ef"></param>
        /// <returns></returns>
        public double GetCriticalEdgeDistance(AnchorageType AnchorageType, double h_ef)
        {
            double c_ac;
            switch (AnchorageType)
            {
                case AnchorageType.Adhesive:
                    c_ac = 2 * h_ef;
                    break;
                case AnchorageType.Undercut:
                    c_ac = 2.5 * h_ef;
                    break;
                case AnchorageType.TorqueControlledExpansion:
                    c_ac = 4 * h_ef;
                    break;
                case AnchorageType.DisplacementControlledExpansion:
                    c_ac = 4 * h_ef;
                    break;
                default:
                    c_ac = 4 * h_ef;
                    break;
            }
            return c_ac;

        }
    }
}
