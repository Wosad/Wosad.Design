using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.Composite
{
    public partial class CompositeBeamSection : AnalyticalElement
    {
        public double GetEffectiveSlabWidth(double L_beam, double L_centerLeft, double L_centerRight, double L_edgeLeft, double L_edgeRight)
        {
            //I3.1a. Effective Width
            //The effective width of the concrete slab shall be the sum of the effective widths for
            //each side of the beam centerline, each of which shall not exceed:
            //(1) one-eighth of the beam span, center-to-center of supports;
            //(2) one-half the distance to the centerline of the adjacent beam; or
            //(3) the distance to the edge of the slab.

            //if the edge distances are set to -1 then that distance does not govern
            L_centerLeft  =   L_centerLeft ==-1? double.PositiveInfinity :  L_centerLeft ;
            L_centerRight =   L_centerRight==-1? double.PositiveInfinity :  L_centerRight;
            L_edgeLeft    =   L_edgeLeft   ==-1? double.PositiveInfinity :  L_edgeLeft   ;
            L_edgeRight = L_edgeRight == -1 ? double.PositiveInfinity : L_edgeRight;

            List<double> LeftSideList = new List<double>() { L_beam / 8.0, L_edgeLeft, L_centerLeft / 2.0 };
            List<double>RightSideList = new List<double>() { L_beam / 8.0, L_edgeRight, L_centerRight / 2.0 };

            var b_eff = LeftSideList.Min() + RightSideList.Min();
            return b_eff;
        }
    }
}
