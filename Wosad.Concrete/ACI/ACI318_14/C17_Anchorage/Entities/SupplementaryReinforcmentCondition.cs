using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Concrete.ACI318_11.Anchorage
{

//  Condition A applies where the potential concrete failure surfaces are crossed by supplementary reinforcement proportioned to tie the potential concrete failure prism into the structural member. 
//  Condition B applies where such supplementary reinforcement is not provided, or where pullout or pryout strength governs.
    
    
    public enum SupplementaryReinforcmentCondition
    {
        A,
        B,
    }
}
