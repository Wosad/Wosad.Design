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
    public class TensionDevelopmentValues
    {
        public const string ld = "ld";
        public const string ksi_e = "ksi_e";
        public const string ksi_t = "ksi_t";
        public const string ksi_s = "ksi_s";
        public const string ksi_t_ksi_e_Product = "ksi_t_ksi_e_Product";
        public const string cc = "cc";
        public const string cl_s = "cl_s";
        public const string bar_surface_distance_center = "bar_surface_distance_center";
        public const string bar_half_distance_center = "bar_half_distance_center";
        public const string Ktr = "Ktr";
        public const string cb = "cb";
    }

    public class TensionDevelopmentDescriptions
    {
        public const string ldSimplified = "ldSimplified";
        public const string ldGeneral = "ldGeneral";
        public const string ldhMin = "ldhMin";
        public const string ksi_e_CoatedLargeSpacing = "ksi_e_CoatedLargeSpacing";
        public const string ksi_e_CoatedSmallSpacing = "ksi_e_CoatedSmallSpacing";
        public const string ksi_e_Uncoated = "ksi_e_Uncoated";
        public const string ksi_t_Top = "ksi_t_Top";
        public const string ksi_t_Bottom = "ksi_t_Bottom";
        public const string ksi_s_SmallDiameter = "ksi_s_SmallDiameter";
        public const string ksi_s_LargeDiameter = "ksi_s_LargeDiameter";
        public const string ksi_t_ksi_e_Product = "ksi_t_ksi_e_Product";
        public const string cb = "cb";
        public const string Ktr = "Ktr";
    }

    public class TensionDevelopmentFormulas
    {
        public const string _12_1 = "12-1";
        public const string ksi_e = "ksi_e";
        public const string ksi_t = "ksi_t";
        public const string ksi_s = "ksi_s";
        public const string ksi_t_ksi_e_Product = "ksi_t_ksi_e_Product";
        public const string cb = "cb"; //add the minimum of 2 values statement

    }
    public class TensionDevelopmentParagraphs
    {
        public class _2
	    {
            //public const string _4_1 = "P-12.2.4-1";
            //public const string _2_1 = "P-12.5.2-1";
	    }
    }
}
