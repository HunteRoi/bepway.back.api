using DAL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class UserController : APIController
    {
        private readonly UserDataAccess dataAccess;
        public UserController (BepwayContext context, IMapper mapper) {
            Context = context;
            Mapper = mapper;
            dataAccess = new UserDataAccess(Context);
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN+","+Model.Constants.Roles.GESTIONNARY)]
        [HttpGet]
        public async Task<IActionResult> Get (int? pageIndex = 0, int? pageSize = 5, string userName = null)
        {
            IEnumerable<Model.User> entities = await dataAccess.GetAllAsync(pageIndex, pageSize, userName);
            return Ok(entities.Select(Mapper.Map<DTO.User>));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN+","+Model.Constants.Roles.GESTIONNARY)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id) 
        {
            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(Mapper.Map<DTO.User>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN+","+Model.Constants.Roles.GESTIONNARY)]
        [HttpGet("{login}")]
        public async Task<IActionResult> GetByLogin (string login) 
        {
            Model.User entity = await dataAccess.FindByLoginAsync(login);
            if (entity == null) return NotFound();
            return Ok(Mapper.Map<DTO.User>(entity));
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Post ([FromBody] DTO.SigninModel user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Model.User entity = Mapper.Map<Model.User>(user);
            
            (string hashed, string salt) = await API.Services.HashPassword.HashAsync(user.Password);
            entity.Password = $"{hashed}.{salt}";

            entity = await dataAccess.AddAsync(entity);
            return Created($"api/Shops/{entity.Id}", Mapper.Map<DTO.User>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, [FromBody] DTO.User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            entity = Mapper.Map(user,entity);
            await Context.SaveChangesAsync();
            return Accepted(Mapper.Map<DTO.User>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.GESTIONNARY)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put (int id, [FromBody] string todoList)
        {
            if (string.IsNullOrEmpty(todoList)) return BadRequest();
            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            entity.TodoList = todoList;
            await Context.SaveChangesAsync();
            return Accepted(Mapper.Map<DTO.User>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete (int id)
        {
            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            await dataAccess.DeleteAsync(entity);
            return Accepted(Mapper.Map<DTO.User>(entity));
        }
    }
}