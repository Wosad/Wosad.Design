using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;

namespace Wosad.Steel.AISC.AISC360v10.HSS.TrussConnections
{
    public abstract partial class HssTrussConnection : AnalyticalElement
    {

        public HssTrussConnection(bool IsTensionChord,
            double P_uChord, double M_uChord)
        {
            this.IsTensionChord = IsTensionChord;
            this.P_uChord = P_uChord;
            this.M_uChord = M_uChord;
        }
        private double _U;

        public double U
        {
            get
            {
                _U = GetU();
                return _U;
            }
            set { _U = value; }
        }

        private double GetU()
        {
            double A_g = GetChordArea();
            double S = GetSectionModulus();
            double Axial = Math.Abs((P_uChord) / (F_y * A_g));
            double Flexure = Math.Abs((M_uChord) / (F_y * S));
            double U = (Axial + Flexure);
            return U;
        }

        protected abstract double GetSectionModulus();
        protected abstract double GetChordArea();

        double P_uChord { get; set; }
        double M_uChord { get; set; }

        protected bool IsTensionChord { get; set; }


        protected abstract double GetF_y();


        protected abstract double GetF_yb();

       
        private double _F_y;

        protected double F_y
        {
            get
            {
                _F_y = GetF_y();
                return _F_y;
            }
            set { _F_y = value; }
        }



        private double _F_yb;

        protected double F_yb
        {
            get
            {
                _F_yb = GetF_yb();
                return _F_yb;
            }
            set { _F_yb = value; }
        }


        protected bool IsMainBranch { get; set; }
    }
}
