using ConsoleAppFramework;
using Minio;
using Minio.DataModel.Args;
using System.Xml.Linq;

namespace MinioServerConsoleApp.Commands.Buckets;

public class CreateBucket
{
    private readonly IMinioClient _minio;
    public CreateBucket(IMinioClient minio)
    {
        _minio = minio;
    }

    /// <summary>
    /// Create new buckets in the MinIO server
    /// </summary>
    /// <param name="bucketname">Bucket name</param>

    [Command("create-bucket")]
    public async Task CreateNewBucket(string bucketname)
    {
        var checkExistingBucket = new BucketExistsArgs().WithBucket(bucketname);
        bool found = await _minio.BucketExistsAsync(checkExistingBucket);

        if (found)
        {
            var mkBktArgs = new MakeBucketArgs().WithBucket(bucketname);
            await _minio.MakeBucketAsync(mkBktArgs);
            Console.WriteLine(bucketname + " " + "Created successfully.");
        }
        else
        {
            Console.WriteLine(bucketname + " " + "already exists.");
        }
    }
}
