using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;
using Wosad.Common.Section.Predefined;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.SteelEntities.Materials;
using Wosad.Steel.AISC.SteelEntities.Sections;

namespace Wosad.Steel.AISC.AISC360_10.Compression
{
    public class CompressionMemberFactory
    {
        public SteelColumn GetCompressionMember(ISection section, ShapeTypeSteel shapeType, double L_x, double K_x, double L_y, double K_y, double F_y)
        {
            string DEFAULT_EXCEPTION_STRING = "Selected shape is not supported. Select a different shape.";
            SteelColumn col = null;
            CalcLog log = new CalcLog();
            SteelMaterial mat = new SteelMaterial(F_y);


            switch (shapeType)
            {
                case ShapeTypeSteel.Angle:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                case ShapeTypeSteel.Box:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                case ShapeTypeSteel.Channel:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                case ShapeTypeSteel.Circular:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                case ShapeTypeSteel.CircularHSS:
                    break;
                case ShapeTypeSteel.DoubleAngle:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                case ShapeTypeSteel.IShapeAsym:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                case ShapeTypeSteel.IShapeBuiltUp:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                case ShapeTypeSteel.IShapeRolled:
                    ISectionI secI = section as ISectionI;
                    if (secI != null)
                    {
                        SteelSectionI sectionI = new SteelSectionI(secI, mat);
                        col = new CompressionMemberIDoublySymmetric(sectionI, true, L_x, L_y, K_x, K_y, log);
                    }

                    break;
                case ShapeTypeSteel.Rectangular:
                    break;
                case ShapeTypeSteel.RectangularHSS:
                    ISectionTube secTube = section as ISectionTube;
                    if (secTube != null)
                    {
                        SteelRhsSection sectionTube = new SteelRhsSection(secTube, mat);
                        col = new CompressionMemberRhs(sectionTube, L_x, L_y, K_x, K_y, log);
                    }
                    break;
                case ShapeTypeSteel.TeeBuiltUp:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                case ShapeTypeSteel.TeeRolled:
                    throw new Exception(DEFAULT_EXCEPTION_STRING);
                    break;
                default:
                    break;
            }
            return col;
        }

    }
    
}
