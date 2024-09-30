using ConsoleAppFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Minio;
using MinioServerConsoleApp.Commands.Buckets;
using MinioServerConsoleApp.Commands.File;
using System.Net;

// Set security protocol
ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12
                                       | SecurityProtocolType.Tls11
                                       | SecurityProtocolType.Tls12;

try
{
    // Host builder with service configuration
    IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {
            var minioConfig = context.Configuration.GetSection("Minio");
            var endpoint = minioConfig["Endpoint"];
            var accessKey = minioConfig["AccessKey"];
            var secretKey = minioConfig["SecretKey"];
            services.AddSingleton<IMinioClient>(sp =>
                new MinioClient()
                    .WithEndpoint(endpoint, 9000)
                    .WithCredentials(accessKey, secretKey)
                    .Build());
        });

    using IHost host = hostBuilder.Build();

    ConsoleApp.ServiceProvider = host.Services;
    ConsoleApp.ConsoleAppBuilder consoleApp = ConsoleApp.Create();

    // Register commands in ConsoleAppFramework
    // Bucket Commands
    consoleApp.Add<CreateBucket>();
    consoleApp.Add<ListBuckets>();
    consoleApp.Add<DeleteBucket>();


    //File Commands
    consoleApp.Add<FileUploader>();
    consoleApp.Add<FileDownload>();
    consoleApp.Add<FileListing>();
    consoleApp.Add<FileDelete>();

    await consoleApp.RunAsync(args);
}
catch (Exception ex)
{
    Console.WriteLine($"Error initializing MinIO client: {ex.Message}");
}
