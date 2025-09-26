namespace FileTypeChecker.Abstracts
{
    using System.IO;
    using System.Threading.Tasks;

    public interface IFileType
    {
        /// <summary>
        /// Returns the name of the current <see cref="IFileType"/>.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Returns the extension of the current <see cref="IFileType"/> without dot.
        /// </summary>
        string Extension { get; }
        
        /// <summary>
        /// Returns the MIME type of the current <see cref="IFileType"/>
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// Returns the max length of all the magic sequences added to the <see cref="FileType"/>.
        /// </summary>
        int MaxMagicSequenceLength { get; }

        /// <summary>
        /// Checks if current <see cref="IFileType"/> matches with a file from stream.
        /// </summary>
        /// <param name="stream">File as a stream.</param>
        /// <param name="resetPosition"></param>
        /// <returns>Does file from stream match with current?</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        bool DoesMatchWith(Stream stream, bool resetPosition = true);

        /// <summary>
        /// Checks if current <see cref="IFileType"/> matches with a file from stream.
        /// </summary>
        /// <param name="bytes">File as a byte array.</param>
        /// <returns>Does file from stream match with current?</returns>
        /// <exception cref="System.ArgumentException"></exception>
        /// <exception cref="System.ArgumentNullException"></exception>
        /// <exception cref="System.NotSupportedException"></exception>
        /// <exception cref="System.ObjectDisposedException"></exception>
        bool DoesMatchWith(byte[] bytes);

        /// <summary>
        /// Asynchronously checks if the current <see cref="IFileType"/> matches with the file from stream.
        /// </summary>
        /// <param name="stream">File as a stream.</param>
        /// <param name="resetPosition">Whether to reset the stream position to the beginning before reading.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains true if the file matches; otherwise, false.</returns>
        /// <exception cref="System.ArgumentNullException">Thrown when <paramref name="stream"/> is null.</exception>
        /// <exception cref="System.NotSupportedException">Thrown when the stream does not support reading or seeking.</exception>
        /// <exception cref="System.ObjectDisposedException">Thrown when the stream has been disposed.</exception>
        Task<bool> DoesMatchWithAsync(Stream stream, bool resetPosition = true);

        /// <summary>
        /// Returns a score that represents how closely the file type matches the input. Higher values indicate better matches.
        /// </summary>
        /// <param name="stream">File content as a stream.</param>
        /// <returns>A numeric score representing the match quality.</returns>
        int GetMatchingNumber(Stream stream);

        /// <summary>
        /// Returns a score that represents how closely the file type matches the input. Higher values indicate better matches.
        /// </summary>
        /// <param name="bytes">File content as a byte array.</param>
        /// <returns>A numeric score representing the match quality.</returns>
        int GetMatchingNumber(byte[] bytes);

        // ReadOnlySpan<byte> overloads for high-performance scenarios

        /// <summary>
        /// Checks if the current <see cref="IFileType"/> matches with the file data.
        /// This is a high-performance overload that avoids memory allocations.
        /// </summary>
        /// <param name="bytes">File content as ReadOnlySpan of bytes.</param>
        /// <returns>True if the file matches; otherwise, false.</returns>
        bool DoesMatchWith(System.ReadOnlySpan<byte> bytes);

        /// <summary>
        /// Returns a score that represents how closely the file type matches the input. Higher values indicate better matches.
        /// This is a high-performance overload that avoids memory allocations.
        /// </summary>
        /// <param name="bytes">File content as ReadOnlySpan of bytes.</param>
        /// <returns>A numeric score representing the match quality.</returns>
        int GetMatchingNumber(System.ReadOnlySpan<byte> bytes);
    }
}