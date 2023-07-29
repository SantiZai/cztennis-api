using api.Entities;
using api.Services;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersControllers : ControllerBase
    {
        readonly UsersServices _services;

        public UsersControllers(UsersServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IEnumerable<User> GetAll() => _services.GetAll();

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            try
            {
                User user = _services.GetById(id)!;
                return user is not null ? user : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            try
            {
                _services.Create(user);
                return Ok("User created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            try
            {
                _services.Login(user);
                return Ok("Login successful");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public IActionResult Update(int id, User user)
        {
            try
            {
                var existingUser = _services.GetById(id);
                if (existingUser is not null)
                {
                    existingUser.FullName = user.FullName ?? existingUser.FullName;
                    existingUser.Password = user.Password ?? existingUser.Password;
                    existingUser.IsAdmin = user.IsAdmin | existingUser.IsAdmin;
                    existingUser.Orders = user.Orders ?? existingUser.Orders;
                    _services.Update(id, existingUser);
                    return Ok(existingUser);
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
