using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    public class CustomerProfile 
    {
        public int Id { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public int CustomerId { get; set; }
        public Customer customer { get; set; }
    }
}
