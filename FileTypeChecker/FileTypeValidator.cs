namespace FileTypeChecker
{
    using FileTypeChecker.Abstracts;
    using FileTypeChecker.Common;
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

        private static bool isInitialized;
        private static readonly List<IFileType> types = new List<IFileType>();


        private static ICollection<IFileType> Types
        {
            get
            {
                if (!isInitialized)
                {
                    RegisterTypes();
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
        /// <exception cref="System.A rgumentNullException"></exception>
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
            DataValidator.ThrowIfNull(assemblies,nameof(Assembly));

            typesAssemblies.AddRange(assemblies);
            RegisterTypes();
        }

        private static void RegisterTypes()
        {
            foreach (Assembly assembly in typesAssemblies)
            {
                types.AddRange(GetTypesInstance(assembly));
            }


            isInitialized = true;
        }

        private static IEnumerable<IFileType> GetTypesInstance(Assembly assembly)
        {
            return assembly.GetTypes()
                    .Where(type => typeof(IFileType).IsAssignableFrom(type)
                                 && !type.IsAbstract
                                 && !type.IsInterface)
                .Select(selectedType => (IFileType)Activator.CreateInstance(selectedType));
        }
    }
}