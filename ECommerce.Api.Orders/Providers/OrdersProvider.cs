using AutoMapper;
using ECommerce.Api.Orders.Db;
using ECommerce.Api.Orders.Interfaces;
using ECommerce.Api.Orders.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;

namespace ECommerce.Api.Orders.Providers
{
    public class OrdersProvider : IOrdersProvider
    {
        private readonly OrdersDbContext dbContext;
        private readonly ILogger<OrdersProvider> logger;
        private readonly IMapper mapper;
        public OrdersProvider(OrdersDbContext ordersDbContext, ILogger<OrdersProvider> logger, IMapper mapper) 
        {
            this.dbContext = ordersDbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();
        }

        private void SeedData()
        {
            if(!dbContext.Orders.Any())
            {
                dbContext.Orders.Add(new Db.Order
                {
                    Id = 1, CustomerId = 1, OrderDate = DateTime.Now,
                    Items = new List<Db.OrderItem> 
                    {
                        new Db.OrderItem
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 1,
                            UnitPrice = 20
                        }
                    }
                });
                dbContext.SaveChanges();
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId)
        {
            try
            {
                var orders = await dbContext.Orders.Where(o=> o.CustomerId ==  customerId).ToListAsync();
                if (orders != null && orders.Any())
                {
                    var result = mapper.Map<IEnumerable<Db.Order>, IEnumerable<Models.Order>>(orders);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }
    }
}
