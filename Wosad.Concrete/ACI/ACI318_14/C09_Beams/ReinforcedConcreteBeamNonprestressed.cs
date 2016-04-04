using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Section.Interfaces;
using Wosad.Concrete.ACI;

namespace Wosad.Concrete.ACI318_14
{
    public partial class ReinforcedConcreteBeamNonprestressed
    {
        public ReinforcedConcreteBeamNonprestressed(IConcreteSection Section, ICalcLog log)
        {
            this.ConcreteSection = Section;
            this.Log = log;
        }

        private IConcreteSection concreteSection;

        public IConcreteSection ConcreteSection
        {
            get { return concreteSection; }
            set { concreteSection = value; }
        }


        private ICalcLog log;

        public ICalcLog Log
        {
            get { return log; }
            set { log = value; }
        }

        public ConcreteFlexuralStrengthResult GetFlexuralDesignStrength(List<RebarPoint> LongitudinalBars, FlexuralCompressionFiberPosition CompressionFiber, ConfinementReinforcementType ConfinementReinforcementType)
        {
            ConcreteSectionFlexure s = new ConcreteSectionFlexure(this.ConcreteSection, LongitudinalBars, Log);
            return s.GetDesignFlexuralStrength(CompressionFiber, ConfinementReinforcementType);
        }


    }
}
