namespace Common;

public static class Constants
{
    public static readonly int[][] ImageSizes = new[]
    {
        new[] { 640, 480 }, // Standard
        new[] { 1280, 720 }, // HD
        new[] { 1920, 1080 }, // Full HD
        new[] { 2560, 1440 }, // QHD
    };
    public static int[] GetRandomImageSize()
    {
        Random random = new Random();
        int randomIndex = random.Next(ImageSizes.Length);
        return ImageSizes[randomIndex];
    }
    
}
