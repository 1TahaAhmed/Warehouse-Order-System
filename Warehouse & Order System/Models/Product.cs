using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    abstract public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public DateTime ProductionDate { get; set; }
        public int StockQuantity { get; set; }

        public void Restock(Product product, int quantity) => product.StockQuantity += quantity;
        public void ReduceStock(Product product,int quantity)
        {
            if (quantity > product.StockQuantity)
                throw new InvalidOperationException("The Product is out of stock!");
            StockQuantity -= quantity;
        }
    }
}