using Application.Common.Interfaces;
using Common;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;
using Size = SixLabors.ImageSharp.Size;

namespace MicrblogApp.Infrastructure.Services;

using static Constants;

public class ImageProcessingService
{
    private readonly ICloudStorage _cloudStorage;


    public ImageProcessingService(ICloudStorage cloudStorage)
        => _cloudStorage = cloudStorage;


    public async Task<string> ProcessImageAsync(Stream imageStream, string originalFileName)
    {
        // Generate a unique filename
        var fileName = $"{Guid.NewGuid()}.webp";

        try
        {
            // Process and resize images
            var processedImages = await ResizeImagesAsync(imageStream, fileName);

            // Upload processed images to Azure Blob Storage
            var uploadTasks = processedImages.Select(async (fileBlob)
                => await _cloudStorage.UploadFileAsync(fileBlob));

            var uploadedUrls = await Task.WhenAll(uploadTasks);

            // Return the URL of the first (default) image
            return uploadedUrls.First();
        }
        catch (Exception ex)
        {
            // Log the exception
            throw new ImageProcessingException($"Failed to process image: {ex.Message}", ex);
        }
    }

    private async Task<List<FileBlob>> ResizeImagesAsync(Stream originalImageStream, string fileName)
    {
        var processedImages = new List<FileBlob>();

        using (var image = await Image.LoadAsync(originalImageStream))
        {
            foreach (var size in ImageSizes)
            {
                var resizedImage = image.Clone(ctx => ctx.Resize(new ResizeOptions
                {
                    Size = new Size(size[0], size[1]),
                    Mode = ResizeMode.Max
                }));

                // Convert to WebP
                var outputStream = new MemoryStream();
                await resizedImage.SaveAsync(outputStream, new WebpEncoder());
                outputStream.Position = 0;

                processedImages.Add(new FileBlob(size[0], size[1], outputStream));
            }
        }

        return processedImages;
    }

    // Best image size selection based on screen dimensions
    public string SelectBestImageUrl(IEnumerable<string> imageUrls, int screenWidth, int screenHeight)
    {
        // If no images, return null
        if (!imageUrls.Any())
            return null;

        // If only one image, return it
        if (imageUrls.Count() == 1)
            return imageUrls.First();

        // Extract size from filename (assumes filename format: guid_WIDTHxHEIGHT.webp)
        var bestMatchUrl = imageUrls
            .Select(url =>
            {
                var filename = Path.GetFileNameWithoutExtension(url);
                var sizePart = filename.Split('_').LastOrDefault();

                if (string.IsNullOrEmpty(sizePart))
                    return (Url: url, Diff: int.MaxValue);

                var parts = sizePart.Split('x');
                if (parts.Length != 2)
                    return (Url: url, Diff: int.MaxValue);

                int width = int.Parse(parts[0]);
                int height = int.Parse(parts[1]);

                // Calculate difference from screen dimensions
                int diff = Math.Abs(width - screenWidth) + Math.Abs(height - screenHeight);
                return (Url: url, Diff: diff);
            })
            .OrderBy(x => x.Diff)
            .First()
            .Url;

        return bestMatchUrl;
    }
}

// Custom exception for image processing errors
public class ImageProcessingException : Exception
{
    public ImageProcessingException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
}

//
// public class ImageProcessingBackgroundService : BackgroundService
// {
//     private readonly IImageProcessingService _imageProcessingService;
//     private readonly ILogger<ImageProcessingBackgroundService> _logger;
//
//     public ImageProcessingBackgroundService(
//         IImageProcessingService imageProcessingService,
//         ILogger<ImageProcessingBackgroundService> logger)
//     {
//         _imageProcessingService = imageProcessingService;
//         _logger = logger;
//     }
//
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         while (!stoppingToken.IsCancellationRequested)
//         {
//             try
//             {
//                 // Implement queue-based image processing logic
//                 // This is a placeholder for actual implementation
//                 await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
//             }
//             catch (Exception ex)
//             {
//                 _logger.LogError(ex, "Error in image processing background service");
//                 await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
//             }
//         }
//     }
// }