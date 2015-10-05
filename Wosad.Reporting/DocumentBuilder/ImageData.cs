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
 
using DocumentFormat.OpenXml.Packaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Wosad.Reporting.ResultBuilder
{
    // This class is used to prevent duplication of images
    public class ImageData
    {
        private byte[] m_Image;
        private string m_ContentType;
        private string m_ResourceID;

        public ImageData(ImagePart part)
        {
            m_ContentType = part.ContentType;
            using (Stream s = part.GetStream(FileMode.Open, FileAccess.Read))
            {
                m_Image = new byte[s.Length];
                s.Read(m_Image, 0, (int)s.Length);
            }
        }

        public void WriteImage(ImagePart part)
        {
            using (Stream s = part.GetStream(FileMode.Create, FileAccess.ReadWrite))
            {
                s.Write(m_Image, 0, m_Image.GetUpperBound(0) + 1);
            }
        }

        public string ResourceID
        {
            get { return m_ResourceID; }
            set { m_ResourceID = value; }
        }

        public bool Compare(ImageData arg)
        {
            if (m_ContentType != arg.m_ContentType)
                return false;
            if (m_Image.GetLongLength(0) != arg.m_Image.GetLongLength(0))
                return false;
            // Compare the arrays byte by byte
            long length = m_Image.GetLongLength(0);
            for (long n = 0; n < length; n++)
                if (m_Image[n] != arg.m_Image[n])
                    return false;
            return true;
        }
    }
}
