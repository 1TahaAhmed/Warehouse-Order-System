using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    public class OrderProduct
    {
        public int ProductId { get; set; }
        public Product product { get; set; } = null!;


        public Order Order { get; set; } = null!;
        public int OrderId { get; set; }


        public double Quantity { get; set; }
        public int UnitPrice { get; set; }
    }
}
