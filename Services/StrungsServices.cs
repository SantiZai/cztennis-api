using api.Context;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class StrungsServices
    {
        private readonly CZContext _context;

        public StrungsServices(CZContext context)
        {
            _context = context;
        }

        public IEnumerable<Strung> GetAll()
        {
            return _context.Strungs.ToList();
        }

        public IEnumerable<Strung> GetAllByIds(List<Order> orders)
        {
            List<int?> ids = orders.Select(order => order.Strung_Id).ToList();
            return ids.Select(id => _context.Strungs.Find(id))!;
        }

        public Strung? GetById(int id)
        {
            return _context.Strungs
                .AsNoTracking()
                .FirstOrDefault(s => s.Id == id);
        }

        public Strung Create(Strung strung)
        {
            Strung newStrung = new()
            {
                Name = strung.Name,
                Brand = strung.Brand,
                Image = strung.Image,
                Price = strung.Price,
                Size = strung.Size,
                Stock = strung.Stock,
            };
            _context.Strungs.Add(newStrung);
            _context.SaveChanges();
            return newStrung;
        }

        public void Update(int id, Strung strung)
        {
            Strung existingStrung = _context.Strungs.SingleOrDefault(s => s.Id == id) ?? throw new KeyNotFoundException("Strung not found");
            existingStrung.Name = strung.Name;
            existingStrung.Brand = strung.Brand;
            existingStrung.Image = strung.Image;
            existingStrung.Price = strung.Price;
            existingStrung.Size = strung.Size;
            existingStrung.Stock = strung.Stock;
            _context.Strungs.Update(existingStrung);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            Strung existingStrung = _context.Strungs.Find(id) ?? throw new KeyNotFoundException("Strung not found");
            _context.Strungs.Remove(existingStrung);
            _context.SaveChanges();
        }
    }
}
