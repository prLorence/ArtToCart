namespace ArtToCart.Application.Shared.Exceptions;

public sealed class ValidationException(IReadOnlyDictionary<string, string[]> errorsDictionary)
    : Exception("Validation Failure, One or more validation errors occurred")
{
    public IReadOnlyDictionary<string, string[]> ErrorsDictionary { get; } = errorsDictionary;
}