using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wosad.Analytics.ACI318_14.Tests.Flexure
{
    public class RebarInput
    {
        public double Area { get; set; }
        public double Cover { get; set; }

        public RebarInput()
        {

        }
        public RebarInput(double Area, double Cover)
        {
            this.Area = Area;
            this.Cover = Cover;
        }
    }
}
