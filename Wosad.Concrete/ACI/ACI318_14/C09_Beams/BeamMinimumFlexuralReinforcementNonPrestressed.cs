using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Concrete.ACI;


namespace Wosad.Concrete.ACI318_14
{
    public partial class ReinforcedConcreteBeamNonprestressed
    {


        
        public double GetMinimumFlexuralReinforcement(double d,double f_y,  double RatioA_sProvidedToRequired=1.0 )
        {
            //ConcreteSectionFlexure FlexuralSection


            //Section 9.6.1.2 
            double b_w = ConcreteSection.b_w;
            double f_cPrime = ConcreteSection.Material.SpecifiedCompressiveStrength;
            double A_sMin1 = ((3*Math.Sqrt(f_cPrime)) / (f_y))*b_w*d;
            double A_sMin2 =((200*b_w*d) / (f_y));

            double A_sMin = Math.Max(A_sMin1, A_sMin2);
            if (RatioA_sProvidedToRequired>=1.333)
            {
                //9.6.1.3
                A_sMin = 0; 
            }
            return A_sMin;
        }

        //private double FindLowest_f_y(FlexuralCompressionFiberPosition CompressionFiber)
        //{
        //    double YCutoff  = (FlexuralSection.Section.SliceableShape.YMax + FlexuralSection.Section.SliceableShape.YMin)/2.0;
        //    List<RebarPoint> tensionBars;
        //            if (CompressionFiber == FlexuralCompressionFiberPosition.Top)
        //            {
        //            tensionBars = FlexuralSection.GetFilteredBars(YCutoff, 
        //            Wosad.Concrete.ACI.ConcreteSectionLongitudinalReinforcedBase.BarCoordinateFilter.Y,
        //            ACI.ConcreteSectionLongitudinalReinforcedBase.BarCoordinateLimitFilterType.Maximum);
        //            }
        //            else
        //            {
        //            tensionBars = FlexuralSection.GetFilteredBars(YCutoff,
        //            Wosad.Concrete.ACI.ConcreteSectionLongitudinalReinforcedBase.BarCoordinateFilter.Y,
        //            ACI.ConcreteSectionLongitudinalReinforcedBase.BarCoordinateLimitFilterType.Minimum);
        //            }

        //            double f_yMin = tensionBars.Min(b => b.Rebar.Material.YieldStress);

        //     return f_yMin;
        //}
    }
}
