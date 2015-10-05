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
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

namespace Wosad.Reporting.ResultBuilder
{
	public class SearchAndReplacer
	{
		public static XmlDocument GetXmlDocument(OpenXmlPart part)
		{
			XmlDocument xmlDoc = new XmlDocument();
			using (Stream partStream = part.GetStream())
			using (XmlReader partXmlReader = XmlReader.Create(partStream))
				xmlDoc.Load(partXmlReader);
			return xmlDoc;
		}

		public static void PutXmlDocument(OpenXmlPart part, XmlDocument xmlDoc)
		{
			using (Stream partStream = part.GetStream(FileMode.Create, FileAccess.Write))
			using (XmlWriter partXmlWriter = XmlWriter.Create(partStream))
				xmlDoc.Save(partXmlWriter);
		}

		static void SearchAndReplaceExactRunInMathSubParagraph(XmlElement paragraph, string search, string replace)
		{

			//Make sure that w namespace is defined
			XmlDocument xmlDoc = paragraph.OwnerDocument;
			string wordNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
			nsmgr.AddNamespace("w", wordNamespace);

			//Make sure that w namespace is defined
			string mathNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/math";
			nsmgr.AddNamespace("m", mathNamespace);


			//Search for text in all paragraphs by "gluing" all inner texts
			XmlNodeList paragraphText = paragraph.SelectNodes("descendant::m:t", nsmgr);
			StringBuilder sb = new StringBuilder();
			foreach (XmlNode text in paragraphText)
			{
				if (((XmlElement)text).InnerText==search)
				{
					#region Replace part
					XmlElement newRun = xmlDoc.CreateElement("m:r", wordNamespace);
					XmlElement newTextElement = xmlDoc.CreateElement("m:t", wordNamespace);
					XmlText newText = xmlDoc.CreateTextNode(replace);
					newTextElement.AppendChild(newText);
					newRun.AppendChild(newTextElement);
					XmlNode parentNode = text.ParentNode;
					parentNode.InsertAfter(newRun, text);
					parentNode.RemoveChild(text);
					#endregion

				}
			}
		}

		static void SearchAndReplaceInMathSubParagraph(XmlElement paragraph, string search, string replace, bool matchCase)
		{

			//Make sure that w namespace is defined
			XmlDocument xmlDoc = paragraph.OwnerDocument;
			string wordNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
			nsmgr.AddNamespace("w", wordNamespace);

			//Make sure that w namespace is defined
			string mathNamespace = "http://schemas.openxmlformats.org/officeDocument/2006/math";
			nsmgr.AddNamespace("m", mathNamespace);



			//Search for text in all paragraphs by "gluing" all inner texts
			XmlNodeList paragraphText = paragraph.SelectNodes("descendant::m:t", nsmgr);
			StringBuilder sb = new StringBuilder();
			foreach (XmlNode text in paragraphText)
			{
			   //KU: all the text is glued together to see if the search string exists
			   sb.Append(((XmlElement)text).InnerText);
			}

			//Check if there is a match 
			if (sb.ToString().Contains(search) || (!matchCase && sb.ToString().ToUpper().Contains(search.ToUpper())))
			{


					#region Break all text into single runs
				XmlNodeList mathText = paragraph.SelectNodes("descendant::m:oMath", nsmgr);

				foreach (XmlElement mText in mathText)
				{
					XmlNodeList runs = mText.SelectNodes("child::m:r", nsmgr);
					foreach (XmlElement run in runs)
					{
						XmlNodeList childElements = run.SelectNodes("child::*", nsmgr);
						if (childElements.Count > 0)
						{
							for (int c = childElements.Count - 1; c >= 0; --c)
							{
								if (childElements[c].Name == "w:rPr") //ignore run properties 
									continue;
								if (childElements[c].Name == "m:t")
								{
									string textElementString = childElements[c].InnerText;
									for (int i = textElementString.Length - 1; i >= 0; --i)
									{
										#region Create a single literal run

										XmlElement newRun = xmlDoc.CreateElement("m:r", mathNamespace);
										XmlElement runProps = (XmlElement)run.SelectSingleNode("child::w:rPr", nsmgr);
										if (runProps != null)
										{
											XmlElement newRunProps = (XmlElement)runProps.CloneNode(true);
											newRun.AppendChild(newRunProps);
										}
										XmlElement newTextElement = xmlDoc.CreateElement("m:t", mathNamespace);
										XmlText newText = xmlDoc.CreateTextNode(textElementString[i].ToString());
										newTextElement.AppendChild(newText);
										if (textElementString[i] == ' ')
										{
											XmlAttribute xmlSpace = xmlDoc.CreateAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
											xmlSpace.Value = "preserve";
											newTextElement.Attributes.Append(xmlSpace);
										}
										newRun.AppendChild(newTextElement);
										mText.InsertAfter(newRun, run);
										#endregion
									}
								}

								else
								{
									//ku: don't know what this is for ...
									//XmlElement newRun = xmlDoc.CreateElement("m:r", mathNamespace);
									//XmlElement runProps = (XmlElement)run.SelectSingleNode("child::w:rPr", nsmgr);
									//if (runProps != null)
									//{
									//    XmlElement newRunProps = (XmlElement)runProps.CloneNode(true);
									//    newRun.AppendChild(newRunProps);
									//}
									//XmlElement newChildElement = (XmlElement)childElements[c].CloneNode(true);
									//newRun.AppendChild(newChildElement);
									//mText.InsertAfter(newRun, run);
								}
							}

							 mText.RemoveChild(run);
						}
					}

				#endregion

					#region Find the matching string of single runs and do the replacement

					while (true)
					{
						bool cont = false;
						runs = mText.SelectNodes("child::m:r", nsmgr);
						for (int i = 0; i <= runs.Count - search.Length; ++i)
						{
							bool match = true;
							for (int c = 0; c < search.Length; ++c)
							{
								XmlElement textElement = (XmlElement)runs[i + c].SelectSingleNode("child::m:t", nsmgr);
								if (textElement == null)
								{
									match = false;
									break;
								}
								if (textElement.InnerText == search[c].ToString())
									continue;
								if (!matchCase && textElement.InnerText.ToUpper() == search[c].ToString().ToUpper())
									continue;
								match = false;
								break;
							}
							if (match)
							{
								XmlElement runProps = (XmlElement)runs[i].SelectSingleNode("descendant::w:rPr", nsmgr);
								XmlElement newRun = xmlDoc.CreateElement("m:r", mathNamespace);
								if (runProps != null)
								{
									XmlElement newRunProps = (XmlElement)runProps.CloneNode(true);
									newRun.AppendChild(newRunProps);
								}
								XmlElement newTextElement = xmlDoc.CreateElement("m:t", mathNamespace);
								XmlText newText = xmlDoc.CreateTextNode(replace);
								newTextElement.AppendChild(newText);
								if (replace[0] == ' ' || replace[replace.Length - 1] == ' ')
								{
									XmlAttribute xmlSpace = xmlDoc.CreateAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
									xmlSpace.Value = "preserve";
									newTextElement.Attributes.Append(xmlSpace);
								}
								newRun.AppendChild(newTextElement);
								mText.InsertAfter(newRun, (XmlNode)runs[i]);
								for (int c = 0; c < search.Length; ++c)
									mText.RemoveChild(runs[i + c]);
								cont = true;
								break;
							}
						}
						if (!cont)
							break;
					}

					#endregion

					#region Consolidate runs

					// Consolidate adjacent runs that have only text elements, and have the
					// same run properties. This isn't necessary to create a valid document,
					// however, having the split runs is a bit messy.
					XmlNodeList children = mText.SelectNodes("child::*", nsmgr);
					List<int> matchId = new List<int>();
					int id = 0;
					for (int c = 0; c < children.Count; ++c)
					{
						if (c == 0)
						{
							matchId.Add(id);
							continue;
						}
						if (children[c].Name == "m:r" &&
							children[c - 1].Name == "m:r" &&
							children[c].SelectSingleNode("m:t", nsmgr) != null &&
							children[c - 1].SelectSingleNode("m:t", nsmgr) != null)
						{
							XmlElement runProps =
								(XmlElement)children[c].SelectSingleNode("w:rPr", nsmgr);
							XmlElement lastRunProps =
								(XmlElement)children[c - 1].SelectSingleNode("w:rPr", nsmgr);
							if ((runProps == null && lastRunProps != null) ||
								(runProps != null && lastRunProps == null))
							{
								matchId.Add(++id);
								continue;
							}
							if (runProps != null && runProps.InnerXml != lastRunProps.InnerXml)
							{
								matchId.Add(++id);
								continue;
							}
							matchId.Add(id);
							continue;
						}
						matchId.Add(++id);
					}

					for (int i = 0; i <= id; ++i)
					{
						var x1 = matchId.IndexOf(i);
						var x2 = matchId.LastIndexOf(i);
						if (x1 == x2)
							continue;
						StringBuilder sb2 = new StringBuilder();
						for (int z = x1; z <= x2; ++z)
							sb2.Append(((XmlElement)children[z]
								.SelectSingleNode("m:t", nsmgr)).InnerText);
						XmlElement newRun = xmlDoc.CreateElement("m:r", mathNamespace);
						XmlElement runProps =
							(XmlElement)children[x1].SelectSingleNode("child::w:rPr", nsmgr);
						if (runProps != null)
						{
							XmlElement newRunProps = (XmlElement)runProps.CloneNode(true);
							newRun.AppendChild(newRunProps);
						}
						XmlElement newTextElement = xmlDoc.CreateElement("m:t", mathNamespace);
						XmlText newText = xmlDoc.CreateTextNode(sb2.ToString());
						newTextElement.AppendChild(newText);
						if (sb2[0] == ' ' || sb2[sb2.Length - 1] == ' ')
						{
							XmlAttribute xmlSpace = xmlDoc.CreateAttribute(
								"xml", "space", "http://www.w3.org/XML/1998/namespace");
							xmlSpace.Value = "preserve";
							newTextElement.Attributes.Append(xmlSpace);
						}
						newRun.AppendChild(newTextElement);
						mText.InsertAfter(newRun, children[x2]);
						for (int z = x1; z <= x2; ++z)
							mText.RemoveChild(children[z]);
					}

					#endregion

				}




				
			}

		}

		static void SearchAndReplaceInParagraph(XmlElement paragraph, string search, string replace, bool matchCase)
		{
            if (replace.Length == 0)
            {
                replace = " ";
            }


                //Make sure that w namespace exists in the XML
                XmlDocument xmlDoc = paragraph.OwnerDocument;
                string wordNamespace = "http://schemas.openxmlformats.org/wordprocessingml/2006/main";
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
                nsmgr.AddNamespace("w", wordNamespace);

                //Search for text in all paragraphs by "gluing" all inner texts
                XmlNodeList paragraphText = paragraph.SelectNodes("descendant::w:t", nsmgr);
                StringBuilder sb = new StringBuilder();
                foreach (XmlNode text in paragraphText) sb.Append(((XmlElement)text).InnerText);

                //Check if there is a match 
                if (sb.ToString().Contains(search) || (!matchCase && sb.ToString().ToUpper().Contains(search.ToUpper())))
                {

                    #region Break all text into single runs
                    XmlNodeList runs = paragraph.SelectNodes("child::w:r", nsmgr);
                    foreach (XmlElement run in runs)
                    {
                        XmlNodeList childElements = run.SelectNodes("child::*", nsmgr);
                        if (childElements.Count > 0)
                        {
                            XmlElement last = (XmlElement)childElements[childElements.Count - 1];
                            for (int c = childElements.Count - 1; c >= 0; --c)
                            {
                                if (childElements[c].Name == "w:rPr")
                                    continue;
                                if (childElements[c].Name == "w:t")
                                {
                                    string textElementString = childElements[c].InnerText;
                                    for (int i = textElementString.Length - 1; i >= 0; --i)
                                    {
                                        XmlElement newRun = xmlDoc.CreateElement("w:r", wordNamespace);
                                        XmlElement runProps = (XmlElement)run.SelectSingleNode("child::w:rPr", nsmgr);
                                        if (runProps != null)
                                        {
                                            XmlElement newRunProps = (XmlElement)runProps.CloneNode(true);
                                            newRun.AppendChild(newRunProps);
                                        }
                                        XmlElement newTextElement = xmlDoc.CreateElement("w:t", wordNamespace);
                                        XmlText newText = xmlDoc.CreateTextNode(textElementString[i].ToString());
                                        newTextElement.AppendChild(newText);
                                        if (textElementString[i] == ' ')
                                        {
                                            XmlAttribute xmlSpace = xmlDoc.CreateAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
                                            xmlSpace.Value = "preserve";
                                            newTextElement.Attributes.Append(xmlSpace);
                                        }
                                        newRun.AppendChild(newTextElement);
                                        paragraph.InsertAfter(newRun, run);
                                    }
                                }
                                else
                                {
                                    XmlElement newRun = xmlDoc.CreateElement("w:r", wordNamespace);
                                    XmlElement runProps = (XmlElement)run.SelectSingleNode("child::w:rPr", nsmgr);
                                    if (runProps != null)
                                    {
                                        XmlElement newRunProps = (XmlElement)runProps.CloneNode(true);
                                        newRun.AppendChild(newRunProps);
                                    }
                                    XmlElement newChildElement = (XmlElement)childElements[c].CloneNode(true);
                                    newRun.AppendChild(newChildElement);
                                    paragraph.InsertAfter(newRun, run);
                                }
                            }
                            paragraph.RemoveChild(run);
                        }
                    }

                    #endregion

                    #region Find the matching string of single runs

                    while (true)
                    {
                        bool cont = false;
                        runs = paragraph.SelectNodes("child::w:r", nsmgr);
                        for (int i = 0; i <= runs.Count - search.Length; ++i)
                        {
                            bool match = true;
                            for (int c = 0; c < search.Length; ++c)
                            {
                                XmlElement textElement = (XmlElement)runs[i + c].SelectSingleNode("child::w:t", nsmgr);
                                if (textElement == null)
                                {
                                    match = false;
                                    break;
                                }
                                if (textElement.InnerText == search[c].ToString())
                                    continue;
                                if (!matchCase && textElement.InnerText.ToUpper() == search[c].ToString().ToUpper())
                                    continue;
                                match = false;
                                break;
                            }
                            if (match)
                            {
                                XmlElement runProps = (XmlElement)runs[i].SelectSingleNode("descendant::w:rPr", nsmgr);
                                XmlElement newRun = xmlDoc.CreateElement("w:r", wordNamespace);
                                if (runProps != null)
                                {
                                    XmlElement newRunProps = (XmlElement)runProps.CloneNode(true);
                                    newRun.AppendChild(newRunProps);
                                }

                                if (replace.Contains("\r\n") == false) //if does not contain line breaks
                                {
                                    XmlElement newTextElement = xmlDoc.CreateElement("w:t", wordNamespace);
                                    XmlText newText = xmlDoc.CreateTextNode(replace);
                                    newTextElement.AppendChild(newText);

                                    if (replace[0] == ' ' || replace[replace.Length - 1] == ' ')
                                    {
                                        XmlAttribute xmlSpace = xmlDoc.CreateAttribute("xml", "space", "http://www.w3.org/XML/1998/namespace");
                                        xmlSpace.Value = "preserve";
                                        newTextElement.Attributes.Append(xmlSpace);

                                    }
                                    newRun.AppendChild(newTextElement);
                                }
                                else
                                {
                                    string[] split = replace.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                    for (int x = 0; x < split.Length; x++)
                                    {
                                        XmlElement newTextElement = xmlDoc.CreateElement("w:t", wordNamespace);
                                        XmlText newText = xmlDoc.CreateTextNode(split[x]);
                                        newTextElement.AppendChild(newText);
                                        newRun.AppendChild(newTextElement);

                                        if (x != split.Length - 1)
                                        {
                                            XmlElement breakElement = xmlDoc.CreateElement("w:br", wordNamespace);
                                            newRun.AppendChild(breakElement);
                                        }
                                    }
                                }
                                paragraph.InsertAfter(newRun, (XmlNode)runs[i]);
                                for (int c = 0; c < search.Length; ++c)
                                    paragraph.RemoveChild(runs[i + c]);
                                cont = true;
                                break;
                            }
                        }
                        if (!cont)
                            break;
                    }

                    #endregion

                    #region Consolidate runs

                    // Consolidate adjacent runs that have only text elements, and have the
                    // same run properties. This isn't necessary to create a valid document,
                    // however, having the split runs is a bit messy.
                    XmlNodeList children = paragraph.SelectNodes("child::*", nsmgr);
                    List<int> matchId = new List<int>();
                    int id = 0;
                    for (int c = 0; c < children.Count; ++c)
                    {
                        if (c == 0)
                        {
                            matchId.Add(id);
                            continue;
                        }
                        if (children[c].Name == "w:r" &&
                            children[c - 1].Name == "w:r" &&
                            children[c].SelectSingleNode("w:t", nsmgr) != null &&
                            children[c - 1].SelectSingleNode("w:t", nsmgr) != null)
                        {
                            XmlElement runProps =
                                (XmlElement)children[c].SelectSingleNode("w:rPr", nsmgr);
                            XmlElement lastRunProps =
                                (XmlElement)children[c - 1].SelectSingleNode("w:rPr", nsmgr);
                            if ((runProps == null && lastRunProps != null) ||
                                (runProps != null && lastRunProps == null))
                            {
                                matchId.Add(++id);
                                continue;
                            }
                            if (runProps != null && runProps.InnerXml != lastRunProps.InnerXml)
                            {
                                matchId.Add(++id);
                                continue;
                            }
                            matchId.Add(id);
                            continue;
                        }
                        matchId.Add(++id);
                    }

                    for (int i = 0; i <= id; ++i)
                    {
                        var x1 = matchId.IndexOf(i);
                        var x2 = matchId.LastIndexOf(i);
                        if (x1 == x2)
                            continue;
                        StringBuilder sb2 = new StringBuilder();
                        for (int z = x1; z <= x2; ++z)
                            sb2.Append(((XmlElement)children[z]
                                .SelectSingleNode("w:t", nsmgr)).InnerText);
                        XmlElement newRun = xmlDoc.CreateElement("w:r", wordNamespace);
                        XmlElement runProps =
                            (XmlElement)children[x1].SelectSingleNode("child::w:rPr", nsmgr);
                        if (runProps != null)
                        {
                            XmlElement newRunProps = (XmlElement)runProps.CloneNode(true);
                            newRun.AppendChild(newRunProps);
                        }
                        XmlElement newTextElement = xmlDoc.CreateElement("w:t", wordNamespace);
                        XmlText newText = xmlDoc.CreateTextNode(sb2.ToString());
                        newTextElement.AppendChild(newText);
                        if (sb2[0] == ' ' || sb2[sb2.Length - 1] == ' ')
                        {
                            XmlAttribute xmlSpace = xmlDoc.CreateAttribute(
                                "xml", "space", "http://www.w3.org/XML/1998/namespace");
                            xmlSpace.Value = "preserve";
                            newTextElement.Attributes.Append(xmlSpace);
                        }
                        newRun.AppendChild(newTextElement);
                        paragraph.InsertAfter(newRun, children[x2]);
                        for (int z = x1; z <= x2; ++z)
                            paragraph.RemoveChild(children[z]);
                    }

                    #endregion
                }
            
            
		}

		public static bool PartHasTrackedRevisions(OpenXmlPart part)
		{
			XmlDocument doc = GetXmlDocument(part);
			string wordNamespace =
				"http://schemas.openxmlformats.org/wordprocessingml/2006/main";
			XmlNamespaceManager nsmgr =
				new XmlNamespaceManager(doc.NameTable);
			nsmgr.AddNamespace("w", wordNamespace);
			string xpathExpression =
				"descendant::w:cellDel|" +
				"descendant::w:cellIns|" +
				"descendant::w:cellMerge|" +
				"descendant::w:customXmlDelRangeEnd|" +
				"descendant::w:customXmlDelRangeStart|" +
				"descendant::w:customXmlInsRangeEnd|" +
				"descendant::w:customXmlInsRangeStart|" +
				"descendant::w:del|" +
				"descendant::w:delInstrText|" +
				"descendant::w:delText|" +
				"descendant::w:ins|" +
				"descendant::w:moveFrom|" +
				"descendant::w:moveFromRangeEnd|" +
				"descendant::w:moveFromRangeStart|" +
				"descendant::w:moveTo|" +
				"descendant::w:moveToRangeEnd|" +
				"descendant::w:moveToRangeStart|" +
				"descendant::w:moveTo|" +
				"descendant::w:numberingChange|" +
				"descendant::w:rPrChange|" +
				"descendant::w:pPrChange|" +
				"descendant::w:rPrChange|" +
				"descendant::w:sectPrChange|" +
				"descendant::w:tcPrChange|" +
				"descendant::w:tblGridChange|" +
				"descendant::w:tblPrChange|" +
				"descendant::w:tblPrExChange|" +
				"descendant::w:trPrChange";
			XmlNodeList descendants = doc.SelectNodes(xpathExpression, nsmgr);
			return descendants.Count > 0;
		}

		public static bool HasTrackedRevisions(WordprocessingDocument doc)
		{
			if (PartHasTrackedRevisions(doc.MainDocumentPart))
				return true;
			foreach (var part in doc.MainDocumentPart.HeaderParts)
				if (PartHasTrackedRevisions(part))
					return true;
			foreach (var part in doc.MainDocumentPart.FooterParts)
				if (PartHasTrackedRevisions(part))
					return true;
			if (doc.MainDocumentPart.EndnotesPart != null)
				if (PartHasTrackedRevisions(doc.MainDocumentPart.EndnotesPart))
					return true;
			if (doc.MainDocumentPart.FootnotesPart != null)
				if (PartHasTrackedRevisions(doc.MainDocumentPart.FootnotesPart))
					return true;
			return false;
		}

		private static void SearchAndReplaceInXmlDocument(XmlDocument xmlDocument, string search,   string replace, bool matchCase)
		{
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDocument.NameTable);
			nsmgr.AddNamespace("w",
				"http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			var paragraphs = xmlDocument.SelectNodes("descendant::w:p", nsmgr);
			foreach (var paragraph in paragraphs)
			{
				SearchAndReplaceInParagraph((XmlElement)paragraph, search, replace, matchCase);
				//SearchAndReplaceInMathSubParagraph((XmlElement)paragraph, search, replace, matchCase);
				SearchAndReplaceExactRunInMathSubParagraph((XmlElement)paragraph, search, replace);
			}
		}

		public static void SearchAndReplace(WordprocessingDocument wordDoc, string search, string replace, bool matchCase)
		{
			if (HasTrackedRevisions(wordDoc))
				throw new SearchAndReplaceException(
					"Search and replace will not work with documents " +
					"that contain revision tracking.");

			XmlDocument xmlDoc;
			xmlDoc = GetXmlDocument(wordDoc.MainDocumentPart.DocumentSettingsPart);
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlDoc.NameTable);
			nsmgr.AddNamespace("w",
				"http://schemas.openxmlformats.org/wordprocessingml/2006/main");
			XmlNodeList trackedRevisions =
				xmlDoc.SelectNodes("descendant::w:trackRevisions", nsmgr);
			if (trackedRevisions.Count > 0)
				throw new SearchAndReplaceException(
					"Revision tracking is turned on for document.");

			xmlDoc = GetXmlDocument(wordDoc.MainDocumentPart);
			SearchAndReplaceInXmlDocument(xmlDoc, search, replace, matchCase);
			PutXmlDocument(wordDoc.MainDocumentPart, xmlDoc);
			foreach (var part in wordDoc.MainDocumentPart.HeaderParts)
			{
				xmlDoc = GetXmlDocument(part);
				SearchAndReplaceInXmlDocument(xmlDoc, search, replace, matchCase);
				PutXmlDocument(part, xmlDoc);
			}
			foreach (var part in wordDoc.MainDocumentPart.FooterParts)
			{
				xmlDoc = GetXmlDocument(part);
				SearchAndReplaceInXmlDocument(xmlDoc, search, replace, matchCase);
				PutXmlDocument(part, xmlDoc);
			}
			if (wordDoc.MainDocumentPart.EndnotesPart != null)
			{
				xmlDoc = GetXmlDocument(wordDoc.MainDocumentPart.EndnotesPart);
				SearchAndReplaceInXmlDocument(xmlDoc, search, replace, matchCase);
				PutXmlDocument(wordDoc.MainDocumentPart.EndnotesPart, xmlDoc);
			}
			if (wordDoc.MainDocumentPart.FootnotesPart != null)
			{
				xmlDoc = GetXmlDocument(wordDoc.MainDocumentPart.FootnotesPart);
				SearchAndReplaceInXmlDocument(xmlDoc, search, replace, matchCase);
				PutXmlDocument(wordDoc.MainDocumentPart.FootnotesPart, xmlDoc);
			}
		}
	}

	public class SearchAndReplaceException : Exception
	{
		public SearchAndReplaceException(string message) : base(message) { }
	}









	public static class Extensions
	{
		public static string ToStringAlignAttributes(this XContainer xContainer)
		{
			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;
			settings.OmitXmlDeclaration = true;
			settings.NewLineOnAttributes = true;
			StringBuilder sb = new StringBuilder();
			using (XmlWriter xmlWriter = XmlWriter.Create(sb, settings))
				xContainer.WriteTo(xmlWriter);
			return sb.ToString();
		}

		public static XDocument GetXDocument(this XmlDocument document)
		{
			XDocument xDoc = new XDocument();
			using (XmlWriter xmlWriter = xDoc.CreateWriter())
				document.WriteTo(xmlWriter);
			XmlDeclaration decl =
				document.ChildNodes.OfType<XmlDeclaration>().FirstOrDefault();
			if (decl != null)
				xDoc.Declaration = new XDeclaration(decl.Version, decl.Encoding,
					decl.Standalone);
			return xDoc;
		}

		public static XElement GetXElement(this XmlNode node)
		{
			XDocument xDoc = new XDocument();
			using (XmlWriter xmlWriter = xDoc.CreateWriter())
				node.WriteTo(xmlWriter);
			return xDoc.Root;
		}
	}
}