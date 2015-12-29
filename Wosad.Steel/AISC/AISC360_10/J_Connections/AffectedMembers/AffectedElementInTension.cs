using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;



namespace Wosad.Steel.AISC360_10.Connections.AffectedElements
{
    public class AffectedElementInTension : AffectedElement
    {
        public AffectedElementInTension(double F_y, double F_u): base( F_y,  F_u)
        {

        }

        public AffectedElementInTension(ISteelSection Section,ICalcLog CalcLog)
            : base(Section, CalcLog)
        {

        }

        public AffectedElementInTension(ISection Section, ISteelMaterial Material, ICalcLog CalcLog)
            :base(Section,Material, CalcLog)
        {

        }

        public double GetNetArea(double A_g, double N_bolts, double d_h, double s, List<double> g, double t_p, bool IsTensionSplicePlate  )
        {
            if (IsTensionSplicePlate==false)
            {
               return A_g - N_bolts * d_h - g.Sum(gage => Math.Pow(s, 2.0) / (4.0 * gage)); 
            }
            else
            {
                return 0.85 * A_g;
            }
            
        }

        /// <summary>
        /// The available strength of affected or connecting element in tension
        /// </summary>
        /// <param name="A_g">Gross area</param>
        /// <param name="A_e">Effective net area</param>
        /// <returns></returns>
        public double GetTensileCapacity(double A_g, double A_e)
        {
            double F_y = Section.Material.YieldStress;
            double F_u = Section.Material.UltimateStress;

            double phiR_nY = GetTensionYieldingStrength(F_y, A_e);
            double phiR_nU = GetTensileRuptureStrength(F_y, A_e);

            double phiR_n = Math.Min(phiR_nY, phiR_nU);
            return phiR_n;
        }

        private double GetTensileRuptureStrength(double F_u, double A_e)
        {

            double R_n = F_u * A_e; // (J4-2)
            double phi = 0.75;
            return phi * R_n;
        }

        private double GetTensionYieldingStrength(double F_y, double A_g)
        {
            double R_n = F_y * A_g; // (J4-1)
            double phi = 1.0;
            return phi * R_n;
        }

    }
}
