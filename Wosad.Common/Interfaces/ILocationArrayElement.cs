using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wosad.Common.Mathematics;

namespace Wosad.Common.Interfaces
{
    public interface ILocationArrayElement
    {
        Point2D Location { get; set; }
        double LimitDeformation { get; set; }

        double GetDistanceToPoint(Point2D point);
    }
}
