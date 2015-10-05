using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.SectionTypes;

namespace Wosad.Common.Tests.Section.ShapeTypes
{
    /// <summary>
    /// Compare calculated properties to W18X35 listed properties.
    /// </summary>
     [TestFixture]
    public class SectionIRolledTests
    {

         [Test]
         public void SectionIRolledReturnsArea()
         {
             SectionIRolled shape = new SectionIRolled("", 17.7, 6.0, 0.425, 0.3, 0.827);
             double A = shape.Area;
             Assert.AreEqual(10.74, Math.Round(A, 2));
             //Manual gives 10.3 but actual area checked in Autocad is 10.42
         }

         [Test]
         public void SectionIRolledReturnsIx()
         {
             SectionIRolled shape = new SectionIRolled("", 17.7, 6.0, 0.425, 0.3, 0.827);
             double Ix = shape.MomentOfInertiaX;
             Assert.AreEqual(540.05, Math.Round(Ix, 2));
             //Manual gives 510 but actual area checked in Autocad is 540.0505
         }

         [Test]
         public void SectionIRolledReturnsIy()
         {
             SectionIRolled shape = new SectionIRolled("", 17.7, 6.0, 0.425, 0.3, 0.827);
             double Ix = shape.MomentOfInertiaY;
             Assert.AreEqual(15.42, Math.Round(Ix, 2));
             //Checked in Autocad is 540.0505
         }

    }
}
