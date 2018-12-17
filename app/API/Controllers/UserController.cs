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

        [HttpGet]
        public async Task<IActionResult> Get (int? pageIndex = 0, int? pageSize = 5, string userName = null)
        {
            IEnumerable<Model.User> entities = await dataAccess.GetAllAsync(pageIndex, pageSize, userName);
            return Ok(entities.Select(Mapper.Map<DTO.User>));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById (int id) 
        {
            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            return Ok(Mapper.Map<DTO.User>(entity));
        }

        [HttpGet("{login}")]
        public async Task<IActionResult> GetByLogin (string login) 
        {
            Model.User entity = await dataAccess.FindByLoginAsync(login);
            if (entity == null) return NotFound();
            return Ok(Mapper.Map<DTO.User>(entity));
        }

        [HttpPost]
        [Authorize(Roles = Model.Constants.Roles.ADMIN)]
        public async Task<IActionResult> Post ([FromBody] DTO.User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Model.User entity = Mapper.Map<Model.User>(user);
            await dataAccess.AddAsync(entity);
            return Created($"api/Shops/{entity.Id}", Mapper.Map<DTO.User>(entity));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = Model.Constants.Roles.ADMIN)]
        public async Task<IActionResult> Put (int id, [FromBody] DTO.User user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            entity = Mapper.Map(user,entity);
            await Context.SaveChangesAsync();
            return Accepted(Mapper.Map<DTO.User>(entity));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = Model.Constants.Roles.ADMIN)]
        public async Task<IActionResult> Delete (int id)
        {
            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            await dataAccess.DeleteAsync(entity);
            return Accepted(Mapper.Map<DTO.User>(entity));
        }
    }
}