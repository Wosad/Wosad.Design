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
using Wosad.Common.Entities;
using Wosad.Concrete.ACI.Infrastructure.Entities;
using Wosad.Concrete.ACI.Infrastructure.Entities.Rebar;
using Wosad.Common.Entities;
using Wosad.Common.CalculationLogger.Interfaces;
using Wosad.Common.Interfaces;

namespace Wosad.Concrete.ACI
{
    public abstract partial class ConcreteSectionBase : AnalyticalElement, IConcreteMember //IStructuralMember,
	{

		public ConcreteSectionBase(IConcreteSection Section, List<RebarPoint> LongitudinalBars, ICalcLog CalcLog)
			: base(CalcLog)
		{
			this.Section = Section;
			this.longitBars = LongitudinalBars;
		}
		//public List<IMemberForce> Forces { get; set; }
		public IConcreteSection Section { get; set; }

		private List<RebarPoint> longitBars;

		public List<RebarPoint> LongitudinalBars
		{
			get { return longitBars; }
			set { longitBars = value; }
		}
		
        //public List<IMemberForce> GetForce(string LoadCaseName)
        //{
        //    var f = Forces.Where(a => a.LoadCaseName == LoadCaseName).ToList();
        //    if (f == null)
        //    {
        //        throw new Exception("Member force for load combination not found");
        //    }

        //    return f;
        //}
	}
}
