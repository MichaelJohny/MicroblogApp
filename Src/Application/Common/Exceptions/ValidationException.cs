using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;

namespace Application.Common.Exceptions;

public class ValidationException : Exception
{
    public ValidationException()
        : base("One or more validation failures have occurred.")
    {
        Failures = new Dictionary<string, string[]>();
    }
    public ValidationException(IEnumerable<IdentityError> failures) : this()
    {
        var codes = failures.Select(e => e.Code).Distinct();
        foreach (var code in codes)
        {
            var codeFailures = failures.Where(e => e.Code == code).Select(e => e.Description)
                .ToArray();
            Failures.Add(code, codeFailures);
        }
    }
    public ValidationException(List<ValidationFailure> failures)
        : this()
    {
        var propertyNames = failures
            .Select(e => e.PropertyName)
            .Distinct();

        foreach (var propertyName in propertyNames)
        {
            var propertyFailures = failures
                .Where(e => e.PropertyName == propertyName)
                .Select(e => e.ErrorMessage)
                .ToArray();

            Failures.Add(propertyName, propertyFailures);
        }
    }

    public IDictionary<string, string[]> Failures { get; }
}