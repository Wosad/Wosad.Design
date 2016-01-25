using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Section.Interfaces;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.Interfaces;
using Wosad.Steel.AISC360_10.Connections.AffectedElements;

namespace Wosad.Steel.AISC.AISC360_10.Connections.AffectedMembers
{
    public class AffectedElementInFlexure: AffectedElement
    {
        public AffectedElementInFlexure(double S_g, double Z_net, double F_y, double F_u): base( F_y,  F_u)
        {
            S_g= S_g;
            Z_net= Z_net;
        }
        public AffectedElementInFlexure(SectionOfPlateWithHoles Section, ISteelMaterial Material, ICalcLog CalcLog)
            : base(Section, Material, CalcLog)
        {
            double S_xb = Section.S_xBot;
            double S_xt =Section.S_xTop;

            S_g = Math.Min(S_xb, S_xt);
            Z_net = Z_net;
        }

        double S_g;
        double Z_net;

        public double GetFlexuralStrength()
        {
            return Math.Min(this.Section.Material.YieldStress * S_g, this.Section.Material.UltimateStress * Z_net);
        }

    }
}
