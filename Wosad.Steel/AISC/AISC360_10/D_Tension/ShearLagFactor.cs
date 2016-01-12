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

namespace Wosad.Steel.AISC.AISC360_10
{
    public class ShearLagFactor
    {

        public double GetShearLagFactor(ShearLagCase Case, double x_b, double t_p, double l,
            double B, double H)
        {
            ShearLagFactorBase shearLagCase;
            switch (Case)
            {
                case ShearLagCase.Case1:
                     shearLagCase = new ShearLagCase1();
                    break;
                case ShearLagCase.Case2:
                    shearLagCase = new ShearLagCase2(x_b,l);
                    break;
                case ShearLagCase.Case3:
                    shearLagCase = new ShearLagCase3();
                    break;
                case ShearLagCase.Case4:
                    shearLagCase = new ShearLagCase4(t_p,l);
                    break;
                case ShearLagCase.Case5:
                    shearLagCase = new ShearLagCase5(B, l);
                    break;
                case ShearLagCase.Case6a:
                     shearLagCase = new ShearLagCase6(true,B,H,l);
                    break;
                case ShearLagCase.Case6b:
                    shearLagCase = new ShearLagCase6(false, B, H, l);
                    break;
                case ShearLagCase.Case7a:
                    shearLagCase = new ShearLagCase7(2,H,t_p,x_b,l);
                    break;
                case ShearLagCase.Case7b:
                    shearLagCase = new ShearLagCase7(3, H, t_p, x_b, l);
                    break;
                case ShearLagCase.Case7c:
                    shearLagCase = new ShearLagCase7(4, H, t_p, x_b, l);
                    break;
                case ShearLagCase.Case8a:
                    shearLagCase = new ShearLagCase8(2, x_b, l);
                    break;
                case ShearLagCase.Case8b:
                    shearLagCase = new ShearLagCase8(3, x_b, l);
                    break;
                case ShearLagCase.Case8c:
                    shearLagCase = new ShearLagCase8(4, x_b, l);
                    break;
                default:
                    shearLagCase = new ShearLagCase2(x_b, l);
                    break;
            }
            return shearLagCase.GetShearLagFactor();
        }

    }
}
