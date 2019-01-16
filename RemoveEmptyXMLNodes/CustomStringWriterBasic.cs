using System;
using System.IO;
using System.Text;

namespace RemoveEmptyXMLNodes
{
    public class CustomStringWriterBasic : StringWriter
    {
        private StringBuilder _node = new StringBuilder();

        public override void Write(char value)
        {
            _node.Append(value);

            if(value == '>' && _node[1] == '/')
            {
                base.Write(_node.ToString());
                _node.Clear();
            }
        }

        public override void Write(char[] buffer)
        {
            if(buffer[0] == '\r' && buffer[1] == '\n')
            {
                if(_node[_node.Length - 2] == '/')
                {
                    base.Write(_node.ToString());
                    base.Write(buffer);
                }
                _node.Clear();
            }
        }

        public override void Write(string value)
        {
            _node.Append(value);
        }
    }
}
