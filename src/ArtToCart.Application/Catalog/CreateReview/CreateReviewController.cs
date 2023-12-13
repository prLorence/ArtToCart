using ArtToCart.Web;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ArtToCart.Application.Catalog.CreateReview;

[Route("/products/review")]
public class CreateReviewController : BaseController
{
    private readonly ISender _sender;

    public CreateReviewController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReview(CreateReviewRequest request)
    {
        var command = new CreateReviewCommand(request.CatalogItemId, request.Value, request.BuyerId, request.Rating);

        var result = await _sender.Send(command);

        if (result.IsFailed)
        {
            return BadRequest(result.Errors.Select(e => e.Reasons));
        }

        return Ok(request.Value);
    }

}