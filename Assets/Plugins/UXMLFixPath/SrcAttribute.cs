using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using UnityEditor;

namespace Nxlk.UXMLFixPath
{
    public class SrcAttribute
    {
        private readonly string _value;

        public GUID GUID
        {
            get
            {
                const string guidPattern = "guid=([a-f0-9]+)";
                var match = Regex.Match(_value, guidPattern);
                if (!match.Success)
                    throw new UnableToParseException("Invalid GUID: " + _value);
                return new GUID(match.Groups[1].Value);
            }
        }

        public SrcAttribute(string value)
        {
            _value = value;
        }

        [Pure]
        public SrcAttribute WithNewPath(string newPath)
        {
            const string projectDatabase = "project://database/";
            const string assetPathPattern = projectDatabase + "([^?]+)";

            return new SrcAttribute(
                Regex.Replace(_value, assetPathPattern, $"{projectDatabase}{newPath}")
            );
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
