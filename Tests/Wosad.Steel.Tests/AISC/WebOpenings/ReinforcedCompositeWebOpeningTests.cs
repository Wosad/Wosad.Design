﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Common.Section.Predefined;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.AISC360v10.Connections.WebOpenings;


namespace Wosad.Steel.Tests.AISC
{
    [TestFixture]
    public class ReinforcedWebOpeningTests : ToleranceTestBase
    {
        public ReinforcedWebOpeningTests()
        {
            tolerance = 0.1; //10% accepted because it is not clear why on page 35 a value of 9.05 is used when it was caculated as 8.35
            SetExampleValues();
        }

        private void SetExampleValues()
        {

            d = 18.24;
            t_w = 0.415;
            t_deck = 3.0;
            t_fill = 2.5;
            t_e = 2.5;
            b_e = 120.0;
            h_0 = 10.0;
            a_o = 24.0;
            A_sn = 9.15;
            Q_n = 26.1;

            AiscShapeFactory f = new AiscShapeFactory();
            section = f.GetShape("W18X60") as ISectionI;
            F_y = 36.0;
            f_cPrime = 4.0;
            N_studs = 14.0;
            N_o = 2.0;
            e = 0.0;
            t_r = 0.375;
            b_r = 2.0;
        }

        double tolerance;

        //4.6 EXAMPLE 4: COMPOSITE BEAM
        //WITH REINFORCED OPENING

        double d;
        double t_w;
        double t_deck;
        double t_fill;
        double t_e;
        double b_e;
        double h_0;
        double a_o;
        double A_sn;
        double Q_n;
        ISectionI section;
        double F_y;
        double f_cPrime;
        double N_studs;
        double N_o;
        double e;
        double t_r;
        double b_r;

        [Test]
        public void OpeningCompositeReturnsShearStrength()
        {
            CompositeIBeamWebOpening o = new CompositeIBeamWebOpening(section, b_e, t_fill, t_deck, F_y, f_cPrime, N_studs, Q_n,
                N_o, a_o, h_0, e, t_r, b_r, Steel.AISC.DeckAtBeamCondition.Parallel,4.5,12.0);
            double phiV_n = o.GetShearStrength();
            double refValue =58.1;
            double actualTolerance = EvaluateActualTolerance(phiV_n, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }
    }
      
}
