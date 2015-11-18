using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.G_Shear
{
    public partial class CircularShearMember : IShearMember

    {
        double D;
        double t_nom;
        bool Is_SAW_member;
        ISteelMaterial material;
        double L_v;

        public CircularShearMember(double D, double t_nom, bool Is_SAW_member, double L_v, ISteelMaterial material)
        {
            this.D=              D;
            this.t_nom=              t_nom;
            this.Is_SAW_member=  Is_SAW_member;
            this.material = material;
            this.L_v = L_v;
        }
        public double GetShearStrength()
        {
            double phi = 0.9;
            double A_g = GetArea();
            double F_cr = Get_F_cr();
            double V_n = ((F_cr * A_g) / (2));
            double phiV_n = phi * V_n;
            return phiV_n;
        }

        private double GetArea()
        {
            double t = Is_SAW_member == true ? t_nom : 0.93 * t_nom;
            double A_g= Math.PI*Math.Pow(D,2)-Math.Pow(D-2*t,2)/4.0;
            return A_g;
        }

        private double Get_F_cr()
        {
            double E = material.ModulusOfElasticity;
            double F_y = material.YieldStress;
            double t = Is_SAW_member == true ? t_nom : 0.93 * t_nom;

            double F_cr1=((1.6*E) / (Math.Sqrt(((L_v) / (D)))*Math.Pow((((D) / (t))), 54)));
            double F_cr2=((0.78*E) / (Math.Pow((((D) / (t))), 32)));
            double F_cr3=0.6*F_y;
            List<double> F_crList = new List<double>() { F_cr1, F_cr2, F_cr3 };
            var F_cr = F_crList.Min();
            return F_cr;
        }
    }
}
