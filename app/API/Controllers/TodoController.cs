using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers {
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route ("api/[Controller]")]
    [Produces ("application/json")]
    [Consumes ("application/json")]
    public class TodoController : APIController {
        private readonly UserDataAccess dataAccess;
        public TodoController (BepwayContext context) {
            Context = context;
            dataAccess = new UserDataAccess (Context);
        }

        [Authorize (Roles = Model.Constants.Roles.ADMIN + "," + Model.Constants.Roles.GESTIONNARY)]
        [HttpPut ("{id:int}")]
        [SwaggerOperation (
            Summary = "Change a user's todo list based on their ID",
            Description = "Returns the edited user data"
        )]
        [SwaggerResponse (202, "Returns the edited user object", typeof (DTO.User))]
        [SwaggerResponse (400, "If the body does not validate the requirements", typeof (DTO.BusinessError))]
        [SwaggerResponse (404, "If the resource isn't found or the user account is disabled")]
        public async Task<IActionResult> Put (int id, [FromBody] string todoList) {
            if (todoList.Length > 1000) return BadRequest (new DTO.BusinessError { Message = "Todo list too long" });

            Model.User entity = await dataAccess.FindByIdAsync (id);
            if (entity == null || !entity.IsEnabled) return NotFound ();
            entity.TodoList = todoList;

            entity = await dataAccess.EditAsync (entity);
            return Accepted (Mapper.Map<DTO.User> (entity));
        }
    }
}