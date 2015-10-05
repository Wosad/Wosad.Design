#region Copyright
   /*Copyright (C) 2015 Wosad Inc

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

   http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
   */
#endregion
 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Wosad.Concrete.ACI318_11
{
    public class TensionHookValues
    {
        public const string ldh = "ldh";
        public const string ksi_e = "ksi_e";
    }

    public class TensionHookDescriptions
    {
        public const string ldh = "ldh";
        public const string ldhMin = "ldhMin";
        public const string ksi_e = "ksi_e";
        public const string lambda = "lambda";
    }

    public class TensionHookFormulas
    {
        public class _5
        {
            public const string _2_2 = "P-12.5.2-2";
        }
    }
    public class TensionHookParagraphs
    {
        public class _5
	    {
            public const string _1_1 = "P-12.5.1-1";
            public const string _2_1 = "P-12.5.2-1";
	    }
    }
}
