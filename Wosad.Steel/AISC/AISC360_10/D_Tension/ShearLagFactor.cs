using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360_10
{
    public class ShearLagFactor
    {
        public double GetShearLagFactor(ShearLagCase Case, double EccentricityOfConnection, double PlateWidth, double LengthOfConnection,
            double MemberWidth, double MemberHeight, int NumberOfBoltLines)
        {
            ShearLagFactorBase shearLagCase;
            switch (Case)
            {
                case ShearLagCase.Case1:
                     shearLagCase = new ShearLagCase1();
                    break;
                case ShearLagCase.Case2:
                    shearLagCase = new ShearLagCase2(EccentricityOfConnection,LengthOfConnection);
                    break;
                case ShearLagCase.Case3:
                    shearLagCase = new ShearLagCase3();
                    break;
                case ShearLagCase.Case4:
                    shearLagCase = new ShearLagCase4(PlateWidth,LengthOfConnection);
                    break;
                case ShearLagCase.Case5:
                    shearLagCase = new ShearLagCase5(MemberWidth, LengthOfConnection);
                    break;
                case ShearLagCase.Case6a:
                     shearLagCase = new ShearLagCase6(true,MemberWidth,MemberHeight,LengthOfConnection);
                    break;
                case ShearLagCase.Case6b:
                    shearLagCase = new ShearLagCase6(false, MemberWidth, MemberHeight, LengthOfConnection);
                    break;
                case ShearLagCase.Case7a:
                    shearLagCase = new ShearLagCase7(2,MemberHeight,PlateWidth,EccentricityOfConnection,LengthOfConnection);
                    break;
                case ShearLagCase.Case7b:
                    shearLagCase = new ShearLagCase7(3, MemberHeight, PlateWidth, EccentricityOfConnection, LengthOfConnection);
                    break;
                case ShearLagCase.Case7c:
                    shearLagCase = new ShearLagCase7(4, MemberHeight, PlateWidth, EccentricityOfConnection, LengthOfConnection);
                    break;
                case ShearLagCase.Case8a:
                    shearLagCase = new ShearLagCase8(2, EccentricityOfConnection, LengthOfConnection);
                    break;
                case ShearLagCase.Case8b:
                    shearLagCase = new ShearLagCase8(3, EccentricityOfConnection, LengthOfConnection);
                    break;
                case ShearLagCase.Case8c:
                    shearLagCase = new ShearLagCase8(4, EccentricityOfConnection, LengthOfConnection);
                    break;
                default:
                    shearLagCase = new ShearLagCase2(EccentricityOfConnection, LengthOfConnection);
                    break;
            }
            return shearLagCase.GetShearLagFactor();
        }

    }
}
