﻿#region Copyright
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

namespace Wosad.Common.CalculationLogger.Interfaces
{
    public interface ICalcLogEntry
    {
        void AddDependencyValue(string Key, string value);
        void AddDependencyValue(string Key, double value);
        Dictionary<string, string> GetDependencyValues();
        string DescriptionReference { get; set; }
        string FormulaID { get; set; }
        string Reference { get; set; }
        string VariableValue { get; set; }
        string ValueName { get; set; }
        Dictionary<string, string> DependencyValues { get; set; }
        List<List<string>> TableData { get; set; }
        string TemplateTableTitle { get; set; }
    }
}
