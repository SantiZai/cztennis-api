using api.Context;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Services
{
    public class StrungServices
    {
        private readonly CZContext _context;

        public StrungServices(CZContext context)
        {
            _context = context;
        }

        public IEnumerable<Strung> GetAll()
        {
            return _context.Strungs.ToList();
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
                Price = strung.Price,
                Size = strung.Size,
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
            existingStrung.Price = strung.Price;
            existingStrung.Size = strung.Size;
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
