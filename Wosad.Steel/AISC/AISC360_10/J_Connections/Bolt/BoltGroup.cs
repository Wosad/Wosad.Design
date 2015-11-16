using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.J_Connections
{
    //Inherits from base class. Added here for convenience
    public class BoltGroup : Wosad.Steel.AISC.SteelEntities.Bolts.BoltGroup
    {
        public BoltGroup(List<ILocationArrayElement> Bolts):base (Bolts)
        {

        }
    }
}
