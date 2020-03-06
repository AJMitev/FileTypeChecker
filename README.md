<h1><img src="https://raw.githubusercontent.com/AJMitev/FileTypeChecker/master/tools/FileTypeCheckerLogo-150.png" align="left" alt="FileTypeChecker" width="90">FileTypeChecker - Don't let users to inject you an invalid file</h1> 

[![Build status](https://ci.appveyor.com/api/projects/status/jx9bcrxs95srhxsj?svg=true)](https://ci.appveyor.com/project/AJMitev/filetypechecker) [![NuGet Badge](https://buildstats.info/nuget/File.TypeChecker)](https://www.nuget.org/packages/File.TypeChecker/)

FileTypeChecker is a easy to use library that allows you to read file and recognize its type. This will help you to validate all files that are provided by external sources. 

## Why to use it?
Have you ever had to allow your users to upload files? How do you prevent malicious attacks? How do you validate that the file is of the allowed type? It is standard practice to use the [FileSystemInfo](https://docs.microsoft.com/en-us/dotnet/api/system.io.fileinfo?view=netcore-3.1#definition) class provided by Microsoft and its [Extension](https://docs.microsoft.com/en-us/dotnet/api/system.io.filesysteminfo.extension?view=netcore-3.1#System_IO_FileSystemInfo_Extension) property, but is that enough? The answer is simple - No! This is why this small but effective library comes to help.

## How it works?
FileTypeChecker use file's "magic numbers" to identify its type. According to Wikipedia this term ("magic numbers") was used for a specific set of 2-byte identifiers at the beginnings of files, but since any binary sequence can be regarded as a number, any feature of a file format which uniquely distinguishes it can be used for identification. This approach offers better guarantees that the format will be identified correctly, and can often determine more precise information about the file. [See more about Magic Numbers](https://en.wikipedia.org/wiki/File_format#Magic_number)

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
