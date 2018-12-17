using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using API.Infrastructure;
using DAL;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/jwt")]
    public class JwtController : APIController
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, BepwayContext context) 
        {
            Context = context;
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login ([FromBody] DTO.LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            string hashed = API.Services.HashPassword.Hash(loginModel.Password);
            Model.User userFound = await (new UserDataAccess(Context)).FindByLoginAndPasswordAsync(loginModel.Login, hashed);
            if (userFound == null) return Unauthorized();
            if (!userFound.IsEnabled) return BadRequest("Account is disabled.");

            JwtSecurityToken token = await CreateToken(userFound);

            return Ok(
                new {
                    access_token = new JwtSecurityTokenHandler().WriteToken(token),
                    expires_in = (int)_jwtOptions.ValidFor.TotalSeconds 
                }
            );
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