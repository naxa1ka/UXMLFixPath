## Description
This package addresses an issue in Unity UI Toolkit where moving UXML files breaks references to templates within those files, causing Unity to log warnings.\
The package automatically updates template paths inside UXML files when they are moved.

## Usage

After installation, the package works automatically. When you move UXML files, template references will be updated without any action required on your part.\
[It](https://github.com/naxa1ka/UXMLSrcFixer/blob/master/Assets/Plugins/Nxlk/UXMLSrcFixer/UxmlAssetPostprocessor.cs) works by using the [AssetPostprocessor](https://docs.unity3d.com/ScriptReference/AssetPostprocessor.html).

Also, during the first installation, you may need to manually call the update of all UXMLs.
![изображение](https://github.com/user-attachments/assets/7a1a669a-f44d-49f7-b7d0-ad46d49e3975)
![изображение](https://github.com/user-attachments/assets/44d56a25-c9d1-4939-a95b-88745d00faf0)

## Install

1. Open the Package Manager from `Window > Package Manager`
2. `"+" button > Add package from git URL`
3. Enter the following
   * https://github.com/naxa1ka/UXMLSrcFixer.git?path=Assets/Plugins/Nxlk/UXMLSrcFixer#tag

Now: https://github.com/naxa1ka/UXMLSrcFixer.git?path=Assets/Plugins/Nxlk/UXMLSrcFixer#1.1.1

