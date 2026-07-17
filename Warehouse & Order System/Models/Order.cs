using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    public enum Status
    {
        PENDING,
        SUCCESS,
        FAILED
    }
    public class Order
    {
        public Status Status { get; set; }
        public Customer Customer { get; set; }
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderProduct> orderProduct { get; set; }
    }
}
