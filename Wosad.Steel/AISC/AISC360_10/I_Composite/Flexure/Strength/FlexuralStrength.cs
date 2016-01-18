using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.Composite
{
    public partial class CompositeBeamSection: AnalyticalElement
    {
        public double GetFlexuralStrength(double SumQ_n)
        {
            this.SumQ_n = SumQ_n;
            double A_s = SteelSection.A;
            double MaximumTForce = A_s * F_y;
            //Equations (C-I3-7) and  (C-I3-8)
            double MaximumSlabForce = Math.Min(SumQ_n, SlabEffectiveWidth * SlabSolidThickness * 0.85 * f_cPrime);
            double SteelSectionHeight = (SteelSection.YMax - SteelSection.YMin);
            double d_3 = SteelSectionHeight - SteelSection.y_pBar; // distance from Py to the top of the steel section
            double d_1 = Get_d_1(); //distance from the centroid of the compression force, C, in the concrete to the top of the steel section
            double d_2; //distance from the centroid of the compression force in the steel section to the top of the steel section
            double C;

            if (MaximumSlabForce>=MaximumTForce)
            {
                //Section is fully composite
                C = MaximumTForce;
                d_2 = 0;
                 
            }
            else
            {
                //Section is partially composite
                double C_steel = (MaximumTForce -  MaximumSlabForce) / 2.0;
                double A_sPrime = C_steel / F_y;
                IMoveableSection compressedSteelSection = SteelSection.GetTopSliceOfArea(A_sPrime);
                var C_steelCoordinate = compressedSteelSection.GetElasticCentroidCoordinate();
                d_2 = SteelSectionHeight - C_steelCoordinate.Y;
                C = MaximumSlabForce + C_steel;
            }
            double P_y = MaximumTForce;
            double phiM_n = 0.9 * (C * (d_1 + d_2) + P_y * (d_3 - d_2)); // (C-I3-10)
            return phiM_n;
        }

        
    }
}
