using Perfect.Marshal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace editor.cfg
{
    [Serializable]
    public abstract class EditorWriter
    {
        public abstract int GetTypeId();
        public abstract void MarshalXml(XmlWriter ele);
        public string FileName { get; set; }
        private void CreateParentDirectory(string file)
        {
            var dirName = Path.GetDirectoryName(file);
            // UnityEngine.Debug.LogFormat("create directory:{0}", dirName);
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
        }
        public string ToXML()
        {
            StringBuilder sb = new StringBuilder();
            using (var writer = XmlWriter.Create(sb, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("data");
                MarshalXml(writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
            return sb.ToString();
        }

        public void Save(string file)
        {
            if (!file.EndsWith(".xml"))
            {
                throw new Exception("文件后缀只支持.xml");
            }
            CreateParentDirectory(file);

            using (var writer = XmlWriter.Create(file, new XmlWriterSettings() { Indent = true, IndentChars = "\t" }))
            {
                writer.WriteStartDocument();
                writer.WriteStartElement("data");
                MarshalXml(writer);
                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }
    }
}
