#region

using System.IO;
using System.Text;

#endregion

namespace RemoveEmptyXMLNodes
{
    ///<summary>
    ///String writer has been extended to skip empty nodes and not write them to the writer. Instead of the regular StringWriter
    ///when we pass CustomTextWriter to the XmlSerializer, the code does not create any nodes of type <abc /> or <abc></abc>
    ///</summary> 
    public class CustomStringWriter : StringWriter
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
            OpenBracketStarted = 1, //S1
            NodeNameFound = 2, //S2
            BackslashFoundInNodeWithNoData = 4, //S4
            CloseBracketStarted = 5,//S5
            ClosingBracketForNodeWithData = 6,//S6
            NextOpeningBracket = 7 //S7
        }

        public override void Write(char value)
        {
            if (state == States.Initial)
            {
                //If we are at initial state keep writing to the writer
                base.Write(_node.ToString());
                _node.Clear();
            }

            switch (value)
            {
                case '>':
                    _node.Append(value);
                    if (state == States.NodeNameFound)
                    {
                        state = States.ClosingBracketForNodeWithData;
                    }
                    //Node found without data
                    if (state == States.BackslashFoundInNodeWithNoData)
                    {
                        _node.Clear();
                        state = States.CloseBracketStarted;
                    }           
                    if (state == States.BackslashFoundInNodeWithData)
                    {
                        base.Write(_node.ToString());
                        _node.Clear();
                        state = States.CloseBracketStarted;
                    }
                    break;
                case '/':
                    _node.Append(value);
                    if (state == States.NodeNameFound)
                    {
                        state = States.BackslashFoundInNodeWithNoData;
                    }
                    if (state == States.OpenBracketStarted)
                    {
                        state = States.BackslashFoundInNodeWithData;
                    }
                    if (state == States.NextOpeningBracket)
                    {
                        state = States.BackslashFoundInNodeWithNoData;
                    }
                    break;
                case '<':
                    if (state == States.Initial)
                    {
                        state = States.OpenBracketStarted;
                    }
                    if (state == States.ClosingBracketForNodeWithData)
                    {
                        state = States.NextOpeningBracket;
                    }
                    if (state == States.CloseBracketStarted)
                    {
                        _node.Append(value);
                    }
                    break;
            }            
        }

        public override void Write(char[] buffer)
        {
            if (buffer.Length == 3 && buffer[0] == ' ' && buffer[1] == '/' && buffer[2] == '>' && state == States.NodeNameFound)
            {
                _node.Clear();
                state = States.Initial;
                return;
            }
            _node.Append(buffer);
        }

        public override void Write(string value)
        {
            switch (state)
            {
                case States.CloseBracketStarted:
                    _node.Append(value);
                    state = States.Initial;
                    base.Write(_node.ToString());
                    _node.Clear();
                    break;

                case States.OpenBracketStarted:
                    _node.Append(value);
                    state = States.NodeNameFound;
                    break;

                case States.ClosingBracketForNodeWithData:
                    _node.Append(value);
                    state = States.Initial;
                    break;

                case States.NextOpeningBracket:
                    base.Write(_node.ToString());
                    _node.Clear();
                    _node.Append('<');
                    _node.Append(value);
                    state = States.NodeNameFound;
                    break;

                case States.Initial:
                    _node.Append(value);
                    base.Write(_node.ToString());
                    _node.Clear();
                    break;

                case States.NodeNameFound:
                    _node.Append(value);
                    if (value == " /")
                    {
                        state = States.BackslashFoundInNodeWithNoData;
                    }
                    break;
            }
        }
    }
}
