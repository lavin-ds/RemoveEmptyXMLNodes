using System.Xml.Serialization;

namespace RemoveEmptyXMLNodesTest.Models
{
    public class From
    {
        [XmlElement]
        public string cc { get; set; }
        [XmlElement]
        public string bcc { get; set; }
    }
}