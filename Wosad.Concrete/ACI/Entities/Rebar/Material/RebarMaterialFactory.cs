using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Concrete.ACI.Entities;

namespace Wosad.Concrete.ACI
{
    public class RebarMaterialFactory
    {
        IRebarMaterial GetMaterial(string SpecificationId)
        {
            switch (SpecificationId)
            {
                case "A615Grade40":
                    return new MaterialAstmA615(A615Grade.Grade40);
                    break;
                case "A615Grade60":
                    return new MaterialAstmA615(A615Grade.Grade60);
                    break;
                case "A615Grade75":
                    return new MaterialAstmA615(A615Grade.Grade75);
                    break;
                case "A706":
                    return new MaterialAstmA706();
                    break;
                case "A416Grade250LRS":
                    return new MaterialAstmA416(A416Grade.Grade250, StrandType.LowRelaxation);
                case "A416Grade250SR":
                    return new MaterialAstmA416(A416Grade.Grade250, StrandType.StressRelieved);
                    break;
                case "A416Grade270LRS":
                    return new MaterialAstmA416(A416Grade.Grade270, StrandType.LowRelaxation);
                case "A416Grade270SR":
                    return new MaterialAstmA416(A416Grade.Grade270, StrandType.StressRelieved);
                    break;
                default:
                    return new MaterialAstmA615(A615Grade.Grade60);
                    break;
            }
        }
    }
}
