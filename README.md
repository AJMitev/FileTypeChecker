# 🛡️ FileTypeChecker

<div align="center">

![FileTypeChecker Logo](https://raw.githubusercontent.com/AJMitev/FileTypeChecker/master/tools/FileTypeCheckerLogo-150.png)

**Secure file type validation for .NET applications using magic number detection**

[![Build Status](https://ci.appveyor.com/api/projects/status/jx9bcrxs95srhxsj?svg=true)](https://ci.appveyor.com/project/AJMitev/filetypechecker)
[![NuGet](https://img.shields.io/nuget/v/File.TypeChecker.svg)](https://www.nuget.org/packages/File.TypeChecker/)
[![Downloads](https://img.shields.io/nuget/dt/File.TypeChecker?color=blue)](https://www.nuget.org/packages/File.TypeChecker/)
[![License: MIT](https://img.shields.io/github/license/ajmitev/filetypechecker)](https://github.com/AJMitev/FileTypeChecker/blob/master/LICENSE)
[![CodeFactor](https://www.codefactor.io/repository/github/ajmitev/filetypechecker/badge)](https://www.codefactor.io/repository/github/ajmitev/filetypechecker)
[![Discord](https://img.shields.io/discord/1035464819102470155?logo=discord)](https://discord.gg/your-discord-invite)

</div>

## ✨ Overview

FileTypeChecker is a powerful .NET library that provides reliable file type identification using magic number detection. Unlike traditional filename extension-based validation, this library analyzes the actual file content to determine the true file type, protecting your applications from malicious files and ensuring robust security.

## 📋 Table of Contents

- [🚀 Quick Start](#-quick-start)
- [💡 Why Use FileTypeChecker?](#-why-use-filetypechecker)
- [⚙️ How It Works](#-how-it-works)
- [📦 Installation](#-installation)
- [🔧 Usage Examples](#-usage-examples)
- [📄 Supported File Types](#-supported-file-types)
- [🌐 Web Applications](#-web-applications)
- [🤝 Contributing](#-contributing)
- [💖 Support the Project](#-support-the-project)
- [📝 License](#-license)

## 🚀 Quick Start

```csharp
using (var fileStream = File.OpenRead("suspicious-file.exe"))
{
    // Check if file type can be identified
    if (FileTypeValidator.IsTypeRecognizable(fileStream))
    {
        // Get the actual file type
        IFileType fileType = FileTypeValidator.GetFileType(fileStream);
        Console.WriteLine($"File type: {fileType.Name} ({fileType.Extension})");
        
        // Check specific type
        bool isImage = fileStream.IsImage();
        bool isPdf = fileStream.Is<PortableDocumentFormat>();
    }
}
```

## 💡 Why Use FileTypeChecker?

### 🎯 The Problem
Traditional file validation relies on file extensions, which can be easily manipulated:
- A malicious executable can be renamed to `.jpg`
- Untrusted files can bypass basic extension checks
- The `FileSystemInfo.Extension` property only reads the filename

### ✅ The Solution
FileTypeChecker analyzes the actual file content using magic numbers:
- **Reliable**: Identifies files by their binary signature, not filename
- **Secure**: Prevents malicious files from masquerading as safe formats
- **Comprehensive**: Supports 30+ file types with extensible architecture
- **Fast**: Minimal performance overhead with efficient binary analysis

## ⚙️ How It Works

FileTypeChecker uses **magic numbers** (binary signatures) to identify file types. These are specific byte sequences found at the beginning of files that uniquely identify the format.

### 🔍 Magic Number Examples
```
PDF:  25 50 44 46  (%PDF)
PNG:  89 50 4E 47  (‰PNG)
JPEG: FF D8 FF     (ÿØÿ)
ZIP:  50 4B 03 04  (PK..)
```

This method provides reliable identification regardless of file extension, offering better security guarantees than filename-based validation.

> 📖 Learn more about [Magic Numbers on Wikipedia](https://en.wikipedia.org/wiki/File_format#Magic_number)

## 📦 Installation

### Package Manager
```powershell
Install-Package File.TypeChecker
```

### .NET CLI
```bash
dotnet add package File.TypeChecker
```

### PackageReference
```xml
<PackageReference Include="File.TypeChecker" Version="4.2.0" />
```

**Requirements**: .NET Standard 2.0+

## 🔧 Usage Examples

### Basic File Type Detection
```csharp
using FileTypeChecker;

using (var fileStream = File.OpenRead("document.pdf"))
{
    // Check if file type is recognizable
    if (FileTypeValidator.IsTypeRecognizable(fileStream))
    {
        // Get file type information
        IFileType fileType = FileTypeValidator.GetFileType(fileStream);
        Console.WriteLine($"Type: {fileType.Name}");
        Console.WriteLine($"Extension: {fileType.Extension}");
    }
}
```

### Category-Based Validation
```csharp
using (var fileStream = File.OpenRead("image.jpg"))
{
    // Check by category
    bool isImage = fileStream.IsImage();
    bool isDocument = fileStream.IsDocument();
    bool isArchive = fileStream.IsArchive();
    
    // Check specific type
    bool isPng = fileStream.Is<PortableNetworkGraphic>();
    bool isJpeg = fileStream.Is<JointPhotographicExpertsGroup>();
}
```

### File Upload Validation
```csharp
public bool ValidateUploadedFile(IFormFile file)
{
    using (var stream = file.OpenReadStream())
    {
        // Verify file is actually an image (regardless of file extension)
        if (!stream.IsImage())
        {
            throw new InvalidOperationException("Only image files are allowed");
        }
        
        // Additional validation for specific formats
        var fileType = FileTypeValidator.GetFileType(stream);
        var allowedTypes = new[] { "PNG", "JPEG", "BMP" };
        
        return allowedTypes.Contains(fileType.Name);
    }
}
```

### Custom File Type Registration
```csharp
// Register your own file type
public class MyCustomType : FileType
{
    public override string Name => "My Custom Format";
    public override string Extension => "mycustom";
    public override string MimeType => "application/x-mycustom";
    
    public override bool IsMatch(byte[] signature, Stream stream)
    {
        return signature.Length >= 4 && 
               signature[0] == 0x4D && signature[1] == 0x59 && 
               signature[2] == 0x43 && signature[3] == 0x54;
    }
}

// Use it
FileTypeValidator.RegisterType<MyCustomType>();
```

> 📚 More examples available in our [Wiki](https://github.com/AJMitev/FileTypeChecker/wiki/How-to-use%3F)

## 📄 Supported File Types

FileTypeChecker supports **30+ file formats** across multiple categories:

### 🖼️ Images
- **PNG** - Portable Network Graphics
- **JPEG** - Joint Photographic Experts Group  
- **GIF** - Graphics Interchange Format (87a/89a)
- **BMP** - Bitmap Image File
- **TIFF** - Tagged Image File Format
- **WebP** - WebP Image Format
- **ICO** - Icon File
- **PSD** - Photoshop Document
- **HEIC** - High Efficiency Image Container

### 📄 Documents
- **PDF** - Portable Document Format
- **DOC/DOCX** - Microsoft Word Documents
- **XLS/XLSX** - Microsoft Excel Spreadsheets
- **HTML** - HyperText Markup Language
- **XML** - Extensible Markup Language

### 🗜️ Archives
- **ZIP** - ZIP Archive
- **RAR** - RAR Archive
- **7Z** - 7-Zip Archive
- **TAR** - TAR Archive
- **GZIP** - GNU Zip
- **BZIP2** - BZIP2 Compressed File

### 🎵 Audio/Video
- **MP3** - MPEG Audio Layer 3
- **MP4** - MPEG-4 Video
- **M4V** - iTunes Video
- **AVI** - Audio Video Interleave
- **WAV** - Windows Audio

### 💻 Executables
- **EXE** - Windows Executable
- **ELF** - Executable and Linkable Format

### ➕ Extensible
Add your own custom file types by implementing the `IFileType` interface.

> 📋 Complete list available in our [Wiki](https://github.com/AJMitev/FileTypeChecker/wiki/What-types-of-file-are-supported%3F)

## 🌐 Web Applications

For **ASP.NET Core** applications, check out [FileTypeChecker.Web](https://github.com/AJMitev/FileTypeChecker.Web) - a companion package with validation attributes for `IFormFile`:

```csharp
public class UploadModel
{
    [AllowedFileTypes(FileType.Jpeg, FileType.Png)]
    [MaxFileSize(5 * 1024 * 1024)] // 5MB
    public IFormFile ProfileImage { get; set; }
}
```

### Features
- ✅ Pre-built validation attributes
- ✅ Model binding integration  
- ✅ Automatic error messages
- ✅ Easy file upload validation

## 🤝 Contributing

We welcome contributions! Please see our [Contributing Guidelines](CONTRIBUTING.md) for details.

### Development
```bash
git clone https://github.com/AJMitev/FileTypeChecker.git
cd FileTypeChecker
dotnet restore
dotnet build
dotnet test
```

## 💖 Support the Project

If this library helps you, consider supporting its development:

- ⭐ **Star the repository** and share it with others
- ☕ [**Buy me a coffee**](https://www.buymeacoffee.com/ajmitev) for continued development
- 👥 [**Become a member**](https://buymeacoffee.com/ajmitev/membership) for direct access to maintainers

<a href="https://www.buymeacoffee.com/ajmitev" target="_blank">
  <img src="https://www.buymeacoffee.com/assets/img/custom_images/orange_img.png" alt="Buy Me A Coffee" height="41" width="174">
</a>

## 📝 License

This project is licensed under the [MIT License](LICENSE) - see the LICENSE file for details.

## 🙏 Credits

- Based on [mjolka](https://github.com/mjolka)'s Stack Overflow answer: [Guessing a file type based on its content](http://codereview.stackexchange.com/questions/85054/guessing-a-file-type-based-on-its-content)
- Inspired by [0xbrock's FileTypeChecker](https://github.com/0xbrock/FileTypeChecker)
- Completely rewritten with object-oriented design, fluent API, and extensibility

---

<div align="center">
  <strong>Made with ❤️ by <a href="https://github.com/AJMitev">Aleksandar J. Mitev</a></strong>
</div>
