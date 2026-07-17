using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public CustomerProfile profile { get; set; }
    }
}
