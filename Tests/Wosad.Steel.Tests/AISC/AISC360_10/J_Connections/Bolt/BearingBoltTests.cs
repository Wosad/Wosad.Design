using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.AISC360_10.Connections.Bolted;
using Wosad.Steel.AISC.Code;
using Wosad.Steel.AISC.SteelEntities.Bolts;

namespace Wosad.Steel.Tests.AISC.AISC360_10.J_Connections.Bolt
{

    public class BearingBoltTests
    {
        public void BearingBoltGroupAReturnsTensileStrength()
        {
            BoltBearingGroupA bolt = new BoltBearingGroupA(7.0 / 8.0, BoltThreadType.Included, SteelDesignFormat.LRFD, null);
            double phiPn = bolt.GetTensileCapacity();
        }
        public void BearingBoltGroupBReturnsTensileStrength()
        {
            BoltBearingGroupB bolt = new BoltBearingGroupB(7.0 / 8.0, BoltThreadType.Included, SteelDesignFormat.LRFD, null);
            double phiPn = bolt.GetTensileCapacity();
        }

        public void BearingBoltGroupAReturnsShearStrength()
        {
            BoltBearingGroupA bolt = new BoltBearingGroupA(7.0 / 8.0, BoltThreadType.Included, SteelDesignFormat.LRFD, null);
            double phiVn = bolt.GetShearCapacity();
        }                                   
        public void BearingBoltGroupBReturnsShearStrength()
        {
            BoltBearingGroupB bolt = new BoltBearingGroupB(7.0 / 8.0, BoltThreadType.Included, SteelDesignFormat.LRFD, null);
            double phiVn = bolt.GetShearCapacity();
        }
    }
}
