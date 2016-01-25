using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;
using Wosad.Common.Mathematics;

namespace Wosad.Steel.AISC.AISC360_10.Connections
{
    public partial class SeatedConnection : AnalyticalElement
    {
        double a_seat;
        double b_seat;
        double theta;
        double F_y;
        double E;


        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="a_seat">Vertical seat dimension</param>
        /// <param name="b_seat">Horizontal seat dimension</param>
        public SeatedConnection(double a_seat, double b_seat, double F_y, double E)
        {
            this.a_seat=a_seat;
            this.b_seat = b_seat;
            this.F_y = F_y;
            this.E = E;
            theta = Math.Atan(a_seat / b_seat);
        }
    }
}
