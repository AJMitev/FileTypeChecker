namespace FileTypeChecker
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.Common;
    using FileTypeChecker.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Provides you a method for file type validation.
    /// </summary>
    public static class FileTypeValidator
    {
        private static readonly List<Assembly> typesAssemblies = new List<Assembly>
        {
            typeof(FileType).Assembly
        };

        private static bool isInitialized = false;
        private static readonly object initializationLock = new object();

        private static readonly HashSet<Type> knownTypes = new HashSet<Type>();
        private static readonly List<IFileType> types = new List<IFileType>();

        private static IReadOnlyCollection<IFileType> Types
        {
            get
            {
                if (!isInitialized)
                {
                    lock (initializationLock)
                    {
                        if (!isInitialized)
                        {
                            RegisterTypes(typesAssemblies);
                            isInitialized = true;
                        }
                    }
                }

                return types;
            }
        }

        /// <summary>
        /// Checks that the particular type is supported.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>If current type is supported</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        public static bool IsTypeRecognizable(Stream fileContent)
        {
            DataValidator.ThrowIfNull(fileContent, nameof(Stream));

            return Types.Any(type => type.DoesMatchWith(fileContent));
        }

        /// <summary>
        /// Get details about current file type.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Instance of <see cref="IFileType}"/> type.</returns>
        /// /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        public static IFileType GetFileType(Stream fileContent)
        {
            DataValidator.ThrowIfNull(fileContent, nameof(Stream));

            return Types.SingleOrDefault(fileType => fileType.DoesMatchWith(fileContent));
        }

        /// <summary>
        /// Gives you opportunity to register your custom types.
        /// </summary>
        /// <param name="assemblies">Assemblies that contains your custom types.</param>
        public static void RegisterCustomTypes(params Assembly[] assemblies)
        {
            DataValidator.ThrowIfNull(assemblies, nameof(Assembly));

            typesAssemblies.AddRange(assemblies);
            RegisterTypes(assemblies);
        }

        /// <summary>
        /// Validates that the file is from certain type
        /// </summary>
        /// <typeparam name="T">Type that implements FileType</typeparam>
        /// <param name="fileContent">File as stream</param>
        /// <returns>True if file match the desired type otherwise returns false.</returns>
        public static bool Is<T>(Stream fileContent) where T : FileType, IFileType, new()
            => fileContent.Is<T>();
        /// <summary>
        /// Validates that the current file is image.
        /// </summary>
        /// <param name="fileContent">File to check as stream.</param>
        /// <returns>Returns true if the provided file is image otherwise returns false. Supported image types are: Bitmap, JPEG, GIF and PNG.</returns>

        public static bool IsImage(Stream fileContent)
            => fileContent.IsImage();
        /// <summary>
        /// Validates that the current file is archive.
        /// </summary>
        /// <param name="fileContent"File to check as stream.></param>
        /// <returns>Returns true if the provided file is archive otherwise returns false. Supported archive types are: Extensible archive, Gzip, Rar, 7Zip, Tar and Zip.</returns>
        public static bool IsArchive(Stream fileContent)
            => fileContent.IsArchive();

        private static IEnumerable<Type> GetTypesInstance(Assembly assembly)
            => assembly.GetTypes()
                    .Where(type => typeof(IFileType).IsAssignableFrom(type)
                                 && !type.IsAbstract
                                 && !type.IsInterface);

        private static void RegisterTypes(IEnumerable<Assembly> assemblies)
        {
            foreach (Assembly assembly in assemblies)
            {
                foreach (var type in GetTypesInstance(assembly))
                {
                    if (knownTypes.Add(type))
                    {
                        var fileType = (IFileType)Activator.CreateInstance(type);
                        types.Add(fileType);
                    }
                }
            }
        }
    }
}