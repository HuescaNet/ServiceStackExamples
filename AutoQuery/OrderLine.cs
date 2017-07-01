using System;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace AutoQuery
{
    [Alias("OrderLines")]
    [Schema("Sales")]
    public class OrderLine : IHasId<int>
    {
        [Alias("OrderLineID")]
        [Required]
        public int Id { get; set; }
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int StockItemID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int PackageTypeID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        [Required]
        public decimal TaxRate { get; set; }
        [Required]
        public int PickedQuantity { get; set; }
        public DateTime? PickingCompletedWhen { get; set; }
        [Required]
        public int LastEditedBy { get; set; }
        [Required]
        public DateTime LastEditedWhen { get; set; }
    }
}