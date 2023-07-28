using api.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderControllers : ControllerBase
    {
        readonly OrderServices _services;

        public OrderControllers(OrderServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IEnumerable<Order> GetAll() => _services.GetAll();

        [HttpGet("{id}")]
        public ActionResult<Order> GetById(int id)
        {
            try
            {
                Order order = _services.GetById(id);
                return order is not null ? order : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(Order order)
        {
            try
            {
                _services.Create(order);
                return Ok("Order created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id,  Order order)
        {
            try
            {
                var existingOrder = _services.GetById(id);
                if (existingOrder is not null)
                {
                    existingOrder.User_Id = order.User_Id ?? existingOrder.User_Id;
                    existingOrder.Strung_Id = order.Strung_Id ?? existingOrder.Strung_Id;
                    _services.Update(id, existingOrder);
                    return Ok(existingOrder);
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
                    return Ok("Order deleted");
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
