using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiSpecflowTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserController(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllAsync()
        {
            List<User> users = await _applicationDbContext.Users.ToListAsync();
            return Ok(users);
        }

        [HttpPost("", Name = "users")]
        public async Task<IActionResult> CreateAsync([FromBody] UserCreateModel userCreateModel)
        {
            if (userCreateModel == null) return BadRequest();
            User user = new User(userCreateModel.Name);
            _applicationDbContext.Users.Add(user);
            await _applicationDbContext.SaveChangesAsync();
            return CreatedAtRoute("users", user);
        }
    }
}