namespace Application.Common.Interfaces;

public interface IImageProcessingService
{
    Task ProcessImageAsync(Stream imageStream, string originalFileName, string postId);
    string SelectBestImageUrl(IEnumerable<string> imageUrls, int screenWidth, int screenHeight);
}