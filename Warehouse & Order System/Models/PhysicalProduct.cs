using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    public class PhysicalProduct : Product
    {
        public int ShippingCost { get; set; }
        public double Weight { get; set; }
    }
}
