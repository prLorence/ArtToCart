using ArtToCart.Application.Orders.Shared;
using ArtToCart.Application.Shared.Interfaces;
using ArtToCart.Domain.Baskets;
using ArtToCart.Domain.Orders;
using ArtToCart.Domain.Orders.ValueObjects;
using ArtToCart.Domain.Products;
using ArtToCart.Domain.Products.ValueObjects;

using FluentResults;

using FluentValidation;

using MapsterMapper;

using MediatR;

namespace ArtToCart.Application.Orders.CreateOrder;

public record CreateOrderCommand(string BasketId, Address ShippingAddress) : IRequest<Result<CreateOrderResponse>>;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        CascadeMode = CascadeMode.Stop;

        RuleFor(c => c.BasketId)
            .NotEmpty()
            .WithMessage("Buyer id is required");

        RuleFor(c => c.ShippingAddress)
            .NotEmpty()
            .WithMessage("Shipping Address is required");
    }
}

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Result<CreateOrderResponse>>
{
    // repositories are the only way for cross layer communication, find a better way for this
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<CatalogItem> _productsRepository;
    private readonly IRepository<Basket> _basketRepository;
    private readonly IMapper _mapper;

    public CreateOrderCommandHandler(IRepository<CatalogItem> productsRepository, IRepository<Order> orderRepository, IRepository<Basket> basketRepository, IMapper mapper)
    {
        _productsRepository = productsRepository;
        _orderRepository = orderRepository;
        _basketRepository = basketRepository;
        _mapper = mapper;
    }

    public async Task<Result<CreateOrderResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var basket = await _basketRepository.FirstOrDefaultAsync(request.BasketId);

        if (basket == null)
        {
            return Result.Fail("Bad request: Checking out on empty basket");
        }

        // use this as a reference for product repository
        // product repository needs a query for getting the selected items using the array of ids
        var basketItemIds = basket.Items.Select(bi => bi.CatalogItemId).ToArray();

        var selectedItems = await _productsRepository.ListAsync(basketItemIds);

        var items = basket.Items.Select(basketItem =>
        {
            var catalogItem = selectedItems.First(c => Equals(c.Id, CatalogItemId.CreateFrom(basketItem.CatalogItemId)));
            var itemOrdered = new CatalogItemOrdered(catalogItem.Id, catalogItem.Name, catalogItem.Images[0].ImageUrl);
            var orderItem = OrderItem.Create(itemOrdered, basketItem.UnitPrice, basketItem.Quantity);
            return orderItem;
        }).ToList();


        var order = Order.Create(basket.BuyerId, request.ShippingAddress, items);

        await _orderRepository.AddAsync(order);

        basket.RemoveCheckedOutItems();

        _basketRepository.UpdateAsync(basket);

        var result = _mapper.Map<OrderDto>(order);

        return new CreateOrderResponse(result);
    }
}