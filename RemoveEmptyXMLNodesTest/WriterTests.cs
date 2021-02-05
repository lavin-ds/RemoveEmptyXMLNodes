using Xunit;
using System.IO;
using RemoveEmptyXMLNodes;
using System.Xml;
using System.Xml.Serialization;
using System;

namespace RemoveEmptyXMLNodesTest
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

    public class WriterTests
    {
        [Fact]
        public void Test1()
        {        
            XmlSerializer ser = new XmlSerializer(typeof(Note));

            StringWriter sw = new StringWriter();
            CustomStringWriterBasic cswb = new CustomStringWriterBasic();
            CustomStringWriter csw = new CustomStringWriter();

            XmlReader reader = XmlReader.Create("test.xml");
            var obj = ser.Deserialize(reader);
           
            ser.Serialize(sw, obj);
            ser.Serialize(cswb, obj);
            ser.Serialize(csw, obj);

            var serializedSWValue = sw.ToString();
            var serializedCSSWValue = cswb.ToString();
            var serializedCSWValue = csw.ToString();

            Assert.NotEqual(serializedSWValue, serializedCSWValue);
        }
    }
}