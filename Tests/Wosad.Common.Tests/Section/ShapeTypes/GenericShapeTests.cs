using System.Collections.Generic;
using NUnit.Framework;
using Wosad.Common.Mathematics;
using Wosad.Common.Section.General;
using Wosad.Common.Section.Interfaces;


namespace Wosad.Common.Tests.Section.ShapeTypes
{
    [TestFixture]
    public class GenericShapeTests
    {
        [Test]
        public void SliceTopReturnsPolygonForRectangle()
        {
            var Points = new List<Point2D>
            {
                new Point2D(-1, -1),
                new Point2D(-1, 1),
                new Point2D(1, 1),
                new Point2D(1, -1)
            };
            var rect = new GenericShape(Points);
            IMoveableSection sect = rect.GetTopSliceSection(1, SlicingPlaneOffsetType.Top);
            Assert.AreEqual(1.0,sect.YMax);
        }
    }
}