using System.Xml.Serialization;

namespace RemoveEmptyXMLNodesTest.Models
{
    [XmlRoot("note")]
    public class Note
    {
        [XmlElement]
        public string to { get; set; }
        [XmlElement]
        public string from { get; set; }
        [XmlElement]
        public string heading { get; set; }
        [XmlElement]
        public string body { get; set; }
    }
}