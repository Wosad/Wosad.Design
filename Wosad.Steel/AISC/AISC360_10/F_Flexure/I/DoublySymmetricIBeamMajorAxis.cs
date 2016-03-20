using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Exceptions;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.AISC360_10.B_General;
using Wosad.Steel.AISC.AISC360_10.General.Compactness;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public class DoublySymmetricIBeam
    {
        public DoublySymmetricIBeam(ISteelSection section, ICalcLog CalcLog, FlexuralCompressionFiberPosition compressionFiberPosition, bool IsRolledMember)
        {
            this.section        = section        ;
            this.IsRolledMember = IsRolledMember ;
            this.CalcLog = CalcLog ;
            this.compressionFiberPosition = compressionFiberPosition;
        }



        FlexuralCompressionFiberPosition compressionFiberPosition;
        ISteelSection section;
        bool IsRolledMember;
        ICalcLog CalcLog;


        public ISteelBeamFlexure GetBeamCase()
        {
            ISteelBeamFlexure beam = null;
            IShapeCompactness compactness = new ShapeCompactness.IShapeMember(section, IsRolledMember, compressionFiberPosition);

            CompactnessClassFlexure flangeCompactness = compactness.GetFlangeCompactnessFlexure();
            CompactnessClassFlexure webCompactness = compactness.GetWebCompactnessFlexure();

            if (flangeCompactness == CompactnessClassFlexure.Compact && webCompactness == CompactnessClassFlexure.Compact)
            {
                return new BeamIDoublySymmetricCompact(section, IsRolledMember, CalcLog);
            }
            else
            {
                if (webCompactness == CompactnessClassFlexure.Compact)
                {
                    return new BeamIDoublySymmetricCompactWebOnly(section, IsRolledMember, CalcLog);
                }
                else if ( webCompactness == CompactnessClassFlexure.Noncompact)
                {
                    if (flangeCompactness == CompactnessClassFlexure.Compact)
                    {
                        return new BeamINoncompactWebCompactFlange(section, IsRolledMember, CalcLog);
                    }
                    else
	                {
                        return new BeamINoncompactWeb(section, IsRolledMember, CalcLog);
	                }

                }

                else //slender web
                {
                    if (flangeCompactness == CompactnessClassFlexure.Compact)
                    {
                        return new BeamISlenderWebCompactFlange(section, IsRolledMember, CalcLog);
                    }
                    else
                    {
                        return new BeamISlenderWeb(section, IsRolledMember, CalcLog);
                    }

                }

              }
            return beam;

        }



        private IShapeCompactness compactness;

	        public IShapeCompactness Compactness
	        {
		        get 
                { 
                    if (compactness == null)
	                {
		                compactness = GetCompactness();
	                }
                    return compactness;
                }
	        }

        private IShapeCompactness GetCompactness()
        {
            ISectionI Isec = section as ISectionI;
            if (Isec != null)
            {
                compactness = new ShapeCompactness.IShapeMember(section, IsRolledMember, compressionFiberPosition);
            }
            else
            {
                throw new ShapeTypeNotSupportedException(" flexural calculation of I-beam");
            }
            return compactness;
        }
	

    }
}
