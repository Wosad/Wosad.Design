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
