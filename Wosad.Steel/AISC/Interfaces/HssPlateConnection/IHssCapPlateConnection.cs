using System;
using Wosad.Steel.AISC.Steel.Entities;
namespace Wosad.Steel.AISC360v10.HSS.ConcentratedForces
{
    public interface IHssCapPlateConnection
    {
        SteelLimitStateValue GetHssYieldingOrCrippling();
        double t_plCap { get; set; }
    }
}
