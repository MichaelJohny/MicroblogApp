using Application.Common.Interfaces;
using Application.Features.Posts.Commands.UploadImage.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Posts.Commands.UploadImage;

public class UploadImageCommand : IRequest<UploadIMageOutputDto>
{
    public IFormFile Image { get; set; }
}

public class UploadImageCommandHandler : IRequestHandler<UploadImageCommand, UploadIMageOutputDto>
{
    private readonly ICloudStorage _cloudStorage;
    private readonly IImageProcessingService _processingService;

    public UploadImageCommandHandler(ICloudStorage cloudStorage, IImageProcessingService processingService)
    {
        _cloudStorage = cloudStorage;
        _processingService = processingService;
    }

    public async Task<UploadIMageOutputDto> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        var postId = Guid.NewGuid().ToString();
        var memoryStream = request.Image.OpenReadStream();
        var originalImageUrl = await _cloudStorage.UploadFileAsync(memoryStream);
        _ = _processingService.ProcessImageAsync(request.Image.OpenReadStream(), request.Image.FileName , postId);
        return new UploadIMageOutputDto(postId, originalImageUrl);
    }
}