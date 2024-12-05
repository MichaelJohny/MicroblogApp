using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Posts.Commands.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        // Validate Content
        RuleFor(command => command.Content)
            .NotEmpty().WithMessage("Content is required.")
            .MaximumLength(140).WithMessage("Content cannot exceed 140 characters.");
    }
}