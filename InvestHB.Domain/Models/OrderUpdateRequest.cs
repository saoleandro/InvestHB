using InvestHB.Domain.Commands;

namespace InvestHB.Domain.Models
{
   public class OrderUpdateRequest
    {
        public int UserId { get; set; }
        public decimal Quantity { get; set; }
        public OrderSide Side { get; set; }
        public OrderType Type { get; set; }
        public decimal Price { get; set; }
        public decimal TriggerPrice { get; set; }
        public int OrderId { get; set; }
        public string Instrument { get; set; }
    }
}
