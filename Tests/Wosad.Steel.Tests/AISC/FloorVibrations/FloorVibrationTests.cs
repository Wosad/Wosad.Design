using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.Entities.FloorVibrations;
using Wosad.Steel.AISC.UFM;

namespace Wosad.Steel.Tests.AISC
{
    [TestFixture]
    public class FloorVibrationTests : ToleranceTestBase
    {
        public FloorVibrationTests()
        {
            tolerance = 0.02; //2% can differ from rounding
        }

        double tolerance;

        /// <summary>
        /// AISC Live Webinar
        /// May 5, 2016
        /// Thomas Murray
        /// Vibration of Steel Framed Structural Systems Due to Human Activity
        /// Session 1: Basic Analysis of Structural Systems Due to Human Activity 
        /// </summary>
        [Test]
        public void SingleBeamReturnsFrequency()
        {
            FloorVibrationBeamSingle bm = new FloorVibrationBeamSingle();
            double f_n = bm.GetFundamentalFrequency(0.376);
            double refValue = 5.77;
            double actualTolerance = EvaluateActualTolerance(f_n, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }

        [Test]
        public void PanelReturnsModalDamping()
        {
            FloorVibrationBeamGirderPanel bmPanel = new FloorVibrationBeamGirderPanel();
            List<string> Components =
                new List<string>()
                {

                    "Structural system",
                    "Ceiling and ductwork",
                    "Electronic office fitout"

                };
            double beta = bmPanel.GetFloorModalDampingRatio(Components);
            double refValue = 0.025;
            double actualTolerance = EvaluateActualTolerance(beta, refValue);
            Assert.LessOrEqual(actualTolerance, tolerance);

        }


    }
}
