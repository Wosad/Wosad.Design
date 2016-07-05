using System;
using Wosad.Steel.AISC.Steel.Entities;

namespace Wosad.Steel.AISC360v10.HSS.ConcentratedForces
{
    public interface IHssTransversePlateConnection
    {
        SteelLimitStateValue GetLocalCripplingAndYieldingStrengthOfHss();
        SteelLimitStateValue GetLocalPunchingStrengthOfPlate();
        SteelLimitStateValue GetLocalYieldingStrengthOfPlate();
    }
}
