using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WifiInterfaceConnection.Core.Xml.XmlClass
{
    public abstract class FileXml
    {
        protected Task<bool> IsSaved { get; set; }
        protected string MsgError { get; set; }
        protected string FileFullName { get; set; }

        public string GetFullName()
        {
            if (IsSaved.Result)
            {
                return new FileInfo(FileFullName).FullName;
            }
            else
            {
                throw new Exception(MsgError);
            }
        }
        public string ToXML()
        {
            using (var stringwriter = new System.IO.StringWriter())
            {
                var xwsSettings = new XmlWriterSettings();
                xwsSettings.Encoding = Encoding.UTF8;
                xwsSettings.OmitXmlDeclaration = true;
                xwsSettings.NewLineOnAttributes = true;
                xwsSettings.Indent = true;
                xwsSettings.ConformanceLevel = ConformanceLevel.Document;

                var writer = XmlWriter.Create(stringwriter, xwsSettings);

                var serializer = new XmlSerializer(this.GetType());
                serializer.Serialize(writer, this);
                return stringwriter.ToString();
            }
        }
        public void SaveAsync(string path = ".")
        {
            IsSaved = SaveTaskAsync($@"{path}\{this.GetType().Name}.xml");
        }
        public async Task<bool> SaveTaskAsync(string pathName)
        {
            try
            {
                await File.WriteAllTextAsync(pathName, ToXML());
                FileFullName = pathName;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
