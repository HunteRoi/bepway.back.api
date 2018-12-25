using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Linq;
using DAL;
using System.Collections.Generic;

namespace API.Controllers
{
    public class PasswordController : APIController
    {
        private readonly UserDataAccess dataAccess;
        public PasswordController(BepwayContext context)
        {
            Context = context;
            dataAccess = new UserDataAccess(Context);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put (int id, [FromBody] DTO.NewLoginModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Model.User userFound = await dataAccess.FindByLoginAsync(model.Login);
            Model.User entity = await dataAccess.FindByIdAsync(id);
            if ((userFound == null || entity == null) && userFound == entity) return NotFound();
            
            List<string> data = userFound.Password.Split('.').ToList();
            (string hashedCheck, string salt) = (data.ElementAt(0), data.ElementAt(1));
            string hashedToVerify = (await API.Services.HashPassword.HashAsync(model.Password, salt)).hashed;
            if (!hashedToVerify.Equals(hashedCheck)) 
            {
                return BadRequest(new DTO.BusinessError { Message = "The login or password is wrong" });
            }

            (string newHashed, string newSalt) = await API.Services.HashPassword.HashAsync(model.NewPassword);
            userFound.Password = $"{newHashed}.{newSalt}";

            userFound = await dataAccess.EditAsync(userFound);
            return Accepted(Mapper.Map<DTO.User>(userFound));
        }
    }
}