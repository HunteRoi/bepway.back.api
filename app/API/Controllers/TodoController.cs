using System.Threading.Tasks;
using DAL;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/User")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class TodoController : APIController
    {
        private readonly UserDataAccess dataAccess;
        public TodoController(BepwayContext context, IMapper mapper)
        {
            Context = context;
            dataAccess = new UserDataAccess(Context);
            Mapper = mapper;
        }

        [Authorize(Roles = Model.Constants.AuthorizationRoles.ADMIN_AND_GESTIONNARY)]
        [HttpPut("{id:int}/todo")]
        [SwaggerOperation(
            Summary = "Change a user's todo list based on their ID",
            Description = "Returns the edited user data",
            Tags = new string[] { "User" }
        )]
        [SwaggerResponse(202, "Returns the edited user object", typeof(DTO.User))]
        [SwaggerResponse(400, "If the body does not validate the requirements", typeof(DTO.Error))]
        [SwaggerResponse(404, "If the resource isn't found or the user account is disabled")]
        public async Task<IActionResult> PutTodo(int id, [FromBody] DTO.TodoList data)
        {
            if (!ModelState.IsValid) return BadRequest(new DTO.Error { Message = "To-do list too long" });

            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null || !entity.IsEnabled) return NotFound();
            entity.TodoList = data.Todo;

            entity = await dataAccess.EditAsync(entity);
            return Accepted(Mapper.Map<DTO.User>(entity));
        }
    }
}