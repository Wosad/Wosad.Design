using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Concrete.ACI;
using Wosad.Concrete.ACI.Entities;
using Wosad.Concrete.ACI318_14.Materials;

namespace Wosad.Concrete.ACI318_14.Tests
{
    public class ConcreteTestBase: ToleranceTestBase
    {
        public IConcreteMaterial GetConcreteMaterial(double fc)
        {
            CalcLog log = new CalcLog();
            ConcreteMaterial concrete = new ConcreteMaterial(fc, ConcreteTypeByWeight.Normalweight, log);
            return concrete;
        }

        public IConcreteSection GetRectangularSection(double Width, double Height, double fc)
        {
            IConcreteMaterial mat = GetConcreteMaterial(fc);
            CrossSectionRectangularShape section = new CrossSectionRectangularShape(mat, null, Width, Height);
            return section;
        }


    }
}
