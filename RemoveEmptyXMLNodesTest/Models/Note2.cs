using System.Xml.Serialization;

namespace RemoveEmptyXMLNodesTest.Models
{
    [XmlRoot("note2")]
    public class Note2
    {
        [XmlElement]
        public string to { get; set; }
        [XmlElement("from")]
        public From from { get; set; }
        [XmlElement]
        public string heading { get; set; }
        [XmlElement]
        public string body { get; set; }
    }
}
