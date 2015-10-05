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
    public class DevelopmentCompressionValues
    {
        public const string ldc = "ldc";
        public const string ldc1 = "ldc1";
        public const string ldc2 = "ldc2";
    }

    public class DevelopmentCompressionDescriptions
    {
        public const string ldSimplified = "ldc";
        public struct c12
        {
            public struct s3
            {
                public struct _2
                {

                }
                public struct _3
                {
                    public const string ExcessFactor = "12.3.3-a";
                    public const string EnclosedFactor = "12.3.3-b";
                }
            }
        }
    }

    public class DevelopmentCompressionFormulas
    {
        public struct c12
        {
            public struct s3
            {
                public struct _2
                {

                }
                public struct _3
                {
                    
                }
            }
        }


    }
    public class DevelopmentCompressionParagraphs
    {
        public struct c12
        {
            public struct s3
            {
                public struct _2
                {
                    public const string F1and2 = "p-12.3.2";
                }
                public struct _3
                {
                    public const string a = "p-12.3.3-a";
                    public const string b = "p-12.3.3-b";
                }
            }
        }
    }
}
