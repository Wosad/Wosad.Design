using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Steel.AISC
{
    public class ComboForceException:  ApplicationException
    {
        public ComboForceException(string CaseId)
        {
            errorString = CaseId;
        }

        string errorString;

        // Override the Exception.Message property.
        public override string Message
        {
            get
            {
                return string.Format("Selected load combination {0} is not consistent with load values.", errorString);
            }
        }
    
    }
}
