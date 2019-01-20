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
            BackslashFoundInNodeWithData = -1, //S1M
            Initial = 0, //S0
            OpenBracketStarted  = 1, //S1
            NodeNameFound = 2, //S2
            BackslashFoundInNodeWithNoData = 4, //S4
            CloseBracketStarted = 5,//S5
            ClosingBracketForNodeWithData = 6,//S6
            NextOpeningBracket = 7 //S7
        }

        public override void Write(char value)
        {
            if(state == States.CloseBracketStarted && value == '<')
            {
                _node.Append(value);
            }

            //Dont append '<' char to _node unless confirmed if data present
            if(state != States.ClosingBracketForNodeWithData)
            {
                _node.Append(value);
            }
            else if(value!= '<')
            {
                _node.Append(value);
            }

            if(value == '<' && state == States.Initial)
            {
                state = States.OpenBracketStarted;
            }

            if(value == '<' && state == States.ClosingBracketForNodeWithData)
            {
                state = States.NextOpeningBracket;
            }

            if(value == '/' && state == States.NodeNameFound)
            {
                state = States.BackslashFoundInNodeWithNoData;
            }

            if(value == '/' && state == States.OpenBracketStarted)
            {
                state = States.BackslashFoundInNodeWithData;
            }

            if(value == '>' && state == States.BackslashFoundInNodeWithData)
            {
                base.Write(_node.ToString());
                _node.Clear();
                state = States.CloseBracketStarted;
            }

            if(value == '/' && state == States.NextOpeningBracket)
            {
                state = States.BackslashFoundInNodeWithNoData;
            }

            if(value == '>' && state == States.NodeNameFound)
            {
                state = States.ClosingBracketForNodeWithData;
            }

            if(value == '>' && state == States.BackslashFoundInNodeWithNoData)
            {
                //Node found without data
                _node.Clear();
                state = States.CloseBracketStarted;
            }

            if(state == States.Initial)
            {
                //If we are at initial state keep writing to the writer
                base.Write(_node.ToString());
                _node.Clear();   
            }
        }

        public override void Write(char[] buffer)
        {
            _node.Append(buffer);
        }

        public override void Write(string value)
        {
            if(state == States.CloseBracketStarted)
            {
                state = States.Initial;
            }  

            if(state != States.NextOpeningBracket)
            {
                _node.Append(value);
            }          

            if(value == " /" && state == States.NodeNameFound)
            {
                state = States.BackslashFoundInNodeWithNoData;
            }

            if(state == States.OpenBracketStarted)
            {
                state = States.NodeNameFound;
            }

            if(state == States.ClosingBracketForNodeWithData)
            {
                state = States.Initial;
            }
            if(state == States.NextOpeningBracket)
            {
                base.Write(_node.ToString());
                _node.Clear();
                _node.Append('<');
                _node.Append(value);
                state = States.NodeNameFound;
            }
            if(state == States.Initial)
            {
                base.Write(_node.ToString());
                _node.Clear();
            }
        }
    }
}
