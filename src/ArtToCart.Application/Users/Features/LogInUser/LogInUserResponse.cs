namespace ArtToCart.Application.Users.Features.LogInUser;

public class LogInUserResponse
{
    public string? Message { get; set; }
    public string? Token { get; set; }
    public object? FailedObjectResult { get; set; }

}