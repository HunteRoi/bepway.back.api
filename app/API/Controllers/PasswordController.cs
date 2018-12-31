using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers {
    [Authorize (AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route ("api/[Controller]")]
    [Consumes ("application/json")]
    [Produces ("application/json")]
    public class PasswordController : APIController {
        private readonly UserDataAccess dataAccess;
        public PasswordController (BepwayContext context) {
            Context = context;
            dataAccess = new UserDataAccess (Context);
        }

        [Authorize (Roles = Model.Constants.Roles.ADMIN + "," + Model.Constants.Roles.GESTIONNARY)]
        [HttpPut ("{id:int}")]
        [SwaggerOperation (
            Summary = "Change a user's password based on their ID",
            Description = "Returns the edited user data",
            Tags = new string[] { "User" }
        )]
        [SwaggerResponse (202, "Returns the edited user", typeof (DTO.User))]
        [SwaggerResponse (400, "If the body does not validate the requirements")]
        [SwaggerResponse (404, "If the user login and ID do not match, if the user does not exist or the account is disabled")]
        public async Task<IActionResult> Put (int id, [FromBody] DTO.NewLoginModel model) {
            if (!ModelState.IsValid) return BadRequest (ModelState);

            Model.User userFound = await dataAccess.FindByLoginAsync (model.Login);
            Model.User entity = await dataAccess.FindByIdAsync (id);
            if ((userFound == null || entity == null) && userFound == entity) return NotFound ();

            List<string> data = userFound.Password.Split ('.').ToList ();
            (string hashedCheck, string salt) = (data.ElementAt (0), data.ElementAt (1));
            string hashedToVerify = (await API.Services.HashPassword.HashAsync (model.Password, salt)).hashed;
            if (!hashedToVerify.Equals (hashedCheck)) {
                return BadRequest (new DTO.BusinessError { Message = "The login or password is wrong" });
            }

            (string newHashed, string newSalt) = await API.Services.HashPassword.HashAsync (model.NewPassword);
            userFound.Password = $"{newHashed}.{newSalt}";

            userFound = await dataAccess.EditAsync (userFound);
            return Accepted (Mapper.Map<DTO.User> (userFound));
        }
    }
}