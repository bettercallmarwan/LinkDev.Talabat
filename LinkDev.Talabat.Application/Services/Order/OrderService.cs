using LinkDev.Talabat.Core.Application.Abstraction.Models.Orders;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Basket;
using LinkDev.Talabat.Core.Domain.Contracts.Persistence;
using LinkDev.Talabat.Core.Domain.Entities.Orders;
using LinkDev.Talabat.Core.Domain.Entities.Products;
using LinkDev.Talabat.Core.Application.Abstraction.Services.Orders;
using AutoMapper;
using LinkDev.Talabat.Core.Application.Exceptions;

namespace LinkDev.Talabat.Core.Application.Services.Order
{
    internal class OrderService(IBasketService basketService, IUnitOfWork unitOfWork, IMapper mapper) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAync(string buyerEmail, OrderToCreateDto order)
        {
            var basket = await basketService.GetCustoemrBasketAsync(order.BasketId);

            // 2. get selected items in basket from products repo
            var orderItems = new List<OrderItem>();
            if (basket.Items.Count() > 0)
            {
                var productRepo = unitOfWork.GetRepository<Product, int>();
                foreach (var item in basket.Items)
                {
                    var product = await productRepo.GetAsync(item.Id);

                    if (product is not null)
                    {
                        var productItemOrdered = new ProductItemOrdered()
                        {
                            ProductId = product.Id,
                            ProductName = product.Name,
                            PictureUrl = product.PictureUrl ?? ""
                        };

                        var orderItem = new OrderItem()
                        {
                            product = productItemOrdered,
                            Price = product.Price,
                            Quantity = item.Quantity
                        };

                        orderItems.Add(orderItem);
                    }
                }
            }

            // 3. calculate subtotal

            var subTotal = orderItems.Sum(item => item.Price * item.Quantity);

            // 4.map address

            var address = mapper.Map<Address>(order.ShippingAddress);

            // 4. create order

            var orderToCreate = new Domain.Entities.Orders.Order() // Fully qualify the Order class
            {
                BuyerEmail = buyerEmail,
                ShippingAddress = address,
                DeliveryMethodId = order.DeliveryMethodId,
                Items = orderItems,
                SubTotal = subTotal
            };

            await unitOfWork.GetRepository<Domain.Entities.Orders.Order, int>().AddAsync(orderToCreate);

            var created = await unitOfWork.CompleteAsync() > 0;

            if (!created) throw new BadRequestException("an error has been occured during creating order");

            return mapper.Map<OrderToReturnDto>(orderToCreate);
        }

        public Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodAsync()
        {
            throw new NotImplementedException();

        }

        public Task<OrderToReturnDto> GetOrderByIdAsync(string buyerEmail, int orderId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderToReturnDto>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
