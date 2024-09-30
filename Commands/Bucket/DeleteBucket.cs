using ConsoleAppFramework;
using Minio;
using Minio.DataModel.Args;

namespace MinioServerConsoleApp.Commands.Buckets;

public class DeleteBucket
{
    private readonly IMinioClient _minio;
    public DeleteBucket(IMinioClient minio)
    {
        _minio = minio;
    }

    /// <summary>
    /// Delete bucket in the MinIO server
    /// </summary>
    /// <param name="bucketname">Bucket name</param>
    
    [Command("delete-bucket")]
    public async Task DeleteExistingBucket(string bucketname)
    {
        Console.WriteLine(bucketname);
        var checkExistingBucket = new BucketExistsArgs().WithBucket(bucketname);
        bool found = await _minio.BucketExistsAsync(checkExistingBucket);

        if (found)
        {
            await _minio.RemoveBucketAsync(new RemoveBucketArgs().WithBucket(bucketname));
            Console.WriteLine(bucketname + " " + "removed.");
        }
        else
        {
            Console.WriteLine(bucketname + " " + "Bucket does not exist.");
        }
    }
}
