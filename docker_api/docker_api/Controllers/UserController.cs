using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using docker_api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace docker_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly UserContext _context;
        public UserController(UserContext context)
        {
            _context = context;

            if (_context.Users.Count() == 0)
            {
                // Create a new TodoItem if collection is empty,
                // which means you can't delete all TodoItems.
                _context.Users.Add(new User { Name = "Item1" });
                _context.SaveChanges();

            }


        }

        [HttpGet]
        public ActionResult<List<User>> GetAll()
        {
            return _context.Users.ToList();
        }



        [HttpGet("{id}", Name = "GetUser")]
        public ActionResult<User> GetById(long id)
        {
            var item = _context.Users.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }


        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public IActionResult Create(User item)
        {
            if (item.Name == null || item.Username == null || item.Password == null || item.Email == null || item.Age == 0)
            {

                return StatusCode(400);
                //return BadRequest();
                //return NoContent();
                //Responses
            }
            else
            {
                _context.Users.Add(item);
                _context.SaveChanges();

                return CreatedAtRoute("GetUser", new { id = item.Id }, item);
            }

        }


        [HttpPut("{id}")]
        public IActionResult Update(long id, User item)
        {
            var todo = _context.Users.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Name = item.Name;
            todo.Username = item.Username;
            todo.Password = item.Password;
            todo.Age = item.Age;

            _context.Users.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.Users.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Users.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
