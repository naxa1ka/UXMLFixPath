using System;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;

namespace Nxlk.UXMLFixPath
{
    public class TemplateNode
    {
        private const string OpeningTagPattern = "<ui:Template";
        private const string ClosingTagPattern = @"\s*/?>";
        private const string OptionalSpacesPattern = @"\s*";
        private const string RequiredSpacesPattern = @"\s+";
        private const string PathAttributePattern = @"path=""[^""]*""";
        private const string SrcAttributePattern = @"src=""([^""]*)""";
        private const string NameAttributePattern = @"name=""[^""]*""";

        private const string TemplateWithPathPattern =
            OptionalSpacesPattern
            + OpeningTagPattern
            + RequiredSpacesPattern
            + "(?:"
            + PathAttributePattern
            + RequiredSpacesPattern
            + NameAttributePattern
            + "|"
            + NameAttributePattern
            + RequiredSpacesPattern
            + PathAttributePattern
            + ")"
            + ClosingTagPattern
            + OptionalSpacesPattern;

        private const string TemplateWithSrcPattern =
            OptionalSpacesPattern
            + OpeningTagPattern
            + RequiredSpacesPattern
            + "(?:"
            + NameAttributePattern
            + RequiredSpacesPattern
            + SrcAttributePattern
            + "|"
            + SrcAttributePattern
            + RequiredSpacesPattern
            + NameAttributePattern
            + ")"
            + ClosingTagPattern
            + OptionalSpacesPattern;

        private readonly string _template;

        public bool IsWithPath { get; }
        public bool IsWithSrc { get; }

        public SrcAttribute SrcAttribute
        {
            get
            {
                if (!IsWithSrc)
                    throw new ArgumentOutOfRangeException();
                var match = Regex.Match(_template, SrcAttributePattern);
                if (!match.Success)
                    throw new UnableToParseException("Template is invalid");
                return new SrcAttribute(match.Groups[1].Value);
            }
        }

        public TemplateNode(string template)
        {
            _template = template;
            IsWithPath = Regex.IsMatch(template, TemplateWithPathPattern);
            IsWithSrc = Regex.IsMatch(template, TemplateWithSrcPattern);

            if (!template.TrimStart().StartsWith(OpeningTagPattern))
                throw new WrongNodeTypeException("This is not a template node");

            if (!IsWithPath && !IsWithSrc)
                throw new UnableToParseException("Invalid template " + template);
        }

        public static bool TryCreate(
            string template,
            [NotNullWhen(true)] out TemplateNode? templateNode
        )
        {
            try
            {
                templateNode = new TemplateNode(template);
                return true;
            }
            catch (WrongNodeTypeException)
            {
                templateNode = null;
                return false;
            }
        }

        [Pure]
        public TemplateNode WithNewPath(string newPath)
        {
            return WithSrcAttribute(SrcAttribute.WithNewPath(newPath));
        }

        [Pure]
        private TemplateNode WithSrcAttribute(SrcAttribute newSrcAttribute)
        {
            return new TemplateNode(
                Regex.Replace(_template, SrcAttributePattern, $"src=\"{newSrcAttribute}\"")
            );
        }

        public override string ToString()
        {
            return _template;
        }
    }
}
