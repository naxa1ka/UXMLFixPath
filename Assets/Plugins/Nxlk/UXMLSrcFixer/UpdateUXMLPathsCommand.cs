using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using UnityEngine.UIElements;

namespace Nxlk.UXMLSrcFixer
{
    public class UpdateUXMLPathsCommand
    {
        private readonly IAssetDatabase _assetDatabase;
        private readonly ILogger _logger;
        private readonly IFileSystem _fileSystem;

        public UpdateUXMLPathsCommand(
            ILogger logger,
            IAssetDatabase assetDatabase,
            IFileSystem fileSystem
        )
        {
            _logger = logger;
            _assetDatabase = assetDatabase;
            _fileSystem = fileSystem;
        }

        public UpdateUXMLPathsCommand(ILogger logger)
            : this(logger, new UnityAssetDatabase(), new FileSystem()) { }

        public UpdateUXMLPathsCommand()
            : this(new UnityLogger()) { }

        public void Execute() =>
            Execute(_assetDatabase.FindAssets(new TypeAssetFilter(typeof(VisualTreeAsset))));

        public void Execute(IEnumerable<string> assetPaths) =>
            Execute(assetPaths.Select(assetPath => new Asset(assetPath)));

        public void Execute(IEnumerable<IAsset> assets)
        {
            var updatedFiles = 0;
            foreach (var asset in assets)
            {
                if (!TryUpdate(asset))
                    continue;
                updatedFiles++;
            }

            if (updatedFiles == 0)
                return;
            _assetDatabase.Refresh();
            _logger.Log($"UXML Path Update Complete. Updated {updatedFiles} files.");
        }

        private bool TryUpdate(IAsset asset)
        {
            const string uxmlFileExtension = ".uxml";
            if (!asset.Path.EndsWith(uxmlFileExtension, StringComparison.OrdinalIgnoreCase))
            {
                _logger.LogError($"The asset {asset} does not end with {uxmlFileExtension}");
                return false;
            }

            var fullPath = _fileSystem.GetFullPath(asset.Path);
            if (!TryRead(fullPath, out var fileContent))
            {
                _logger.LogError($"Failed to read file from {fullPath}");
                return false;
            }

            var hasChanges = false;

            using var stringWriter = new StringWriter();
            using var stringReader = new StringReader(fileContent);

            while (true)
            {
                var line = stringReader.ReadLine();
                if (line == null)
                    break;

                if (!TemplateNode.TryCreate(line, out var templateNode))
                {
                    stringWriter.WriteLine(line);
                    continue;
                }

                if (templateNode.IsWithPath)
                {
                    stringWriter.WriteLine(line);
                    continue;
                }

                var newAssetPath = _assetDatabase.GUIDToAssetPath(templateNode.SrcAttribute.GUID);
                var updatedLine = templateNode.WithNewPath(newAssetPath).ToString();
                if (line == updatedLine)
                {
                    stringWriter.WriteLine(line);
                    continue;
                }
                stringWriter.WriteLine(updatedLine);

                hasChanges = true;
            }

            if (!hasChanges)
                return false;

            try
            {
                _fileSystem.WriteAllText(fullPath, stringWriter.ToString());
                _logger.Log($"Updated source in {asset.Path}");
                return true;
            }
            catch
            {
                _logger.LogError($"Failed to write updated UXML to {fullPath}");
                return false;
            }
        }

        private bool TryRead(string fullPath, [NotNullWhen(returnValue: true)] out string? content)
        {
            try
            {
                content = _fileSystem.ReadAllText(fullPath);
                return true;
            }
            catch
            {
                content = null;
                return false;
            }
        }
    }
}
