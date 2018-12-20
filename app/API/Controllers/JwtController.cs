using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using API.Infrastructure;
using DAL;


namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/jwt")]
    [Produces("application/json")]
    [Consumes("application/json")]
    //[SwaggerTag()]
    public class JwtController : APIController
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, BepwayContext context) 
        {
            Context = context;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Requests a token",
            Description = "Returns an access token and its expiration time in seconds"
        )]
        [SwaggerResponse(200, "Returns a token object", typeof(DTO.Token))]
        [SwaggerResponse(400, "If the body does not validate the requirements")]
        [SwaggerResponse(401, "If the user account is disabled or the password does not match")]
        [SwaggerResponse(404, "If the user is not found")]
        public async Task<IActionResult> Login ([FromBody] DTO.LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            Model.User userFound = await (new UserDataAccess(Context)).FindByLoginAsync(loginModel.Login);          
            if (userFound == null) return NotFound();

            List<string> data = userFound.Password.Split('.').ToList();
            string hashedCheck = data.ElementAt(0);
            string salt = data.ElementAt(1);
            string hashedToVerify = (await API.Services.HashPassword.HashAsync(loginModel.Password, salt)).hashed;
            if (!isEnabledAndPasswordsMatch(userFound, hashedToVerify, hashedCheck)) return Unauthorized();

            JwtSecurityToken token = await CreateToken(userFound);
            return Ok(
                new DTO.Token {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds 
                }
            );
        }

        private bool isEnabledAndPasswordsMatch(Model.User userFound, string hashedToVerify, string hashedCheck)
        {
            return userFound.IsEnabled && hashedToVerify.Equals(hashedCheck);
        }

        private async Task<JwtSecurityToken> CreateToken(Model.User userFound)
        {
            var claims = new List<Claim>
            {
                new Claim(PrivateClaims.UserId,userFound.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userFound.Login),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, 
                    ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64
                )    
            };
            
            if (!String.IsNullOrEmpty(userFound.Roles)) 
            {
                userFound.Roles.Split(' ').ToList().ForEach(roleName => claims.Add(new Claim(PrivateClaims.Roles, roleName)));
            }

            return new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );
        }

        private static long ToUnixEpochDate (DateTime date) 
            => (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970,1,1,0,0,0, TimeSpan.Zero)).TotalSeconds);
    }
}