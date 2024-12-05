using Common;

namespace Application.Common.Interfaces;

public interface ICloudStorage
{
    Task<string> UploadFileAsync(FileBlob fileBlob);
    Task<string> UploadFileAsync(Stream stream);
    Task DeleteFileAsync(string fileNameForStorage);
}