using Minio.DataModel.Args;
using Minio;
using ConsoleAppFramework;

namespace MinioServerConsoleApp.Commands.File;

public class FileListing
{
    private readonly IMinioClient _minio;

    public FileListing(IMinioClient minio)
    {
        _minio = minio;
    }

    /// <summary>
    /// Lists all files in a specified MinIO bucket.
    /// </summary>
    /// <param name="bucketName">The name of the bucket to list files from</param>
    [Command("file-list")]
    public async Task ListFiles(string bucketName)
    {
        try
        {
            ListObjectsArgs args = new ListObjectsArgs().WithBucket(bucketName);
            var objects = _minio.ListObjectsEnumAsync(args);

            await foreach (var obj in objects)
            {
                Console.WriteLine($"File: {obj.Key}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while listing files: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }
}