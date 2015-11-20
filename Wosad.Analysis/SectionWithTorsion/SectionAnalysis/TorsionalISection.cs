using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wosad.Common.Section.Interfaces;

namespace Wosad.Analysis.SectionWithTorsion.SectionAnalysis
{
    public class TorsionalISection
    {
        ISectionI section;
        public TorsionalISection(ISectionI section)
        {
            this.section = section;
        }
    }
}
