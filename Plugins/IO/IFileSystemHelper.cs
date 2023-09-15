using System.Text.Json;
using Nucleus.Api.Enums;

namespace Nucleus.Plugins.IO {
    public interface IFileSystemHelper {
        #region Generic
        
        /// <summary>
        /// Information about the <see cref="Directory"/> that the Helper is linked to
        /// </summary>
        DirectoryInfo Info { get; }
        
        /// <summary>
        /// Checks if the current <see cref="Directory"/> can be Written to
        /// </summary>
        bool IsWriteable { get; }
        
        /// <summary>
        /// Checks if the current <see cref="Directory"/> can be Read from
        /// </summary>
        bool IsReadable { get; }
        
        /// <summary>
        /// Localize a relative path
        /// </summary>
        /// <param name="localpath"></param>
        /// <returns></returns>
        string Path(params string[] localpath);
        
        /// <summary>
        /// Localize a relative directory path
        /// </summary>
        /// <param name="localpath"></param>
        /// <param name="create">Created the directories if they do not exist</param>
        /// <returns></returns>
        DirectoryInfo Directory(string localpath, bool create = false);
        
        /// <summary>
        /// Check if a path exists on the disk
        /// </summary>
        /// <param name="localpath"></param>
        /// <returns></returns>
        bool Exists(string localpath);
        
        /// <summary>
        /// Delete a file from the disk
        /// </summary>
        /// <param name="localpath"></param>
        /// <returns></returns>
        bool Delete(string localpath);
        
        #endregion
        #region Movement
        
        /// <summary>
        /// Move a file from one location to another
        /// <br />This will move files relative to the parent location (eg; If <paramref name="localFrom"/> is <b>"Folder/Folder/File.json"</b> and <paramref name="localTo"/> is <b>"File2.json"</b>, the new location will be <b>"~/File2.json"</b>)
        /// </summary>
        /// <param name="localFrom">The file <b>(relative to the parent folder)</b> to move</param>
        /// <param name="localTo">The location <b>(relative to the parent folder)</b> to move to</param>
        /// <param name="replace">Whether to replace the file (<paramref name="localTo"/>) if it exists already</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        /// <returns></returns>
        bool Move(string localFrom, string localTo, bool replace = false);
        
        /// <summary>
        /// Move a file from one location to another
        /// <br />This will move files relative to the location entered in <paramref name="localFrom"/> (eg; If <paramref name="localFrom"/> is <b>"Folder/Folder/File.json"</b> and <paramref name="fileName"/> is <b>"File2.json"</b>, the new location will be <b>"Folder/Folder/File2.json"</b>)
        /// </summary>
        /// <param name="localFrom">The file <b>(relative to the parent folder)</b> to move</param>
        /// <param name="fileName">The location <b>(relative to the <paramref name="localFrom"/> entry)</b> to move to</param>
        /// <param name="replace">Whether to replace the file (<paramref name="fileName"/>) if it exists already</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        /// <returns></returns>
        bool MoveTransitive(string localFrom, string fileName, bool replace = false);
        
        #endregion
        #region Writing
        
        /// <summary>
        /// Write a string to a file
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="contents"></param>
        /// <param name="mode">The mode to open the File in (Eg; <see cref="FileMode.OpenOrCreate"/> will create or override the file, <see cref="FileMode.Append"/> will write additional data to the end of the file)</param>
        /// <param name="cancellation">Cancellation token for asynchronous writing</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask WriteAsync(string localpath, string contents, FileMode mode = FileMode.OpenOrCreate, CancellationToken cancellation = default);
        
        /// <summary>
        /// Write a Text Stream to a file
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="stream"></param>
        /// <param name="mode">The mode to open the File in (Eg; <see cref="FileMode.OpenOrCreate"/> will create or override the file, <see cref="FileMode.Append"/> will write additional data to the end of the file)</param>
        /// <param name="cancellation">Cancellation token for asynchronous writing</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask WriteAsync(string localpath, Stream stream, FileMode mode = FileMode.OpenOrCreate, CancellationToken cancellation = default);
        
        /// <summary>
        /// Write a Text Stream to a file
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="stream"></param>
        /// <param name="mode">The mode to open the File in (Eg; <see cref="FileMode.OpenOrCreate"/> will create or override the file, <see cref="FileMode.Append"/> will write additional data to the end of the file)</param>
        /// <param name="cancellation">Cancellation token for asynchronous writing</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask WriteAsync(string localpath, Task<Stream> stream, FileMode mode = FileMode.OpenOrCreate, CancellationToken cancellation = default);
        
        /// <summary>
        /// Write the Response from an HTTP Request to a file
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="response"></param>
        /// <param name="mode">The mode to open the File in (Eg; <see cref="FileMode.OpenOrCreate"/> will create or override the file, <see cref="FileMode.Append"/> will write additional data to the end of the file)</param>
        /// <param name="cancellation">Cancellation token for asynchronous writing</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask WriteAsync(string localpath, HttpResponseMessage response, FileMode mode = FileMode.OpenOrCreate, CancellationToken cancellation = default);
        
        /// <summary>
        /// Write JSON to a file
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="contents"></param>
        /// <param name="jsonOptions">If you need to (de)serialize custom JSON types</param>
        /// <param name="cancellation">Cancellation token for asynchronous writing</param>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask WriteJsonAsync<T>(string localpath, T contents, JsonSerializerOptions? jsonOptions = null, CancellationToken cancellation = default);
        
        /// <summary>
        /// Write lines of text to a file
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="contents"></param>
        /// <param name="mode">The mode to open the File in (Eg; <see cref="FileMode.OpenOrCreate"/> will create or override the file, <see cref="FileMode.Append"/> will write additional data to the end of the file)</param>
        /// <param name="cancellation">Cancellation token for asynchronous writing</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask WriteLinesAsync(string localpath, IEnumerable<string?> contents, FileMode mode = FileMode.OpenOrCreate, CancellationToken cancellation = default);
        
        /// <summary>
        /// Write lines of text to a file
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="contents"></param>
        /// <param name="mode">The mode to open the File in (Eg; <see cref="FileMode.OpenOrCreate"/> will create or override the file, <see cref="FileMode.Append"/> will write additional data to the end of the file)</param>
        /// <param name="cancellation">Cancellation token for asynchronous writing</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask WriteLinesAsync(string localpath, IAsyncEnumerable<string?> contents, FileMode mode = FileMode.OpenOrCreate, CancellationToken cancellation = default);
        
        #endregion
        #region Reading
        
        /// <summary>
        /// Read a full file
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="cancellation">Cancellation token for asynchronous reading</param>
        /// <returns></returns>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask<string> ReadAsync(string localpath, CancellationToken cancellation = default);
        
        /// <summary>
        /// Read a JSON file and Parse it
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="jsonOptions">If you need to (de)serialize custom JSON types</param>
        /// <param name="cancellation">Cancellation token for asynchronous reading</param>
        /// <typeparam name="T"></typeparam>
        /// <returns>JSON deserialized version of <see cref="T"/> or default(<see cref="T"/>)</returns>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        ValueTask<T?> ReadJsonAsync<T>(string localpath, JsonSerializerOptions? jsonOptions = null, CancellationToken cancellation = default);
        
        /// <summary>
        /// Read a file line by line
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="cancellation">Cancellation token for asynchronous reading</param>
        /// <returns></returns>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        IAsyncEnumerable<string> ReadLinesAsync(string localpath, CancellationToken cancellation = default);
        
        /// <summary>
        /// Reads a file and returns a ShaSum
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="type">The type of Sum to create</param>
        /// <param name="cancellation">Cancellation token for asynchronous reading</param>
        /// <exception cref="IOException">If permission is denied to the FileSystem</exception>
        /// <returns></returns>
        ValueTask<string> ShasumAsync(string localpath, HashType type = HashType.SHA1, CancellationToken cancellation = default);
        
        #endregion
        #region Watchers
        
        /// <summary>
        /// Watch a local path (File or directory) for changes
        /// </summary>
        /// <param name="localpath">A file path localized to the Plugins Folder</param>
        /// <param name="action">Called whenever a change occurs</param>
        /// <returns>A disposable that can be called to stop watching</returns>
        IDisposable Watch(string localpath, Action action);
        
        #endregion
    }
}
