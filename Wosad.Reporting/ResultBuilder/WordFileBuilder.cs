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
 
using System.Collections.Generic;
using System.IO;
using DocumentFormat.OpenXml.Packaging;
using System.Xml.Linq;
using Wosad.Common.CalculationLogger.Interfaces;


namespace Wosad.Reporting.ResultBuilder
{
    public class WordFileBuilder
    {
        private static XNamespace ns = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";

        public static void CreateWordFile(List<ICalcLog> Logs, string OutputFilename)
        {
            TemplateResourceLocator locator = new TemplateResourceLocator();
            //List<Source> CalculationParts = new List<Source>();

            string tempfolder = Path.GetTempPath();


            using (WordprocessingDocument output = WordprocessingDocument.Create(OutputFilename, DocumentFormat.OpenXml.WordprocessingDocumentType.Document))
            {
                DocumentBuilder.InitializeNewDocument(output);
                foreach (var logItem in Logs)
                {
                    var EntriesList = logItem.GetEntriesList();

                    foreach (var entry in EntriesList)
                    {
                        var fileMemoryStream = locator.GetResourceStream(entry);
                        if (fileMemoryStream != null)
                        {
                            string tempDocxFile = Path.ChangeExtension(Path.GetRandomFileName(), "docx");
                            string tempWordFilePath = Path.Combine(tempfolder, tempDocxFile);

                            using (var fileStream = new FileStream(tempWordFilePath, FileMode.CreateNew, FileAccess.ReadWrite))
                            {
                                fileMemoryStream.CopyTo(fileStream); // fileStream is not populated
                            }

                            using (WordprocessingDocument blockDocument = WordprocessingDocument.Open(tempWordFilePath, true))
                            {
                                Dictionary<string, string> DependencyValues = entry.GetDependencyValues();

                                //do all dependecy variables
                                // - the variables that are in the formula but are on the right hand side of the equation
                                // in other words in a=(b+c) "b" and "c" are the dependency variables 

                                foreach (var pair in DependencyValues)
                                {
                                    string ReplaceDependencyString = "~" + pair.Key;
                                    SearchAndReplacer.SearchAndReplace(blockDocument, ReplaceDependencyString, pair.Value.ToString(), true);
                                }

                                //now replace the final result 
                                // in other words in a=(b+c)=[result]  ... "[result]" is being replaced

                                string ReplaceValueString = "~" + entry.ValueName;

                                SearchAndReplacer.SearchAndReplace(blockDocument, ReplaceValueString, entry.VariableValue, true);

                                //if entry contains a table look for table template
                                //and add rows

                                if (entry.TableData != null && entry.TemplateTableTitle != null)
                                {
                                    TableBuilder.AppendTableWithData(blockDocument, entry.TemplateTableTitle, entry.TableData);
                                }

                                blockDocument.FlushParts();
                                blockDocument.MainDocumentPart.Document.Save();
                                blockDocument.Close();

                                // add this calc block to sources collection
                            }
                            using (WordprocessingDocument blockDocument = WordprocessingDocument.Open(tempWordFilePath, true))
                            {

                                bool lastKeepSections = false;
                                IEnumerable<XElement> blockDocumentContents =
                                    blockDocument.MainDocumentPart.GetXDocument().Descendants(ns + "body").Elements();
                                List<ImageData> images = new List<ImageData>();
                                output.AppendDocument(blockDocument, blockDocumentContents, false, lastKeepSections, images);
                            }
                            if (File.Exists(tempWordFilePath))
                            {
                                File.Delete(tempWordFilePath);
                            }

                        }

                    }
                }
                output.FlushParts();
            } //end of using statement
        }
    }
}
