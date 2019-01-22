using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Model;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [Produces("application/json")]
    public class CompanyController : APIController
    {
        private readonly CompanyDataAccess dataAccess;
        public CompanyController(BepwayContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
            dataAccess = new CompanyDataAccess(Context);
        }

        [Authorize(Roles = Model.Constants.AuthorizationRoles.ALL)]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Requests a page of companies from a certain zoning if provided",
            Description = "Returns a certain number of companies"
        )]
        [SwaggerResponse(200, "Returns an array of companies", typeof(IEnumerable<DTO.Company>))]
        [SwaggerResponse(400, "If the provided research data do not match on one or more companies")]
        public async Task<IActionResult> Get(int? pageIndex = Constants.Page.Index, int? pageSize = Constants.Page.Size, int? zoningId = null, String companyName = null, String address = null, String activityName = null)
        {
            int totalCount = dataAccess.GetTotalCount();
            IEnumerable<Model.Company> entities = await dataAccess.GetAllInfoAsync(pageIndex, pageSize, zoningId, companyName, address, activityName);
            if (entities == null) return NotFound();

            Request.HttpContext.Response.Headers.Add("X-TotalCount", totalCount.ToString());
            Request.HttpContext.Response.Headers.Add("X-PageIndex", pageIndex.Value.ToString());
            Request.HttpContext.Response.Headers.Add("X-PageSize", pageSize.Value.ToString());

            return Ok(entities.Select(Mapper.Map<DTO.Company>));
        }

        [Authorize(Roles = Model.Constants.AuthorizationRoles.ALL)]
        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Requests a company based on their ID",
            Description = "Returns the company data"
        )]
        [SwaggerResponse(200, "Returns a company data", typeof(DTO.Company))]
        [SwaggerResponse(404, "If the company does not exist")]
        public async Task<IActionResult> GetById(int id)
        {
            Model.Company entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();

            return Ok(Mapper.Map<DTO.Company>(entity));
        }

        [Authorize(Roles = Model.Constants.AuthorizationRoles.ALL)]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a company",
            Description = "Returns the created company data",
            Consumes = new string[] { "application/json " }
        )]
        [SwaggerResponse(201, "Returns the created company data and its URI")]
        [SwaggerResponse(400, "If the body does not validate the requirements")]
        public async Task<IActionResult> Post([FromBody] DTO.Company company)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Model.Company entity = Mapper.Map<Model.Company>(company);
            entity = await dataAccess.AddAsync(entity);
            return Created($"api/Company/{entity.Id}", Mapper.Map<DTO.Company>(entity));
        }

        [Authorize(Roles = Model.Constants.AuthorizationRoles.ADMIN_AND_GESTIONNARY)]
        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Edits a company based on their ID",
            Description = "Returns the edited company data",
            Consumes = new string[] { "application/json " }
        )]
        [SwaggerResponse(202, "Returns the edited company data", typeof(DTO.Company))]
        [SwaggerResponse(400, "If the body does not validate the requirements")]
        [SwaggerResponse(404, "If the company does not exist")]
        public async Task<IActionResult> Put(int id, [FromBody] DTO.Company company)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Model.Company entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();

            entity = await dataAccess.EditAsync(Mapper.Map(company, entity));
            return Accepted(Mapper.Map<DTO.Company>(entity));
        }

        [Authorize(Roles = Model.Constants.AuthorizationRoles.ADMIN_AND_GESTIONNARY)]
        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Deletes a company based on their ID",
            Description = "Returns the deleted company data"
        )]
        [SwaggerResponse(202, "Returns the deleted company data", typeof(DTO.Company))]
        [SwaggerResponse(404, "If the user company not exist")]
        public async Task<IActionResult> Delete(int id)
        {
            Model.Company entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();

            await dataAccess.DeleteAsync(entity);
            return Accepted(Mapper.Map<DTO.Company>(entity));
        }
    }
}