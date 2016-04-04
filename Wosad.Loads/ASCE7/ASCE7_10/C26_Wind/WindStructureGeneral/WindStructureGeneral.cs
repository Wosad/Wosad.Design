using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Loads.ASCE.ASCE7_10.WindLoads.Building.DirectionalProcedure;

namespace Wosad.Loads.ASCE7.ASCE7_10.Wind
{


    public partial class WindStructureGeneral : BuildingDirectionalProcedureElement
    {
        public WindStructureGeneral(ICalcLog CalcLog)
            : base(CalcLog)
        {


        }
    }
}
