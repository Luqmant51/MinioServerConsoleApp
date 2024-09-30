using ConsoleAppFramework;
using Minio;
using Minio.DataModel.Args;

namespace MinioServerConsoleApp.Commands.File;

public class FileDownload
{

    private readonly IMinioClient _minio;
    public FileDownload(IMinioClient minio)
    {
        _minio = minio;
    }

    /// <summary>
    /// Downloads a file from MinIO bucket to local file system
    /// </summary>
    /// <param name="minio">The MinIO client instance</param>
    /// <param name="bucketName">The name of the bucket</param>
    /// <param name="objectName">The name of the object to download</param>
    /// <param name="destinationPath">The path where the file will be saved</param>

    [Command("file-download")]
    public async Task DownloadFile(string bucketName, string objectName, string destinationPath)
    {
        try
        {
            // Ensure the directory exists
            Directory.CreateDirectory(Path.GetDirectoryName(destinationPath));

            // Download the object from MinIO
            await _minio.GetObjectAsync(new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithCallbackStream(async (stream) =>
                {
                    using var fileStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write);
                    await stream.CopyToAsync(fileStream);
                    Console.WriteLine($"File '{objectName}' downloaded successfully to '{destinationPath}'.");
                }));
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"Access denied: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"I/O error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while downloading: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }

}
