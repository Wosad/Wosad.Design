using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.AISC360_10;

namespace Wosad.Steel.Tests.AISC.AISC360_10.D_Tension
{
    [TestFixture]
    public class ShearLagFactorTests
    {

        /// <summary>
        /// Design Guide 29. Example 5.1 
        /// Page 46.
        /// </summary>
        [Test]
        public void TensionShearLagFactorCase2ReturnsValue()
        {
            ShearLagFactor slf = new ShearLagFactor();
            double U = slf.GetShearLagFactor(ShearLagCase.Case2,1.65,0.0,18.0,0,0);
            Assert.AreEqual(0.908,Math.Round(U,3));
        }

    }
}
