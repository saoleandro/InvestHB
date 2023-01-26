using InvestHB.Domain.Commands;
using System.ComponentModel.DataAnnotations;

namespace InvestHB.Domain.Models
{
    public class OrderRequest
    {
        [Required(ErrorMessage = "O usuário é obrigatório")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória")]
        public decimal Quantity { get; set; }
        public OrderSide Side { get; set; }
        public OrderType Type { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório")]
        public decimal Price { get; set; }
        public decimal TriggerPrice { get; set; }
        public string Symbol { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        public DateTime? ExpiresAt { get; set; }
    }
}
