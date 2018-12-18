using System.Threading.Tasks;
using DAL;
using Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [Produces("application/json")]
    public class TodoController : APIController
    {
        private readonly UserDataAccess dataAccess;
        public TodoController (BepwayContext context)
        {
            Context = context;
            dataAccess = new UserDataAccess(Context);
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN+","+Model.Constants.Roles.GESTIONNARY)]
        [HttpPut("{id:int}/todo")]
        [ProducesResponseType(202)]
        [ProducesResponseType(typeof(DTO.BusinessError),400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put (int id, [FromBody] string todoList)
        {
            if (todoList.Length > 1000) return BadRequest(new DTO.BusinessError() {
                Message = "Todo list too long"
            });

            Model.User entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            
            entity.TodoList = todoList;
            await Context.SaveChangesAsync();
            return Accepted(Mapper.Map<DTO.User>(entity));
        }
    }
}