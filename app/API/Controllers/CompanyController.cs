using System;
using DAL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [Produces("application/json")]
    public class CompanyController : APIController
    {
        private readonly CompanyDataAccess dataAccess;
        public CompanyController (BepwayContext context, IMapper mapper) {
            Context = context;
            Mapper = mapper;
            dataAccess = new CompanyDataAccess(Context);
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<DTO.Company>), 200)]
        public async Task<IActionResult> Get (int? pageIndex = 0, int? pageSize = 15, String companyName = null)
        {
            IEnumerable<Model.Company> entities = await dataAccess.GetAllAsync(pageIndex.Value, pageSize.Value, companyName);
            return Ok(entities.Select(Mapper.Map<DTO.Company>));
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(DTO.Company), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById (int id) 
        {
            Model.Company entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();

            return Ok(Mapper.Map<DTO.Company>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN+","+Model.Constants.Roles.GESTIONNARY)]
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Post ([FromBody] DTO.SigninModel company)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
         
            Model.Company entity = Mapper.Map<Model.Company>(company);
            entity = await dataAccess.AddAsync(entity);
            return Created($"api/Shops/{entity.Id}", Mapper.Map<DTO.Company>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN+","+Model.Constants.Roles.GESTIONNARY)]
        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(DTO.Company), 202)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Put (int id, [FromBody] DTO.Company company)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Model.Company entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();

            entity = await dataAccess.EditAsync(Mapper.Map(company,entity));
            return Accepted(Mapper.Map<DTO.Company>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN+","+Model.Constants.Roles.GESTIONNARY)]
        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(DTO.Company), 202)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Delete (int id)
        {
            Model.Company entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();
            
            await dataAccess.DeleteAsync(entity);
            return Accepted(Mapper.Map<DTO.Company>(entity));
        }
    }
}