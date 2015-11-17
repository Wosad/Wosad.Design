using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;
using Wosad.Common.Section.SectionTypes;
using Wosad.Steel.AISC.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.J_Connections
{
    public class BeamCopeSingle: BeamCopeBase
    {
        public BeamCopeSingle(double c, double d_c, ISectionI Section, ISteelMaterial Material):
            base(c,d_c,Section,Material)
        {

        }

        private SectionTee _tee;

        public SectionTee tee
        {
            get {
                if (_tee == null)
                {
                    GetCopeSection();
                 }
                return _tee; }
 
        }
        

        private void GetCopeSection()
        {
             _tee = new SectionTee(null, d - d_c, Section.FlangeWidthTop, Section.FlangeThicknessBottom, Section.WebThickness);
        }

        public double GetPlateBucklingModelAdjustmentFactor()
        {
            double f;
            if (this.c/d <=1)
            {
                f = (2 * c) / d;
            }
            else
            {
                f = 1.0 + c / d;
            }
            return f;
        }


        public double GetPlateBucklingCoefficient()
        {
            double k;
            if (c/h_o<=1)
            {
                k = 2.2 * Math.Pow((((h_o) / (c))), 1.65);
            }
            else
            {
                k = ((2.2 * h_o) / (c));
            }
            return k;
        }
        protected override double GetS_net()
        {
            double S_top = tee.SectionModulusXTop;
            double S_bot = tee.SectionModulusXBot;
            return Math.Min(S_top, S_bot);
        }

        protected override double GetF_cr()
        {
            double f = GetPlateBucklingModelAdjustmentFactor();
            double k = GetPlateBucklingCoefficient();

            double F_cr ;
            bool PermissibleCopeGeometry = CheckCopeGeometry();
            if (PermissibleCopeGeometry == true)
            {
                F_cr = 26210 * Math.Pow((((t_w) / (h_o))), 2) * f * k;
            }
            else
            {
                F_cr = GetFcrGeneral();
            }
            
            return F_cr;
        }

        protected override double GetZ_net()
        {
            return tee.PlasticSectionModulusX;
        }

        protected override double Get_h_o()
        {
            return tee.Height - d_c;
        }

        public override double Get_t_w()
        {
            return tee.WebThickness;
        }
    }
}
