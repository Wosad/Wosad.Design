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
    public class ChsShapeFactory
    {
        public ISteelCompressionMember GetChsShape(ISteelSection Section, double L_x, double L_y, double L_z, ICalcLog CalcLog)
        {

            ISteelCompressionMember column = null;
            IShapeCompactness compactnessX = new ShapeCompactness.HollowMember(Section, Common.Section.Interfaces.FlexuralCompressionFiberPosition.Top, Common.Entities.MomentAxis.XAxis);
            IShapeCompactness compactnessY = new ShapeCompactness.HollowMember(Section, Common.Section.Interfaces.FlexuralCompressionFiberPosition.Top, Common.Entities.MomentAxis.YAxis);
            ICalcLog Log = new CalcLog();

            CompactnessClassAxialCompression webCompactnessX = compactnessX.GetWebCompactnessCompression();
            CompactnessClassAxialCompression webCompactnessY = compactnessY.GetWebCompactnessCompression();

            //TODO:
            // ADD DISTINCTION BETWEEN CIRCULAR SHAPES
            return column;
        }
    }
}
