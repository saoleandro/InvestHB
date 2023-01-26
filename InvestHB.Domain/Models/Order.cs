using InvestHB.Domain.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestHB.Domain.Models
{
    public class Order
    {
        public Order(
            int userId, 
            string symbol, 
            decimal quantity, 
            OrderSide side, 
            OrderType type, 
            OrderStatus status, 
            decimal price, 
            decimal triggerPrice, 
            DateTime? expiresAt, 
            DateTime createdAt)
        {
            UserId = userId;
            Symbol = symbol;
            Quantity = quantity;
            Side = side;
            Type = type;
            Status = status;
            Price = price;
            TriggerPrice = triggerPrice;
            ExpiresAt = expiresAt;
            CreatedAt = createdAt;
        }

        public Guid Id { get; set; }
        public int UserId { get; set; }
        public string Symbol { get; set; }
        public decimal Quantity { get; set; }
        public OrderSide Side { get; set; }
        public OrderType Type { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Price { get; set; }
        public decimal TriggerPrice { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; }

        public string ToCSV(string delimiter)
        {
            var csv = $"{this.Id}{delimiter}{this.Symbol}{delimiter}{this.Quantity}{delimiter}{Side}{delimiter}{Type}{delimiter}{Price}";

            return csv;
        }
    }

    public enum OrderStatus
    {
        Open = 0,
        Executed,
        PartialExecuted,
        Cancelled
    }

    public enum OrderSide
    {
        Buy = 0,
        Sell
    }

    public enum OrderType
    {
        Limit = 0,
        Pending,
        Market,
        Stop
    }
}
