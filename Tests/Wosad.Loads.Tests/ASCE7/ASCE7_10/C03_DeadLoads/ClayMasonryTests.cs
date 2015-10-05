using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Loads.ASCE.ASCE7_10.DeadLoads.Components;

namespace Wosad.Loads.Tests.ASCE7.ASCE7_10.C03_DeadLoads
{

       [TestFixture]
    public partial class ComponentDeadWeightTests
    {
    

        [Test]
        public void ClayMasonryReturnsValue12In()
        {
            
            ComponentClayBrick brick = new ComponentClayBrick(2, 0, 0);
            var brickEntr = brick.Weight;
            Assert.AreEqual(115.0, brickEntr);
        }
       }
    
}
