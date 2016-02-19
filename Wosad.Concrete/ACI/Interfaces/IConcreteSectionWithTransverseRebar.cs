using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI
{
    public interface IConcreteSectionWithTransverseRebar : IConcreteSectionWithLongitudinalRebar
    {
        double A_s { get; set; }
        double s { get; set; }
        double b_w { get; set; }
    }
}
