using api.Context;
using api.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class UsersServices
    {
        private readonly CZContext _context;

        public UsersServices(CZContext context)
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

        public User Login(User user)
        {
            return _context.Users
                .SingleOrDefault(u => u.FullName == user.FullName && u.Password == user.Password) ?? throw new KeyNotFoundException("User not found");
        }

        public void Update(int id, User user)
        {
            User existingUser = _context.Users.SingleOrDefault(u => u.Id == id) ?? throw new KeyNotFoundException("User not found");
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
