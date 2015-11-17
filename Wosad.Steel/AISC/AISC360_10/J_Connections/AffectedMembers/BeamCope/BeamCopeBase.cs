using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.J_Connections
{
    public abstract partial class BeamCopeBase
    {
        public BeamCopeBase(double c, double d_c, ISectionI Section, ISteelMaterial Material)
        {
            this.c = c;
            this.d_c = d_c;
            this.Section = Section;
            this.Material = Material;
        }

        public double GetFlexuralStrength()
        {
            double phiM_n_Rupture = GetRuptureStrength();
            double phiM_n_Buckling = GetBucklingStrength();
            return Math.Min(phiM_n_Rupture, phiM_n_Buckling);
        }

        private double GetBucklingStrength()
        {
            double F_cr = GetF_cr();
            //AISC 2010 Manual page 9-7
            double S_net = GetS_net(); //Eq (9-6)
            return 0.9 * F_cr * S_net;
        }

        private double GetRuptureStrength()
        {
            double F_u = Material.UltimateStress;
            double Z_net = GetZ_net();
            //AISC 2010 Manual page 9-6
            return 0.75 * F_u * Z_net; //Eq (9-4)
            //note this is different from 13th edition of the manual
            //where Snet was used
         }

        protected abstract double GetS_net();
        protected abstract double GetZ_net();
        protected abstract double GetF_cr();
        public double c { get; set; }
        public double  d_c { get; set; }
        public ISectionI Section { get; set; }
        public ISteelMaterial Material { get; set; }

        private double _d;

        public double d
        {
            get {
                if (_d == 0.0)
                {
                    _d = Section.Height; 
                }
                return _d; }

        }
        

        protected abstract double Get_h_o();
        private double _h_o;

        public double h_o
        {
            get {
                if (_h_o==0)
                {
                    _h_o = Get_h_o();
                }
                return _h_o; }
        }
        
        public bool CheckCopeGeometry()
        {
            bool IsAcceptableCope = true;
            
            if (c<=2*d && d_c<=d/2.0)
            {
                IsAcceptableCope = true;
            }
            else
            {
                IsAcceptableCope = false;
            }
            return IsAcceptableCope;
        }
    }
}
