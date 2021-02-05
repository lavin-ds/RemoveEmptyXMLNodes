using RemoveEmptyXMLNodes;
using RemoveEmptyXMLNodesTest.Models;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using Xunit;

namespace RemoveEmptyXMLNodesTest
{   
    public class WriterTests
    {
        [Fact]
        public void Test1()
        {

            XmlSerializer ser = new XmlSerializer(typeof(Note));
            XmlReader reader = XmlReader.Create("test.xml");
            var obj = ser.Deserialize(reader);

            string serializedSWValue = string.Empty;
            string serializedCSWBValue = string.Empty;
            string serializedCSWValue = string.Empty;

            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                serializedSWValue = sw.ToString();
            }

            using (CustomStringWriterBasic cswb = new CustomStringWriterBasic())
            {
                ser.Serialize(cswb, obj);
                serializedCSWBValue = cswb.ToString();
            }

            using (CustomStringWriter csw = new CustomStringWriter())
            {
                ser.Serialize(csw, obj);
                serializedCSWValue = csw.ToString();
            }

            Assert.DoesNotContain("from", serializedCSWValue);
            Assert.Contains("from", serializedSWValue);
        }

        [Fact]
        public void Test2()
        {

            XmlSerializer ser = new XmlSerializer(typeof(Note));
            XmlReader reader = XmlReader.Create("test2.xml");
            var obj = ser.Deserialize(reader);

            string serializedSWValue = string.Empty;
            string serializedCSWBValue = string.Empty;
            string serializedCSWValue = string.Empty;

            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                serializedSWValue = sw.ToString();
            }

            using (CustomStringWriterBasic cswb = new CustomStringWriterBasic())
            {
                ser.Serialize(cswb, obj);
                serializedCSWBValue = cswb.ToString();
            }

            using (CustomStringWriter csw = new CustomStringWriter())
            {
                ser.Serialize(csw, obj);
                serializedCSWValue = csw.ToString();
            }

            Assert.DoesNotContain("from", serializedCSWValue);
            Assert.DoesNotContain("heading", serializedCSWValue);
            Assert.Contains("from", serializedSWValue);
            Assert.Contains("heading", serializedSWValue);
        }

        [Fact]
        public void Test3()
        {

            XmlSerializer ser = new XmlSerializer(typeof(Note2));
            XmlReader reader = XmlReader.Create("test3.xml");
            var obj = ser.Deserialize(reader);

            string serializedSWValue = string.Empty;
            string serializedCSWBValue = string.Empty;
            string serializedCSWValue = string.Empty;

            using (StringWriter sw = new StringWriter())
            {
                ser.Serialize(sw, obj);
                serializedSWValue = sw.ToString();
            }

            using (CustomStringWriterBasic cswb = new CustomStringWriterBasic())
            {
                ser.Serialize(cswb, obj);
                serializedCSWBValue = cswb.ToString();
            }

            using (CustomStringWriter csw = new CustomStringWriter())
            {
                ser.Serialize(csw, obj);
                serializedCSWValue = csw.ToString();
            }

            Assert.DoesNotContain("from", serializedCSWValue);
            Assert.DoesNotContain("heading", serializedCSWValue);
            Assert.DoesNotContain("cc", serializedCSWValue);
            Assert.DoesNotContain("bcc", serializedCSWValue);
            Assert.Contains("from", serializedSWValue);
            Assert.Contains("heading", serializedSWValue);
            Assert.Contains("cc", serializedSWValue);
            Assert.Contains("bcc", serializedSWValue);
        }
    }
}