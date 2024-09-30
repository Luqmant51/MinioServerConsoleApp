using ConsoleAppFramework;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel.Args;

namespace MinioServerConsoleApp.Commands.File;

public class FileDelete
{
    private readonly IMinioClient _minio;
    public FileDelete(IMinioClient minio)
    {
        _minio = minio;
    }

    /// <summary>
    /// Deletes a file from the MinIO bucket.
    /// </summary>
    /// <param name="bucketName">The name of the bucket</param>
    /// <param name="objectName">The name of the object to delete</param>
    [Command("file-delete")]
    public async Task DeleteExistingFile(string bucketName, string objectName)
    {
        try
        {
            // Delete the object from MinIO
            await _minio.RemoveObjectAsync(new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName));

            Console.WriteLine($"File '{objectName}' deleted successfully from bucket '{bucketName}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while deleting the file: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }


    /// <summary>
    /// Deletes all objects in a specified MinIO bucket.
    /// </summary>
    /// <param name="bucketName">The name of the bucket from which to delete all objects</param>
    [Command("file-delete-all")]
    public async Task DeleteAllObjects(string bucketName)
    {
        try
        {
            ListObjectsArgs args = new ListObjectsArgs().WithBucket(bucketName);
            var objects = _minio.ListObjectsEnumAsync(args);

            // Iterate through each object and delete it
            await foreach (var obj in objects)
            {
                await _minio.RemoveObjectAsync(new RemoveObjectArgs()
                    .WithBucket(bucketName)
                    .WithObject(obj.Key));

                Console.WriteLine($"Deleted: {obj.Key}");
            }

            Console.WriteLine("All objects deleted successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while deleting objects: {ex.Message}");
            Console.WriteLine($"Stack Trace: {ex.StackTrace}");
        }
    }
}
