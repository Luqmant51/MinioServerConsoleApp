using ConsoleAppFramework;
using Microsoft.AspNetCore.StaticFiles;
using Minio;
using Minio.DataModel.Args;

namespace MinioServerConsoleApp.Commands.File;


public class FileUploader
{
    private readonly IMinioClient _minio;
    public FileUploader(IMinioClient minio)
    {
        _minio = minio;
    }

    /// <summary>
    /// Upload a file to MinIO bucket from local file system
    /// </summary>
    /// <param name="bucketname">The name of the bucket</param>
    /// <param name="filepath">The path of the object to upload</param>

    [Command("file-upload")]
    public async Task FileUpload(string bucketname, string filepath)
    {
        var objectName = Path.GetFileName(filepath);
        var provider = new FileExtensionContentTypeProvider();

        try
        {
            using var fileStream = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            var contentType = provider.TryGetContentType(objectName, out var mimeType) ? mimeType : "application/octet-stream";

            // Upload the file to MinIO
            await _minio.PutObjectAsync(new PutObjectArgs()
                .WithBucket(bucketname)
                .WithObject(objectName)
                .WithStreamData(fileStream)
                .WithObjectSize(fileStream.Length)
                .WithContentType(contentType)).ConfigureAwait(false);

            Console.WriteLine($"File '{objectName}' uploaded successfully.");
            Console.WriteLine($"Method Called: {nameof(_minio.PutObjectAsync)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred: {ex.Message}");
        }
    }
}
