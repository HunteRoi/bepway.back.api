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
using Model;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [Produces("application/json")]
    public class ZoningController : APIController
    {
        private readonly ZoningDataAccess dataAccess;
        public ZoningController(BepwayContext context, IMapper mapper)
        {
            Context = context;
            Mapper = mapper;
            dataAccess = new ZoningDataAccess(Context);
        }

        [AllowAnonymous]
        [HttpGet]
        [SwaggerOperation(
            Summary = "Requests a page of zonings",
            Description = "Returns a certain number of zonings"
        )]
        [SwaggerResponse(200, "Returns an array of zonings", typeof(IEnumerable<DTO.Zoning>))]
        public async Task<IActionResult> Get(int? pageIndex = Constants.Page.Index, int? pageSize = Constants.Page.Size, String zoningName = null)
        {
            IEnumerable<Model.Zoning> entities = await dataAccess.GetAllAsync(pageIndex.Value, pageSize.Value, zoningName);

            Request.HttpContext.Response.Headers.Add("X-TotalCount", dataAccess.GetTotalCount().ToString());
            Request.HttpContext.Response.Headers.Add("X-PageIndex", pageIndex.Value.ToString());
            Request.HttpContext.Response.Headers.Add("X-PageSize", pageSize.Value.ToString());


            return Ok(entities.Select(Mapper.Map<DTO.Zoning>));
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        [SwaggerOperation(
            Summary = "Requests a zoning based on their ID",
            Description = "Returns the zoning data"
        )]
        [SwaggerResponse(200, "Returns a zoning data", typeof(DTO.Zoning))]
        [SwaggerResponse(404, "If the zoning does not exist")]
        public async Task<IActionResult> GetById(int id)
        {
            Model.Zoning entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();

            return Ok(Mapper.Map<DTO.Zoning>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN + "," + Model.Constants.Roles.GESTIONNARY)]
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a zoning",
            Description = "Returns the created zoning data",
            Consumes = new string[] { "application/json " }
        )]
        [SwaggerResponse(201, "Returns the created zoning data and its URI")]
        [SwaggerResponse(400, "If the body does not validate the requirements")]
        public async Task<IActionResult> Post([FromBody] DTO.Zoning zoning)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Model.Zoning entity = Mapper.Map<Model.Zoning>(zoning);
            entity = await dataAccess.AddAsync(entity);
            return Created($"api/Zoning/{entity.Id}", Mapper.Map<DTO.Zoning>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN + "," + Model.Constants.Roles.GESTIONNARY)]
        [HttpPut("{id:int}")]
        [SwaggerOperation(
            Summary = "Edits a zoning based on their ID",
            Description = "Returns the edited zoning data",
            Consumes = new string[] { "application/json " }
        )]
        [SwaggerResponse(202, "Returns the edited zoning data", typeof(DTO.Zoning))]
        [SwaggerResponse(400, "If the body does not validate the requirements")]
        [SwaggerResponse(404, "If the zoning does not exist")]
        public async Task<IActionResult> Put(int id, [FromBody] DTO.Zoning zoning)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Model.Zoning entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();

            entity = await dataAccess.EditAsync(Mapper.Map(zoning, entity));
            return Accepted(Mapper.Map<DTO.Zoning>(entity));
        }

        [Authorize(Roles = Model.Constants.Roles.ADMIN + "," + Model.Constants.Roles.GESTIONNARY)]
        [HttpDelete("{id:int}")]
        [SwaggerOperation(
            Summary = "Deletes a zoning",
            Description = "Returns the deleted zoning data"
        )]
        [SwaggerResponse(202, "Returns the deleted zoning data", typeof(DTO.Zoning))]
        [SwaggerResponse(404, "If the user zoning not exist")]
        public async Task<IActionResult> Delete(int id)
        {
            Model.Zoning entity = await dataAccess.FindByIdAsync(id);
            if (entity == null) return NotFound();

            await dataAccess.DeleteAsync(entity);
            return Accepted(Mapper.Map<DTO.Zoning>(entity));
        }
    }
}