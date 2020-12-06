using Xunit;
using System.IO;
using RemoveEmptyXMLNodes;

namespace RemoveEmptyXMLNodesTest
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            string tstStr = "<note><to>Tove</to><from></from><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>";
            string resultStr = "<note><to>Tove</to><heading>Reminder</heading><body>Don't forget me this weekend!</body></note>";

            StringReader strReader = new StringReader(tstStr);

            int intCharacter;
            CustomStringWriter strWriter = new CustomStringWriter();
            while(true)
            {
                 intCharacter = strReader.Read();

                // Check for the end of the string
                // before converting to a character.
                if(intCharacter == -1) break;

                strWriter.Write(intCharacter);
            }
            Assert.Equal(strWriter.ToString(), resultStr);
        }
    }
}