using api.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("strungs")]
    public class StrungsControllers : ControllerBase
    {
        readonly StrungsServices _services;

        public StrungsControllers(StrungsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IEnumerable<Strung> GetAll() => _services.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Strung> GetById(int id)
        {
            try
            {
                Strung strung = _services.GetById(id)!;
                return strung is not null ? strung : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Strung strung)
        {
            try
            {
                _services.Create(strung);
                return Ok("Strung created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, Strung strung)
        {
            try
            {
                var existingStrung = _services.GetById(id);
                if (existingStrung is not null)
                {
                    existingStrung.Name = strung.Name ?? existingStrung.Name;
                    existingStrung.Brand = strung.Brand ?? existingStrung.Brand;
                    existingStrung.Image = strung.Image ?? existingStrung.Image;
                    existingStrung.Price = strung.Price ?? existingStrung.Price;
                    existingStrung.Size = strung.Size ?? existingStrung.Size;
                    _services.Update(id, existingStrung);
                    return Ok(existingStrung);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if(_services.GetById(id) is not null)
                {
                    _services.Delete(id);
                    return Ok("User deleted");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
