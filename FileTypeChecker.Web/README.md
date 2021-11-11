## How to install?

If you are working on a web project like MVC or WebApi this library will provide you with all validation attributes you needed for easy data validation. You can install this library using NuGet into your project.

```nuget
Install-Package File.TypeChecker.Web
```

or by using dotnet CLI

```
dotnet add package File.TypeChecker.Web
```

## How to use?

This library will provide you with five powerful and easy to use validation attributes. They will give you the power to allow or forbid any supported type of file. For example you can restrict your users to be able to upload only images or only archives just by setting an attribute into your method or class.

All validation attributes should be used over IFormFile interface and can be used in a class over property or with method parameter.

- AllowImages: This validation attribute will restrict IFormFile to be only image format like jpg, gif, bmp, png and tiff
- AllowArchives: This validation attribute will restrict IFormFIle to be only archive format.
- AllowedTypes: This validation attribute will allow you to specify what types of file you want to receive from user. We advice you to use FileExtension class to specify the extension string.
- ForbidExecutables: This validation attribute will forbid your users to upload executable files.
- ForbidTypes: This validation attribute will allow you to specify what types of file you don't want to recive from user. We advice you to use FileExtension class to specify the extension string.

```c#
[HttpPost("filesUpload")]
public IActionResult UploadFiles([AllowImages] IFormFile imageFile, [AllowArchives] IFormFile archiveFile)
{
    // Some cool stuf here ...
}
```

```c#
using FileTypeChecker.Web.Attributes;

public class InputModel
{
    [AllowImages]
    public IFormFile FirstFile { get; set; }

    [AllowArchives]
    public IFormFile SecondFile { get; set; }

    [AllowedTypes(FileExtension.Bitmap)]
    public IFormFile ThirdFile { get; set; }

    [ForbidExecutables]
    public IFormFile FourthFile { get; set; }

    [ForbidTypes(FileExtension.Doc)]
    public IFormFile FifthFile { get; set; }
}
```

If you are interested in finding more samples please use our [wiki page](https://github.com/AJMitev/FileTypeChecker/wiki/How-to-use%3F).
