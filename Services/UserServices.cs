using api.Context;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class UserServices
    {
        private readonly CZContext _context;

        public UserServices(CZContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User? GetById(int id)
        {
            return _context.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Id == id);
        }

        public User Create(User user)
        {
            User newUser = new()
            {
                FullName = user.FullName,
                Password = user.Password,
                IsAdmin = user.IsAdmin,
                Orders = user.Orders
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
            return newUser;
        }

        public void Update(int id, User user)
        {
            User existingUser = _context.Users.SingleOrDefault(u => u.Id == id) ?? throw new KeyNotFoundException("Order not found");
            existingUser.FullName = user.FullName;
            existingUser.Password = user.Password;
            existingUser.IsAdmin = user.IsAdmin;
            existingUser.Orders = user.Orders;
            _context.Users.Update(existingUser);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            User existingUser = _context.Users.Find(id) ?? throw new KeyNotFoundException("User not found");
            _context.Users.Remove(existingUser);
            _context.SaveChanges();
        }
    }
}
