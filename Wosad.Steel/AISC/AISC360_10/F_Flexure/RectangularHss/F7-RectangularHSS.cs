using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.AISC360_10.General.Compactness;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC.SteelEntities;


namespace Wosad.Steel.AISC.AISC360_10.Flexure
{
    public partial class BeamRectangularHss : FlexuralMemberRhsBase, ISteelBeamFlexure
    {
        ICalcLog CalcLog;
        MomentAxis MomentAxis;

        public BeamRectangularHss(ISteelSection section, MomentAxis MomentAxis, ICalcLog CalcLog)
            : base(section, CalcLog)
        {
            GetSectionValues();
            this.MomentAxis = MomentAxis;
            FlangeCompactnessClass = GetFlangeCompactness();

        }


        public CompactnessClassFlexure FlangeCompactness { get; set; }
        public CompactnessClassFlexure WebCompactness { get; set; }

        CompactnessClassFlexure FlangeCompactnessClass;

        //This section applies to square and rectangular HSS, and doubly symmetric boxshaped
        //members bent about either axis, having compact or noncompact webs and
        //compact, noncompact or slender flanges as defined in Section B4.1 for flexure.




        #region Limit States

        public virtual SteelLimitStateValue GetFlexuralYieldingStrength(FlexuralCompressionFiberPosition CompressionLocation)
        {
            return GetPlasticMomentCapacity(MomentAxis);
        }


        public virtual SteelLimitStateValue GetFlexuralFlangeLocalBucklingStrength(FlexuralCompressionFiberPosition CompressionLocation)
        {

            SteelLimitStateValue ls = new SteelLimitStateValue();
            

            if (FlangeCompactnessClass == CompactnessClassFlexure.Compact)
            {
                ls = new SteelLimitStateValue(-1, false);
            }
            else
            {
                double phiM_n = GetCompressionFlangeLocalBucklingCapacity(CompressionLocation, MomentAxis);
                ls = new SteelLimitStateValue(phiM_n, true);
            }


            return ls;
        }


        public SteelLimitStateValue GetFlexuralWebOrWallBucklingStrength(FlexuralCompressionFiberPosition CompressionLocation)
        {

            SteelLimitStateValue ls = new SteelLimitStateValue();

            ShapeCompactness.HollowMember Compactness = new ShapeCompactness.HollowMember(Section, CompressionLocation, MomentAxis);
            CompactnessClassFlexure cClass = Compactness.GetWebCompactnessFlexure();

            if (cClass == CompactnessClassFlexure.Compact)
            {
                ls = new SteelLimitStateValue(-1, false);

            }
            else
            {
                double phiM_n = GetWebLocalBucklingCapacity(MomentAxis, CompressionLocation);
                ls = new SteelLimitStateValue(phiM_n, true);
            }
            return ls;
        }



        #endregion




        internal void GetSectionValues()
        {

            E = Section.Material.ModulusOfElasticity;
            Fy = Section.Material.YieldStress;

        }

        double E;
        double Fy;

    }
}
