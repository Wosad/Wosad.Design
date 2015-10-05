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
        public void ConcreteOnDeck1_5InvLWReturnsValue()
        {
            ComponentDeckWithLWFill1_5 concDeck = new ComponentDeckWithLWFill1_5(4, 3, 0);
            var deckEntry = concDeck.Weight;
            Assert.AreEqual(47, deckEntry);
        }

        [Test]
        public void ConcreteOnDeck3LWReturnsValue()
        {
            ComponentDeckWithLWFill3 concDeck = new ComponentDeckWithLWFill3(1, 3, 0);
            var deckEntry = concDeck.Weight;
            Assert.AreEqual(39, deckEntry);
        }
    }
}
