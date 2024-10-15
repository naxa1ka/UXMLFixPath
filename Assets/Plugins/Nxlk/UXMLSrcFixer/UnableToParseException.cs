using System;

namespace Nxlk.UXMLSrcFixer
{
    public class UnableToParseException : Exception
    {
        public UnableToParseException(string message) : base(message)
        {
        }
    }
}