<h1><img src="https://raw.githubusercontent.com/AJMitev/FileTypeChecker/master/tools/FileTypeCheckerLogo-150.png" align="left" alt="FileTypeChecker" width="90">&nbsp; FileTypeChecker - Don't let users to inject you an invalid file</h1> 

FileTypeChecker is a easy to use library that allows you to read file and recognize its type. This will help you to validate all files that is provided by external sources. 

## How it works?
File type checker that checks the file's magic numbers/identifying bytes. Useful for verifying uploaded files in web applications. NuGet package of code originally written by https://github.com/mjolka and extended to allow for dependency injecting the known file types.

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

