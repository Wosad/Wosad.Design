using System;
using Wosad.Steel.AISC.SteelEntities;
namespace Wosad.Steel.AISC.AISC360v10.K_HSS.TrussConnections



{
    public interface IHssTrussBranchConnection
    {
        SteelLimitStateValue GetBranchPunchingStrength();
        SteelLimitStateValue GetBranchYieldingFromUnevenLoadDistributionStrength(bool IsMainBranch);
        SteelLimitStateValue GetChordSidewallLocalCripplingStrength();
        SteelLimitStateValue GetChordSidewallLocalYieldingStrength();
        SteelLimitStateValue GetChordSidewallShearStrength();
        SteelLimitStateValue GetChordWallPlastificationStrength();
    }
}
