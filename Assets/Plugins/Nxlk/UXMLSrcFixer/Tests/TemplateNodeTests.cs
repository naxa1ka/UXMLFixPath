using NUnit.Framework;

namespace Nxlk.UXMLSrcFixer.Tests
{
    public class TemplateNodeTests
    {
        [Test]
        public void WhenCreatingTemplateNode_WithPath_ThenItShouldBeValid()
        {
            // Arrange.
            const string template = @"<ui:Template path=""some/path"" name=""TemplateName"" />";

            // Act.
            var templateNode = new TemplateNode(template);

            // Assert.
            Assert.IsTrue(templateNode.IsWithPath);
            Assert.IsFalse(templateNode.IsWithSrc);
        }

        [Test]
        public void WhenCreatingTemplateNode_WithSrc_ThenItShouldBeValid()
        {
            // Arrange.
            const string template =
                @"<ui:Template name=""SettingsNavigationBar"" src=""project://database/Assets/Templates/SettingsNavigationBar.uxml?fileID=9197481963319205126&amp;guid=e15ab4dd52f0f4c4f82b93ff590239dd&amp;type=3#SettingsNavigationBar"" />";

            // Act.
            var templateNode = new TemplateNode(template);

            // Assert.
            Assert.IsTrue(templateNode.IsWithSrc);
            Assert.IsFalse(templateNode.IsWithPath);
        }

        [Test]
        public void WhenCreatingTemplateNode_WithInvalidTemplate_ThenItShouldThrowUnableToParseException()
        {
            // Arrange.
            const string invalidTemplate =
                @"<ui:WrongTag name=""TemplateName"" src=""project://database/somepath"" />";

            // Act & Assert.
            Assert.Throws<WrongNodeTypeException>(() => new TemplateNode(invalidTemplate));
        }

        [Test]
        public void WhenWithNewPath_AndValidPath_ThenItShouldUpdateThePathCorrectly()
        {
            // Arrange.
            const string template =
                @"<ui:Template name=""TemplateName"" src=""project://database/somepath"" />";
            var templateNode = new TemplateNode(template);
            const string newPath = "newpath";

            // Act.
            var newTemplateNode = templateNode.WithNewPath(newPath);

            // Assert.
            Assert.AreEqual("project://database/newpath", newTemplateNode.SrcAttribute.ToString());
        }
    }
}
