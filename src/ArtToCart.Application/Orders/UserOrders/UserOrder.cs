using ArtToCart.Application.Orders.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Orders;

using FluentResults;

using FluentValidation;

using MapsterMapper;

using MediatR;

namespace ArtToCart.Application.Orders.UserOrders;

public record UserOrderQuery(string BuyerId) : IRequest<Result<UserOrderResponse>>;

public class UserOrderQueryValidator : AbstractValidator<UserOrderQuery>
{
    public UserOrderQueryValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(q => q.BuyerId)
            .NotEmpty()
            .WithMessage("Buyer Id is required");


    }
}

public class UserOrderQueryHandler : IRequestHandler<UserOrderQuery, Result<UserOrderResponse>>
{
    private readonly IRepository<Order> _repository;
    private readonly IMapper _mapper;

    public UserOrderQueryHandler(IRepository<Order> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Result<UserOrderResponse>> Handle(UserOrderQuery request, CancellationToken cancellationToken)
    {
        var order = await _repository.ListAsync(new[] {request.BuyerId});

        if (order == null)
        {
            return new UserOrderResponse(new List<OrderDto>());
        }

        var result = _mapper.Map<List<OrderDto>>(order);

        return new UserOrderResponse(result);
    }
}