using System;

namespace Nxlk.UXMLFixPath
{
    public class WrongNodeTypeException : Exception
    {
        public WrongNodeTypeException(string message) : base(message)
        {
        }
    }
}