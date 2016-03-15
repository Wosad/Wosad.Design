using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wosad.Common.Exceptions
{

    public class ShapeTypeNotSupportedException : ApplicationException
    {

        public ShapeTypeNotSupportedException()
        {
            typeString = "";
        }

        public ShapeTypeNotSupportedException(string ActionType)
        {

            typeString = string.Format(" for {0} calculation", ActionType);
        }

        string typeString;

        public override string Message
        {
            get
            {
                return string.Format("Section type not supported{0}.", typeString);
            }
        }
    }
}
