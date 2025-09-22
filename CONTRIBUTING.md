# Contributing to FileTypeChecker

Thank you for your interest in contributing to FileTypeChecker! This document provides guidelines for contributing to this .NET library for file type validation using magic number detection.

## Getting Started

### Prerequisites
- .NET SDK 6.0 or later
- Git

### Setting Up the Development Environment

1. Fork and clone the repository:
   ```bash
   git clone https://github.com/yourusername/FileTypeChecker.git
   cd FileTypeChecker
   ```

2. Build the project:
   ```bash
   dotnet build
   ```

3. Run tests to ensure everything works:
   ```bash
   dotnet test
   ```

## Development Workflow

### Building and Testing

- **Build the library**: `dotnet build`
- **Run all tests**: `dotnet test`
- **Run async tests only**: `dotnet test --filter "FileTypeValidatorAsyncTests|StreamExtensionsAsyncTests"`
- **Test the sample app**: 
  ```bash
  cd Samples/FileTypeChecker.App
  dotnet run
  ```

### Code Style and Standards

- Follow standard C# coding conventions
- Use meaningful variable and method names
- Add XML documentation for all public APIs
- Maintain both synchronous and asynchronous API support
- Include appropriate unit tests for new functionality

## Types of Contributions

### Adding New File Types

To add support for a new file type:

1. Create a new class in `FileTypeChecker/Types/` inheriting from `FileType`
2. Implement the required magic sequences and validation logic
3. Add both sync and async support
4. Include comprehensive tests with sample files
5. Update documentation if needed

Example:
```csharp
public class MyFileType : FileType
{
    public override string Name => "My File Format";
    public override string[] Extensions => new[] { "myf" };
    
    public override bool IsMatch(ReadOnlySpan<byte> data)
    {
        // Implementation
    }
    
    public override async Task<bool> IsMatchAsync(Stream stream, CancellationToken cancellationToken = default)
    {
        // Async implementation
    }
}
```

### Bug Fixes

1. Create an issue describing the bug
2. Write a failing test that reproduces the issue
3. Fix the bug
4. Ensure all tests pass
5. Submit a pull request

### Documentation Improvements

- Update XML documentation for public APIs
- Improve code comments for complex logic
- Update README or other documentation files as needed

## Testing Guidelines

### Test Requirements

- All new features must include comprehensive tests
- Tests should cover both synchronous and asynchronous APIs
- Include test files in `FileTypeChecker.Tests/files/` for file type validation tests
- Use descriptive test method names
- Test edge cases and error conditions

### Test File Organization

- Place test files in `FileTypeChecker.Tests/files/`
- Use representative file samples for each supported format
- Keep test files small when possible
- Document any special test file requirements

## Pull Request Process

1. **Fork** the repository
2. **Create** a feature branch from `master`
3. **Make** your changes following the coding standards
4. **Add** or update tests as appropriate
5. **Ensure** all tests pass locally
6. **Update** documentation if needed
7. **Submit** a pull request with a clear description

### Pull Request Guidelines

- Use a descriptive title
- Provide a detailed description of changes
- Reference any related issues
- Include screenshots or examples if applicable
- Ensure CI checks pass

## Code Review Process

- All submissions require review
- Maintainers will provide feedback and may request changes
- Address feedback promptly
- Once approved, maintainers will merge the PR

## Performance Considerations

- Keep file reading to a minimum (analyze only necessary bytes)
- Implement efficient byte comparison algorithms
- Consider memory allocation patterns
- Profile performance-critical code paths
- Maintain async/await best practices for non-blocking I/O

## Architecture Guidelines

### Core Principles

1. **Dual API Support**: Provide both sync and async methods for all operations
2. **Performance**: Minimize I/O operations and memory allocations

### Key Components

- **FileTypeValidator**: Main entry point for validation
- **IFileType/FileType**: Interface and base class for file type implementations
- **MagicSequence**: Binary signature representation
- **StreamExtensions**: Fluent validation methods

## Getting Help

- Create an issue for bugs or feature requests
- Use discussions for questions about usage or contribution
- Check existing issues and documentation before asking questions

## License

By contributing, you agree that your contributions will be licensed under the same license as the project.
