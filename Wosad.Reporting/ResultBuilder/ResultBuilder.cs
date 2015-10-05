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
using System.IO;
using System.Linq;
using System.Text;
using Wosad.Common.CalculationLogger.Interfaces;

namespace Wosad.Reporting.ResultBuilder
{
    public class ResultBuilder
    {
        public byte[] BuildResultStream(List<ICalcLog> Log, CalculatorOutputType OutputType)
        {
            string tempfolder = Path.GetTempPath();
            string tempDocxFile = Path.ChangeExtension(Path.GetRandomFileName(), "docx");
            string tempWordFilePath = Path.Combine(tempfolder, tempDocxFile);

            WordFileBuilder.CreateWordFile(Log, tempWordFilePath);

            
            MemoryStream ms = new MemoryStream();
            try
            {
                using (FileStream fs = File.OpenRead(tempWordFilePath))
                {
                    fs.CopyTo(ms);
                }

                // add here other reports type if applicable
            }
            catch (Exception ex)
            {

            }

            if (File.Exists(tempWordFilePath))
            {
                File.Delete(tempWordFilePath);
            }

            var ret = ms.ToArray();
            ms.Close();

            return ret;
        }
    }
}
