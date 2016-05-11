using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.BasePlate
{
    public abstract class BasePlateTypeBase : IBasePlate
    {
        public BasePlateTypeBase(double B_bp, double N_bp)
        {
            this.B_bp = B_bp;
            this.N_bp = N_bp;
        }
        public abstract double GetLength();

        public double B_bp { get; set; }

        public double  N_bp { get; set; }

    }
}
