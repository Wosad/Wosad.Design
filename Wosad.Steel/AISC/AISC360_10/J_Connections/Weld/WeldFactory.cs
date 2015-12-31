using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Steel.AISC.Entities.Welds.Interfaces;

namespace Wosad.Steel.AISC.AISC360_10.Connections.Weld
{
    public class WeldFactory
    {

        public IWeld GetWeld(WeldType weldType,double F_y, double F_u, double F_EXX, double Size, double A_nBase, double Length)
        {
            IWeld weld =null;
            switch (weldType)
            {
                case WeldType.CJP:
                    weld = new CJPGrooveWeld(F_y, F_u, F_EXX, Size, A_nBase, Length);
                    break;
                case WeldType.PJP:
                    weld = new PJPGrooveWeld(F_y, F_u, F_EXX, Size, A_nBase, Length);
                    break;
                case WeldType.Fillet:
                    weld = new FilletWeld(F_y, F_u, F_EXX, Size, A_nBase, Length);
                    break;
            }
            return weld;
        }
    }
}
