using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace Nxlk.UXMLFixPath
{
    public class TemplateNode
    {
        public const string OpeningTag = "<ui:Template";

        private readonly string _template;

        public TemplateNode(string template)
        {
            _template = template;
        }

        public SrcAttribute SrcAttribute
        {
            get
            {
                const string srcPattern = @"src=""([^""]+)""";
                var match = Regex.Match(_template, srcPattern);
                if (!match.Success)
                    throw new UnableToParseException("Template is invalid");
                return new SrcAttribute(match.Groups[1].Value);
            }
        }

        [Pure]
        public TemplateNode WithSrcAttribute(SrcAttribute newSrcAttribute)
        {
            return new TemplateNode(
                _template.Replace($"src=\"{SrcAttribute}\"", $"src=\"{newSrcAttribute}\"")
            );
        }

        [Pure]
        public TemplateNode WithNewPath(string newPath)
        {
            return WithSrcAttribute(SrcAttribute.WithNewPath(newPath));
        }

        public override string ToString()
        {
            return _template;
        }
    }
}
