using Application.Common.Interfaces;
using Azure.Storage.Blobs;
using Common;

namespace MicrblogApp.Infrastructure.Services;

public class AzureCloudStorage : ICloudStorage
{
    private readonly BlobServiceClient _blobServiceClient;
    private readonly string _containerName;
    public AzureCloudStorage(BlobServiceClient blobServiceClient, string containerName= "post-images")
    {
        _blobServiceClient = blobServiceClient;
        _containerName = containerName;
    }

    public async Task<string> UploadFileAsync(FileBlob fileBlob)
    {
        // Generate a unique filename
        var fileName = $"{Guid.NewGuid()}.webp";
        var containerClient = _blobServiceClient.GetBlobContainerClient(_containerName);
        await containerClient.CreateIfNotExistsAsync();

        var blobClient = containerClient.GetBlobClient($"{Path.GetFileNameWithoutExtension(fileName)}_{fileBlob.Width}x{fileBlob.Height}.webp");
        await blobClient.UploadAsync(fileBlob.Stream, overwrite: true);

        return blobClient.Uri.ToString();
    }

    public Task DeleteFileAsync(string fileNameForStorage)
    {
        throw new NotImplementedException();
    }
}