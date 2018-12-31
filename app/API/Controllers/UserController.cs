using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers {
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route ("api/[Controller]")]
    [Produces ("application/json")]
    public class UserController : APIController {
        private readonly UserDataAccess dataAccess;
        public UserController (BepwayContext context, IMapper mapper) {
            Context = context;
            Mapper = mapper;
            dataAccess = new UserDataAccess (Context);
        }

        [Authorize (Roles = Model.Constants.Roles.ADMIN + "," + Model.Constants.Roles.GESTIONNARY)]
        [HttpGet]
        [SwaggerOperation (
            Summary = "Requests a page of users",
            Description = "Returns a certain number of users"
        )]
        [SwaggerResponse (200, "Returns an array of users", typeof (IEnumerable<DTO.User>))]
        public async Task<IActionResult> Get (int? pageIndex = 0, int? pageSize = 5, String userName = null) {
            IEnumerable<Model.User> entities = await dataAccess.GetAllAsync (pageIndex.Value, pageSize.Value, userName);
            return Ok (entities.Select (Mapper.Map<DTO.User>));
        }

        [Authorize (Roles = Model.Constants.Roles.ADMIN + "," + Model.Constants.Roles.GESTIONNARY)]
        [HttpGet ("{id:int}")]
        [SwaggerOperation (
            Summary = "Requests a user based on their ID",
            Description = "Returns the user data"
        )]
        [SwaggerResponse (200, "Returns a user data", typeof (DTO.User))]
        [SwaggerResponse (404, "If the user does not exist")]
        public async Task<IActionResult> GetById (int id) {
            Model.User entity = await dataAccess.FindByIdAsync (id);
            if (entity == null) return NotFound ();

            return Ok (Mapper.Map<DTO.User> (entity));
        }

        [Authorize (Roles = Model.Constants.Roles.ADMIN + "," + Model.Constants.Roles.GESTIONNARY)]
        [HttpGet ("{login}")]
        [SwaggerOperation (
            Summary = "Requests a user based on their login",
            Description = "Returns the user data"
        )]
        [SwaggerResponse (200, "Returns a user data", typeof (DTO.User))]
        [SwaggerResponse (404, "If the user does not exist")]
        public async Task<IActionResult> GetByLogin (string login) {
            Model.User entity = await dataAccess.FindByLoginAsync (login);
            if (entity == null) return NotFound ();

            return Ok (Mapper.Map<DTO.User> (entity));
        }

        [Authorize (Roles = Model.Constants.Roles.ADMIN)]
        [HttpPost]
        [SwaggerOperation (
            Summary = "Creates a user",
            Description = "Returns the created user data",
            Consumes = new string[] { "application/json " }
        )]
        [SwaggerResponse (201, "Returns the created user data and its URI")]
        [SwaggerResponse (400, "If the body does not validate the requirements")]
        public async Task<IActionResult> Post ([FromBody] DTO.SignupModel user) {
            if (!ModelState.IsValid) return BadRequest (ModelState);

            Model.User entity = Mapper.Map<Model.User> (user);
            (string hashed, string salt) = await API.Services.HashPassword.HashAsync (user.Password);
            entity.Password = $"{hashed}.{salt}";

            entity = await dataAccess.AddAsync (entity);
            return Created ($"api/User/{entity.Id}", Mapper.Map<DTO.User> (entity));
        }

        [Authorize (Roles = Model.Constants.Roles.ADMIN)]
        [HttpPut ("{id:int}")]
        [SwaggerOperation (
            Summary = "Edits a user based on their ID",
            Description = "Returns the edited user data",
            Consumes = new string[] { "application/json " }
        )]
        [SwaggerResponse (202, "Returns the edited user data", typeof (DTO.User))]
        [SwaggerResponse (400, "If the body does not validate the requirements")]
        [SwaggerResponse (404, "If the user does not exist")]
        public async Task<IActionResult> Put (int id, [FromBody] DTO.User user) {
            if (!ModelState.IsValid) return BadRequest (ModelState);

            Model.User entity = await dataAccess.FindByIdAsync (id);
            if (entity == null) return NotFound ();

            entity = await dataAccess.EditAsync (Mapper.Map (user, entity));
            return Accepted (Mapper.Map<DTO.User> (entity));
        }

        [Authorize (Roles = Model.Constants.Roles.ADMIN)]
        [HttpDelete ("{id:int}")]
        [SwaggerOperation (
            Summary = "Deletes a user based on their ID",
            Description = "Returns the deleted user data"
        )]
        [SwaggerResponse (202, "Returns the deleted user data", typeof (DTO.User))]
        [SwaggerResponse (404, "If the user does not exist")]
        public async Task<IActionResult> Delete (int id) {
            Model.User entity = await dataAccess.FindByIdAsync (id);
            if (entity == null) return NotFound ();

            await dataAccess.DeleteAsync (entity);
            return Accepted (Mapper.Map<DTO.User> (entity));
        }
    }
}