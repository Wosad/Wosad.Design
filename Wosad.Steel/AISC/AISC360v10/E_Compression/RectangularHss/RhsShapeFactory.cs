using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Steel.AISC.AISC360v10.B_General;
using Wosad.Steel.AISC.AISC360v10.Compression;
using Wosad.Steel.AISC.AISC360v10.General.Compactness;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360v10.Compression
{
    public class RhsShapeFactory
    {
        public ISteelCompressionMember GetRhsShape(ISteelSection Section, double L_ex, double L_ey, double L_ez, ICalcLog CalcLog)
        {

            ISteelCompressionMember column = null;
            IShapeCompactness compactnessX = new ShapeCompactness.HollowMember(Section, Common.Section.Interfaces.FlexuralCompressionFiberPosition.Top, Common.Entities.MomentAxis.XAxis);
            IShapeCompactness compactnessY = new ShapeCompactness.HollowMember(Section, Common.Section.Interfaces.FlexuralCompressionFiberPosition.Top, Common.Entities.MomentAxis.YAxis);
            ICalcLog Log = new CalcLog();

            CompactnessClassAxialCompression webCompactnessX = compactnessX.GetWebCompactnessCompression();
            CompactnessClassAxialCompression webCompactnessY = compactnessY.GetWebCompactnessCompression();

            if (webCompactnessX ==  CompactnessClassAxialCompression.NonSlender && webCompactnessY == CompactnessClassAxialCompression.NonSlender )
            {
                return new RhsNonSlender(Section, L_ex, L_ey, L_ez, CalcLog);
            }
            else
            {
                return new RhsSlender(Section, L_ex, L_ey, L_ez, CalcLog);
            }
            return column;
        }
    }
}
