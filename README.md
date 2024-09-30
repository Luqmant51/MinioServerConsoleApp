
# MinioServerConsoleApp

A .NET Console Application to interact with MinIO, an object storage solution compatible with Amazon S3 APIs.

## Features

- Create and delete buckets
- List all buckets
- Upload and download files
- Delete files
- List all files in a bucket

## Prerequisites

- .NET 8 SDK
- MinIO server running (see [Setting Up MinIO](#setting-up-minio))

## Setting Up MinIO

To run MinIO using Docker, execute the following command:

```bash
docker run -p 9000:9000 -p 9001:9001 --name minio \
  -e "MINIO_ACCESS_KEY=your-access-key" \
  -e "MINIO_SECRET_KEY=your-secret-key" \
  minio/minio server /data --console-address ":9001"
```

This will expose MinIO on ports `9000` for API and `9001` for the web console. You can access the MinIO dashboard using `http://localhost:9001`.

## Running the Application

Once you have MinIO running, you can use the application to interact with the server.

### Build the Application

Before running the commands, ensure you build the application:

```bash
dotnet build
```

Navigate to the folder containing the executable after building the project:

```bash
cd bin/Debug/net8.0/
```

### Available Commands

- **List all buckets**:
  
  ```bash
  ./MinioServerConsoleApp.exe list-buckets
  ```

- **Create a new bucket**:

  ```bash
  ./MinioServerConsoleApp.exe create-bucket --bucketname "my-new-bucket"
  ```

- **Delete an existing bucket**:

  ```bash
  ./MinioServerConsoleApp.exe delete-bucket --bucketname "my-existing-bucket"
  ```

- **Upload a file**:

  ```bash
  ./MinioServerConsoleApp.exe file-upload --bucketname "mybucket" --filepath "/path/to/file"
  ```

- **Download a file**:

  ```bash
  ./MinioServerConsoleApp.exe file-download --bucketname "mybucket" --objectname "file.txt" --destinationpath "/path/to/download/"
  ```

- **Delete a file**:

  ```bash
  ./MinioServerConsoleApp.exe file-delete --bucketname "mybucket" --objectname "file.txt"
  ```

- **Delete all files in a bucket**:

  ```bash
  ./MinioServerConsoleApp.exe file-delete-all --bucketname "mybucket"
  ```

- **List all files in a bucket**:

  ```bash
  ./MinioServerConsoleApp.exe file-list --bucketname "mybucket"
  ```

## Additional Resources

For more information, refer to the official [MinIO documentation](https://docs.min.io/).

Use your own  MinIO server and credentials to test the commands. The commands are designed to be used with a Min