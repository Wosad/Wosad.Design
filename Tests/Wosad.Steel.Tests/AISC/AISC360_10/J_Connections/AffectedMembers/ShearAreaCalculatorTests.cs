using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.AISC360_10.J_Connections.AffectedMembers;

namespace Wosad.Steel.Tests.AISC.AISC360_10.Connections.AffectedMembers
{
    [TestFixture]
    public class ShearAreaCalculatorTests
    {
        /// <summary>
        /// AISC Design guide 29. Page47
        /// </summary>
        /// 
        [Test]
        public void ShearAreaCalculatorCalculatesA_gv()
        {
            ShearAreaCalculator c = GetShearAreaCalc();
            double A_gv = c.GetGrossAreaShear();
            double refValue = 39.0;
            Assert.AreEqual(refValue, A_gv);
        }
        [Test]
        public void ShearAreaCalculatorCalculatesA_nv()
        {
            ShearAreaCalculator c = GetShearAreaCalc();
            double A_nv = c.GetNetAreaShear();
            double refValue = 26.0;
            Assert.AreEqual(refValue, A_nv);
        }
        [Test]
        public void ShearAreaCalculatorCalculatesA_nt()
        {
            ShearAreaCalculator c = GetShearAreaCalc();
            double A_nt = c.GetNetAreaTension();
            double refValue = 7.0;
            Assert.AreEqual(refValue, A_nt);
        }
        [Test]
        private ShearAreaCalculator GetShearAreaCalc()
        {
            double d_hole = 7.0/8.0+2*(1.0/16.0);
            double t =2*1.0;
            ShearAreaCalculator c = new ShearAreaCalculator(Steel.AISC.ShearAreaCase.LBlock, 7, 2, 3, 3, d_hole, t, 1.5, 2.0);
            return c;
        }
    }
}
