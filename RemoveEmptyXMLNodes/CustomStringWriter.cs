#region Namespaces

using System;
using System.IO;
using System.Text;

#endregion

namespace RemoveEmptyXMLNodes
{
    ///<summary>
    ///String writer has been extended to skip empty nodes and not write them to the writer. Instead of the regular StringWriter
    ///when we pass CustomTextWriter to the XmlSerializer, the code does not create any nodes of type <abc /> or <abc></abc>
    ///</summary> 
    public class CustomTextWriter : StringWriter
    {
        ///<summary>
        ///The _node variable to hold the data in the buffer before writing to the base writer
        ///</summary>
        private StringBuilder _node = new StringBuilder();

        ///<summary>
        ///The state variable which tells us the current state of the Xml document as it is being parsed
        ///</summary>
        private States state = States.Initial;

        ///<summary>
        ///The state variable which tells us the current state of the Xml document as it is being parsed
        ///</summary>
        private enum States
        {
            ClosingDataNode = -1,
            Initial = 0,
            OpenBracketStarted = 1,
            NodeNameFound = 2,
            ClosingEmptyNode = 4,
            EndNodeClosed = 5,
            StartNodeClosed = 6,
            ChildNodeOrEmptyEndNodeStarted = 7
        }
    }
}
