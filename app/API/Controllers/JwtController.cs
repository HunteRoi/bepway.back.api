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

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/jwt")]
    public class JwtController : APIController
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions) 
        {
            _jwtOptions = jwtOptions.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Login ([FromBody] DTO.LoginModel loginModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var repository = new DAL.AuthentificationRepository();
            Model.User userFound = repository.GetUsers().FirstOrDefault(user => 
                user.Login == loginModel.Login && user.Password == loginModel.Password
            );
            if (userFound == null) return Unauthorized();
            
            var claims = new [] {
                new Claim(JwtRegisteredClaimNames.Sub, userFound.Login),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, 
                    ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(),
                    ClaimValueTypes.Integer64
                ),
            };
            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials
            );

            return Ok(new {
                access_token = new JwtSecurityTokenHandler().WriteToken(token),
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds 
            });
        }

        private static long ToUnixEpochDate (DateTime date) 
            => (long) Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970,1,1,0,0,0, TimeSpan.Zero)).TotalSeconds);
    }
}