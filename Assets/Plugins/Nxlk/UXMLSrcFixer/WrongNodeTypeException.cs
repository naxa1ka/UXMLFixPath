using System;

namespace Nxlk.UXMLSrcFixer
{
    public class WrongNodeTypeException : Exception
    {
        public WrongNodeTypeException(string message) : base(message)
        {
        }
    }
}