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
using System.IO;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;


namespace Wosad.Reporting.ResultBuilder
{
    class DocProc
    {
        static void Main(string[] args)
        {
            using (WordprocessingDocument part1 = WordprocessingDocument.Open("Source1.docx", false))
            {
                List<Source> sources = new List<Source>();
                sources.Add(new Source(part1, 16, 37, true));
                DocumentBuilder.BuildDocument(sources, "Test1.docx");
            }
            using (WordprocessingDocument part1 = WordprocessingDocument.Open("Source1.docx", false))
            {
                List<Source> sources = new List<Source>();
                sources.Add(new Source(part1, 0, 16, true));
                sources.Add(new Source(part1, 53, true));
                DocumentBuilder.BuildDocument(sources, "Test2.docx");
            }
            using (WordprocessingDocument part1 = WordprocessingDocument.Open("Source2.docx", false))
            using (WordprocessingDocument part2 = WordprocessingDocument.Open("Source1.docx", false))
            {
                List<Source> sources = new List<Source>();
                sources.Add(new Source(part1, true));
                sources.Add(new Source(part2, true));
                DocumentBuilder.BuildDocument(sources, "Test3.docx");
            }
            using (WordprocessingDocument part1 = WordprocessingDocument.Open("Source1.docx", false))
            {
                List<Source> sources = new List<Source>();
                sources.Add(new Source(part1, 16, 37, true));
                using (MemoryStream mem = new MemoryStream())
                {
                    using (WordprocessingDocument newDoc = DocumentBuilder.BuildOpenDocument(sources, mem))
                    {
                        // Modify the document as desired
                        // RevisionAccepter.AcceptRevisions(newDoc);
                    }
                    mem.Seek(0, SeekOrigin.Begin);
                    using (FileStream fs = new FileStream("Test4.docx", FileMode.Create))
                        mem.WriteTo(fs);
                }
            }
        }
    }
}
