using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Posts.Commands.UploadImage;

public class UploadImageValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageValidator()
    {
        // Validate Image
        RuleFor(command => command.Image)
            .Must(BeAValidImageType).WithMessage("Only JPG, PNG, and WebP formats are allowed.")
            .Must(HaveValidSize).WithMessage("Image size must not exceed 2MB.")
            .When(command => command.Image != null); // Validate only if an image is uploaded    
    }

    private bool BeAValidImageType(IFormFile image)
    {
        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var fileExtension = Path.GetExtension(image.FileName)?.ToLower();
        return allowedExtensions.Contains(fileExtension);
    }

    private bool HaveValidSize(IFormFile image)
    {
        const int maxSizeInBytes = 2 * 1024 * 1024; // 2MB
        return image.Length <= maxSizeInBytes;
    }
}