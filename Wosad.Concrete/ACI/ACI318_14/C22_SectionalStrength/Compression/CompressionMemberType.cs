using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_14
{

    //Table 22.4.2.1
    public enum CompressionMemberType
    {
        NonPrestressedWithTies,
        NonPrestressedWithSpirals,
        PrestressedWithTies,
        PrestressedWithSpirals,
        Composite
    }
}
