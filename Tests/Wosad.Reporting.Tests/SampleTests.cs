using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Common.CalculationLogger.Interfaces;


namespace Wosad.Reporting.Tests
{
    [TestFixture]
    public class ResultBuilderTests
    {
        [Test]
        public void ResultReturnsCorrectNumberOfBytes()
        {
            // create test log
            ICalcLogEntry lEntry = new CalcLogEntry();
            lEntry.ValueName = "L";
            lEntry.Reference = "";
            lEntry.FormulaID = null; //reference to formula from code
            lEntry.DescriptionReference = "/Templates/Analysis/UnbracedLength/BeamUnbracedLength.docx";
            lEntry.VariableValue = Math.Round(12.33467, 3).ToString();

            ICalcLog log = new CalcLog();
            log.AddEntry(lEntry);

            Wosad.Reporting.ResultBuilder.ResultBuilder buidler = new ResultBuilder.ResultBuilder();
            var result = buidler.BuildResultStream(new List<ICalcLog>() { log }, ResultBuilder.CalculatorOutputType.Word);

            Assert.AreEqual(4998, result.Count());
        }

    }
}
