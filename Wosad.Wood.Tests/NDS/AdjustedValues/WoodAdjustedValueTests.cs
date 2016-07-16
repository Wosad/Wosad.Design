using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Wood.NDS.NDS2015;




namespace Wosad.Wood.Tests.NDS.SawnLumber
{
    [TestFixture]
    public partial class WoodAdjustedValueTests : ToleranceTestBase
    {
        public WoodAdjustedValueTests()
        {
            tolerance = 0.02; //2% can differ from rounding
        }

        double tolerance;


    }
}
