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
        double Get_m();
        double Get_n();
        double  B_bp { get; set; }
        double N_bp { get; set; }
        double GetphiP_p();
        double A_1 { get; set; }
        double A_2 { get; set; }

        double  F_y { get; set; }
        double f_c { get; set; }
    }
}
