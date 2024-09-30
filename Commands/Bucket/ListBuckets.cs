using Minio;

namespace MinioServerConsoleApp.Commands.Buckets;

public class ListBuckets
{
    private readonly IMinioClient _minio;
    public ListBuckets(IMinioClient minio)
    {
        _minio = minio;
    }
    /// <summary>
    /// Lists all buckets in the MinIO server
    /// </summary>
    public async Task ListAllBuckets()
    {
        try
        {
            // Get the list of buckets
            var bucketsResult = await _minio.ListBucketsAsync();
            Console.WriteLine("Buckets:");

            // Iterate over the buckets using the Buckets property
            foreach (var bucket in bucketsResult.Buckets)
            {
                Console.WriteLine($"- {bucket.Name} (Created on: {bucket.CreationDate})");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error occurred while listing buckets: {ex.Message}");
        }
    }
}
