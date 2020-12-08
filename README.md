# ImageBase

ImageBase is a .NET project that wrote with C# for managing your images collection and compared it to other images. This will allow you to check the image for originality.

## Installation

Use the link [https](https://github.com/fornit1917/imagebase.git) to install ImageBase.
Write code to create repository clone

```bash
gh repo clone fornit1917/imagebase
```
## Structure project
```bash
├── imageBase
│   ├── ImageBase.Common
│   ├── ImageBase.GrabbingImages
│   ├── ImageBase.HashBase
│   ├── ImageBase.ImageHash
│   ├── ImageBase.WebApp
│   ├── ImageBase.Common.UnitTests
│   ├── ImageBase.HashBase.UnitTests
│   ├── ImageBase.ImageHash.Tests
│   └── ImageBase.WebApp.UnitTests
└──
```
_ImageBase.Common_ - this project contains calculate Hamming distance for image  
_ImageBase.GrabbingImages_ - This project contains populating your database with free Pexels images  
_ImageBase.HashBase_ - this project contains generate VP Tree and compares hashes images  
_ImageBase.ImageHash_ - this project calculates hashes images  
_ImageBase.WebApp_ - this project contains rest API requests to manage your images collection
## Usage
ImageBase.GrabbingImages
```ccharp
static async Task Main(string[] args)
{
 Grabber grabber = InicializeGrabber();
 KeyWord = "Nature";
 PhotoPage photoPage = await grabber.SearchPhotosAsync(KeyWord, 1,5); //Set 3 parameters: tema, start page and count image
 _listImageDtos = CreateListImages(photoPage);
 ConvertToCSVAndSaveInFile(_listImageDtos,"AllImages.csv"); //Set 2 parameters: list with your images and output saving file
}
```
## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[GPL-3.0 License](https://github.com/fornit1917/imagebase/blob/main/LICENSE)