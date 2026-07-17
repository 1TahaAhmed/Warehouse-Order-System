using System;
using System.Collections.Generic;
using System.Text;

namespace Warehouse___Order_System.Models
{
    public class OrderEvent
    {
        public event Func<Order,Task>? OnOrderEvent;
        
        public async Task PlaceOrderAsync(Order order, ApplicationDbContext db)
        {
            db.Orders.AddAsync(order);
            await db.SaveChangesAsync();
            if (order != null)
            {
                await OnOrderEvent.Invoke(order);
            }
        }
    }
}