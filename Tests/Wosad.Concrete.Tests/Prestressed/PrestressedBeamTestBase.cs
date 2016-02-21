using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rhino.Mocks;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Concrete.ACI;
using Wosad.Concrete.ACI318_14.Materials;
using Wosad.Concrete.ACI.Entities;

namespace Wosad.Concrete.ACI318_14.Tests.Prestressed
{
    public class PrestressedBeamTestBase
    {
        private ICalcLog log;

        public ICalcLog Log
        {
            get { return log; }
            set { log = value; }
        }
        public PrestressedBeamTestBase()
        {
            MockRepository mocks = new MockRepository();
            log = mocks.Stub<ICalcLog>();
        }

        protected IPrestressedConcreteMaterial GetPrestressedConcreteMaterial(double fc, double fci)
        {
            ConcretePrestressed concrete = new ConcretePrestressed(fc, fci, ConcreteTypeByWeight.Normalweight, log);
            return concrete;
        }
    }
}
