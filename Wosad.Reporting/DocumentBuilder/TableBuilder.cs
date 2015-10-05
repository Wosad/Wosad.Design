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
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Wosad.Reporting.ResultBuilder
{
    public class TableBuilder
    {
        public static void AppendTableWithData(WordprocessingDocument doc, string TableTitle, List<List<string>> data)
        {
            Body bod = doc.MainDocumentPart.Document.Body;

            Table table = bod.Descendants<Table>().Where(tbl => tbl.InnerText.Contains(TableTitle)).FirstOrDefault();

            if (table!=null)
            {
                foreach (var RowList in data)
                {
                     //var tr = new TableRow();
                    //TableRow tr = table.Elements<TableRow>().Last();
                    //TableRow rowCopy = (TableRow)tr.CloneNode(true);
                    //rowCopy.AppendChild(new TableCell(new Paragraph(new Run(new Text("test")))));
                    TableRow tr = new TableRow();
                     foreach (var cellElement in RowList)
                     {
                         // Add a cell to each column in the row.
                         TableCell tc = new TableCell(new Paragraph(new Run(new Text(cellElement))));
                         tr.Append(tc);
                     }
                     table.AppendChild(tr);
                }
            }

        }
    }
}
