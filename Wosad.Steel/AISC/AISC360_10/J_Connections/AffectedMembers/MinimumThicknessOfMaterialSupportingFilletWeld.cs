using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.SteelEntities;

namespace Wosad.Steel.AISC360_10.Connections.AffectedElements
{
    public partial class AffectedElement : SteelDesignElement
    {
        public double GetMinimumThicknessOfMaterialSupportingFilletWeld(double w_weld, double F_u)
        {
           double t_min = ((3.09 * w_weld) / (16 * F_u));
           return t_min;
        }
    }
}
