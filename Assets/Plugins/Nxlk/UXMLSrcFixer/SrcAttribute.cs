using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using UnityEditor;

namespace Nxlk.UXMLSrcFixer
{
    public class SrcAttribute
    {
        private const string GUIDPattern = "guid=([a-f0-9]+)";
        private const string ProjectDatabase = "project://database/";
        private const string AssetPathPattern = ProjectDatabase + "([^?]+)";
        private const string HtmlStyleSpace = "%20";
        private const string Space = " ";

        private readonly string _value;

        public GUID GUID
        {
            get
            {
                var match = Regex.Match(_value, GUIDPattern);
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
            newPath = newPath.Replace(Space, HtmlStyleSpace);
            return new SrcAttribute(
                Regex.Replace(_value, AssetPathPattern, $"{ProjectDatabase}{newPath}")
            );
        }

        public override string ToString()
        {
            return _value;
        }
    }
}
