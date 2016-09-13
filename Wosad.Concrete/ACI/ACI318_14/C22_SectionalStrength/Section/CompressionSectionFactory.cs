using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.CalculationLogger;
using Wosad.Common.Mathematics;
using Wosad.Concrete.ACI318_14;

namespace Wosad.Concrete.ACI.ACI318_14
{
    public class CompressionSectionFactory
    {

        public ConcreteSectionCompression GetCompressionMember(ConcreteSectionFlexure flexuralSection,
        CompressionMemberType CompressionMemberType)
        {
            
            CalcLog log = new CalcLog();

            ConcreteSectionCompression compSection = new ConcreteSectionCompression(flexuralSection, CompressionMemberType, log);
            return compSection;
        }
      }

    
}
