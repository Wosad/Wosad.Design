#region Copyright
   /*Copyright (C) 2015 Wosad Inc

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
   */
#endregion
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
            double N = N_u       ==0 ? 0 : Math.Abs(N_u/phiN_n);
            double T = T_uTorsion==0 ? 0 : Math.Abs(T_uTorsion/phiT_nTorsion);
            double Mx =M_ux      ==0 ? 0 : Math.Abs( M_ux/phiM_x);
            double My= M_uy      ==0 ? 0 : Math.Abs(M_uy/phiM_y);
            double V = V_ur      ==0 ? 0 : Math.Abs(V_ur/phiV_rn);

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
