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
    public partial class CompositeBeamSection: AnalyticalElement
    {
        public CompositeBeamSection()
        {
            ICalcLog log = new CalcLog();
            this.Log = log;
        }

        public CompositeBeamSection(ICalcLog log): base(log)
        {

        }
        public CompositeBeamSection(ISliceableSection SteelSection, double SlabEffectiveWidth,
            double SlabSolidThickness, double SlabDeckThickness, double F_y, double f_cPrime)
        {
            this.SteelSection =       SteelSection ;
            this.SlabEffectiveWidth = SlabEffectiveWidth ;
            this.SlabSolidThickness = SlabSolidThickness ;
            this.SlabDeckThickness =  SlabDeckThickness ;
            this.SumQ_n =             SumQ_n ;
            this.F_y =                F_y ;
            this.f_cPrime =           f_cPrime ;
        }

        public ISliceableSection SteelSection { get; set; }
        public double SlabEffectiveWidth { get; set; }
        public double SlabSolidThickness { get; set; }
        public double SlabDeckThickness { get; set; }
        public double SumQ_n { get; set; }
        public double C_Slab { get; set; }
        public double F_y { get; set; }
        public double f_cPrime { get; set; }
    }
}
