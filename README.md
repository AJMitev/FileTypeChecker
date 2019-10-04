<h1><img src="https://raw.githubusercontent.com/AJMitev/FileTypeChecker/master/tools/FileTypeCheckerLogo-150.png" align="left" alt="FileTypeChecker" width="90">&nbsp; FileTypeChecker - Don't let users to inject you an invalid file</h1> 

FileTypeChecker is a easy to use library that allows you to read file and recognize its type. This will help you to validate all files that is provided by external sources.

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
```

