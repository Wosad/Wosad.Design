using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC
{
    public interface IBasePlate
    {
        double GetLength();
        double  B_bp { get; set; }
        double N_bp { get; set; }
    }
}
