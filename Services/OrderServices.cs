using api.Context;
using api.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class OrderServices
    {
        private readonly CZContext _context;

        public OrderServices(CZContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public Order? GetById(int id)
        {
            return _context.Orders
                .AsNoTracking()
                .FirstOrDefault(o => o.Id == id);
        }

        public Order Create(Order order)
        {
            if (_context.Users.Find(order.User_Id) is not null && _context.Strungs.Find(order.Strung_Id) is not null)
            {
                Order newOrder = new()
                {
                    Id = order.Id,
                    User_Id = order.User_Id,
                    Strung_Id = order.Strung_Id,
                };
                _context.Orders.Add(newOrder);
                _context.SaveChanges();
                return newOrder;
            }
            else
            {
                throw new InvalidOperationException("User or Strung not found.");
            }
        }

        public void Update(int id, Order order)
        {
            if (_context.Users.Find(order.User_Id) is not null && _context.Strungs.Find(order.Strung_Id) is not null)
            {
                Order existingOrder = _context.Orders.SingleOrDefault(o => o.Id == id) ?? throw new KeyNotFoundException("Order not found");
                existingOrder.User_Id = order.User_Id;
                existingOrder.Strung_Id = order.Strung_Id;
                _context.Orders.Update(existingOrder);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("User or Strung not found.");
            }
        }

        public void Delete(int id)
        {
            Order existingOrder = _context.Orders.Find(id) ?? throw new KeyNotFoundException("Order not found");
            _context.Orders.Remove(existingOrder);
            _context.SaveChanges();
        }
    }
}
