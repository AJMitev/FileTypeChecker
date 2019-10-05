<h1><img src="https://raw.githubusercontent.com/AJMitev/FileTypeChecker/master/tools/FileTypeCheckerLogo-150.png" align="left" alt="FileTypeChecker" width="90">FileTypeChecker - Don't let users to inject you an invalid file</h1> 

[![NuGet Badge](https://buildstats.info/nuget/File.TypeChecker)](https://www.nuget.org/packages/File.TypeChecker/)

FileTypeChecker is a easy to use library that allows you to read file and recognize its type. This will help you to validate all files that is provided by external sources. 

## How it works?
FileTypeChecker use file's "magic numbers" to identifying its type. [See more about Magic Numbers](https://en.wikipedia.org/wiki/Magic_number_(programming)#Magic_numbers_in_files)

## How to use?
```c#
using (var fileStream = File.OpenRead("myFileLocation"))
{
    var isRecognizableType = FileTypeValidator.IsTypeRecognizable(fileStream);

    if (!isRecognizableType)
    {
        // Do something ...
    }

    IFileType fileType = FileTypeValidator.GetFileType(fileStream);
    Console.WriteLine("Type Name: {0}", fileType.Name);
    Console.WriteLine("Type Extension: {0}", fileType.Extension);
}
```

## What types of file are supported?
Currently FileTypeChecker recognizes following file types:

- JPEG
- Bitmap
- Portable Network Graphic
- Graphics Interchange Format 87a
- Graphics Interchange Format 89a
- Tagged Image File Format
- Portable Document Format
- Microsoft Office Document
- eXtensible Markup Language
- Comma-separated Values
- RAR archive version 1.50
- RAR archive version 5.00
- Photoshop Document file
- ZIP file
- eXtensible ARchive format
- TAR Archive
- 7-Zip File Format
- GZIP compressed file
- DOS MZ executable
- Executable and Linkable Format
- Audio Video Interleave video format
- MPEG-1 Layer 3 file
- MP3 file with an ID3v2 container
- ISO9660 CD/DVD image file
