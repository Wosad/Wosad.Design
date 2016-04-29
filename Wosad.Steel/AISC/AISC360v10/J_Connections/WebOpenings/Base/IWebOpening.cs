using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC.AISC360v10.Connections.WebOpenings
{
    public interface IWebOpening
    {
        double GetShearStrength();
        double GetFlexuralStrength();
    }
}
