using InvestHB.Domain.Interfaces.Repository;
using InvestHB.Domain.Models;

namespace InvestHB.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private Dictionary<int, List<Order>> cacheOrders {get;set;}

        public OrderRepository()
        {
            cacheOrders = new Dictionary<int, List<Order>>();
            GetOrders();
        }

        public async Task<int> Add(Order order)
        {
            if(!cacheOrders.TryGetValue(order.UserId, out List<Order> orders))
                orders = new List<Order>();                

            orders.Add(order);

            return await Task.Run(() => { return order.UserId; });
        }

        public async Task<List<Order>> Get(int userId)
        {
            if (cacheOrders.TryGetValue(userId, out List<Order> orders))
                return await Task.FromResult(orders);

            return await Task.FromResult(new List<Order>());            
        }
        
        public async Task<bool> Delete(int orderId, int userId)
        {
            if (cacheOrders.TryGetValue(userId, out List<Order> orders))
                cacheOrders.Remove(userId);

            return await Task.FromResult(true);
        }

        private void GetOrders()
        {
           var orders = new List<Order>();
            orders.Add(
                new Order(
                    123,
                    "PETR4",
                    100,
                    OrderSide.Buy,
                    OrderType.Limit,
                    OrderStatus.Open,
                    3000,
                    3000,
                    null,
                    DateTime.Now));

            orders.Add(
             new Order(
                 123,
                 "PETR4",
                 20,
                 OrderSide.Sell,
                 OrderType.Market,
                 OrderStatus.Executed,
                 200,
                 200,
                 null,
                 DateTime.Now));

            orders.Add(
                new Order(
                    123,
                    "BBAS3",
                    1000,
                    OrderSide.Buy,
                    OrderType.Limit,
                    OrderStatus.Open,
                    25000,
                    25000,
                    null,
                    DateTime.Now));

            cacheOrders.Add(123, orders);

            orders = new List<Order>();
            orders.Add(
               new Order(
                   321,
                   "PETR4",
                   200,
                   OrderSide.Buy,
                   OrderType.Market,
                   OrderStatus.Executed,
                   5000,
                   5000,
                   null,
                   DateTime.Now));

            cacheOrders.Add(321, orders);
           
        }
             
    }
}
