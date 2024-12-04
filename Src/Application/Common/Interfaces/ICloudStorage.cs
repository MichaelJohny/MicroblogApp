using Common;

namespace Application.Common.Interfaces;

public interface ICloudStorage
{
    Task<string> UploadFileAsync(FileBlob fileBlob);
    Task DeleteFileAsync(string fileNameForStorage);
}