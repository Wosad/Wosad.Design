using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Entities;

namespace Wosad.Steel.AISC.AISC360_10.Combination
{
    public class Combination: AnalyticalElement
    {
        public double GetInteractionRatio
            (
            CombinationCaseId comboCaseId,
            double N_u,
            double T_uTorsion,
            double M_ux,
            double M_uy,
            double V_ur,
            double phiN_n,
            double phiT_nTorsion,
            double phiM_x,
            double phiM_y,
            double phiV_rn,
            double ForceTolerance = 0.05
            )
        {
            double N = Math.Abs(N_u/phiN_n);
            double T = Math.Abs(T_uTorsion/phiT_nTorsion);
            double Mx =Math.Abs( M_ux/phiM_x);
            double My = Math.Abs(M_uy/phiM_y);
            double V = Math.Abs(V_ur/phiV_rn);

            double tf = N_u * ForceTolerance; //threshholdForce
            double tm = M_ux * ForceTolerance; //threshholdMoment
            double InteractionRatio = 1;
            double InteractionRatioPMM = 1;
            double InteractionRatioV = 1;

            switch (comboCaseId)
	        {
		        case CombinationCaseId.H1:
                    InteractionRatioV = T+V;
                    if (N>=0.2)
                    {
                        InteractionRatioPMM = N+8.0/9.0*(Mx+My); //H1-1a
                    }
                    else
                    {
                        InteractionRatioPMM = N / 2 + (Mx + My);  //H1-1b
                    }
                    InteractionRatio = Math.Min(Math.Abs(InteractionRatioPMM), Math.Abs(InteractionRatioV));
                 break;
                case CombinationCaseId.H2:
                 InteractionRatioPMM = N + Mx + My;
                 InteractionRatioV = T + V;
                 InteractionRatio = Math.Min(Math.Abs(InteractionRatioPMM), Math.Abs(InteractionRatioV));
                 break;
                case CombinationCaseId.H3:
                    InteractionRatioPMM = N / 2 + (Mx + My);  //H2-1
                 break;
                case CombinationCaseId.Linear:
                 InteractionRatio = N + Mx + My+T+V;
                 break;
                case CombinationCaseId.Elliptical:
                 InteractionRatio = Math.Pow(N, 2) + Math.Pow(M_ux, 2) + Math.Pow(M_uy, 2) + Math.Pow(V, 2) + Math.Pow(T, 2);
                 break;
                case CombinationCaseId.Anchorage:
                 InteractionRatio = Math.Pow(N, 5.0 / 3.0) + Math.Pow(M_ux, 5.0 / 3.0) + Math.Pow(M_uy, 5.0 / 3.0) + Math.Pow(V, 5.0 / 3.0) + Math.Pow(T, 5.0 / 3.02);
                 break;
                case CombinationCaseId.Plastic:
                  // BO DOWSWELL
                 //Plastic Strength of Connection Elements
                 //ENGINEERING JOURNAL / FIRST QUARTER / 2015 
                 InteractionRatio = Math.Pow(N, 2) + Math.Pow(M_ux, 1.7) + Math.Pow(M_uy,1.7) + Math.Pow(V, 4) + Math.Pow(T, 2);
                 break;
                default:
                 InteractionRatio = Math.Pow(N, 2) + Math.Pow(M_ux, 1.7) + Math.Pow(M_uy, 1.7) + Math.Pow(V, 4) + Math.Pow(T, 2);
                 break;
	        }

            return InteractionRatio;
        }
    }
}
