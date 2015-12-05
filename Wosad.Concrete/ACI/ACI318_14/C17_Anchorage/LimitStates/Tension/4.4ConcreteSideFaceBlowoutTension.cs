using Wosad.Concrete.ACI318_11.Anchorage.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage.LimitStates
{
    public class ConcreteSideFaceBlowoutTension : AnchorageConcreteLimitState
    {
        public ConcreteSideFaceBlowoutTension
            (int n, double h_eff, AnchorInstallationType AnchorType
            )
            : base(n, h_eff, AnchorType)
        {

        }
        public override double GetNominalStrength()
        {
            double Nsb =   GetNsb ();
            double Nsbg = GetNsbg(Nsb);

            double phi = GetPhi();
            double phiNsbg = phi*Nsbg;
            return phiNsbg;

        }

        private double GetPhi()
        {
            //=if(B5=Z4,if(AnchorType=O5,0.75,if(B6=AA4,0.75,if(B6=AA5,0.65,0.55))),if(AnchorType=O5,0.7,if(B6=AA4,0.65,if(B6=AA5,0.55,0.45))))
            throw new NotImplementedException();
        }

        private double GetNsbg(double Nsb)
        {
            //=if(MultipleHeadedCloseToEdge=S4,noOfSuchAnchors(1+s_0/6/ca_MIN)+(B7-noOfSuchAnchors)*if(ca_MIN<0.4*h_eff,160*ca_MIN*l*SQRT(Abrg*fc)/1000,10000),B7*Nsb)
            throw new NotImplementedException();
        }

        private double GetNsb()
        {
            //=if(ca_MIN<0.4*h_eff,160*ca_MIN*l*SQRT(Abrg*fc)/1000*if(ca_2<3*ca_MIN,(1+ca_2/ca_MIN)/4,1),10000)
            throw new NotImplementedException();
        }
    }
}
