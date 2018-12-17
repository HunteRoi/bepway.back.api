using DAL;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    public class UserController : APIController
    {
        private readonly UserDataAccess dataAccess;
        public UserController (BepwayContext context, IMapper mapper) {
            Context = context;
            Mapper = mapper;
            dataAccess = new UserDataAccess(Context);
        }

        [HttpGet]
        public async Task<IActionResult> Get (int? pageIndex = 0, int? pageSize = 5, string userName = null)
        {
            // est-ce correct? pourquoi ne pas le déplacer vers le DAL? ... à voir
            IEnumerable<Model.User> users = (await dataAccess.GetAllAsync(pageIndex, pageSize, userName))
                .Where(user => user.Login == userName || user.Login.Contains(userName))
                .OrderBy(user => user.Id)
                //.TakePage(pageIndex, pageSize)
                /*.ToArrayAsync()*/;
            return Ok(users.Select(Mapper.Map<DTO.User>));
        }

        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetById (int id) 
        // // {
        //     Model.Shop entity = await FindShopById(id);
        //     if (entity == null)
        //         return NotFound();
        //     return Ok(CreateDTOFromEntity(entity));
        // }

        // [HttpGet("{login}")]
        // public async Task<IActionResult> GetByLogin (string login) 
        // {

        // }

        // [HttpPost]
        // [Authorize(Roles = Model.Constants.Roles.ADMIN, Model.Constants.Roles.GESTIONNARY)]
        // public async Task<IActionResult> Post ([FromBody] DTO.User user)
        // {
            // if (!ModelState.IsValid)
            //     return BadRequest(ModelState);
            // Model.Shop entity = CreateEntityFromDTO(dto);
            // _context.Shops.Add(entity);
            // await _context.SaveChangesAsync();
            // return Created($"api/Shops/{entity.Id}", CreateDTOFromEntity(entity));
        // }

        // [HttpPut("{id}")]
        // //[Authorize(Roles = Model.Constants.Roles.ADMIN, Model.Constants.Roles.GESTIONNARY)]
        // public async Task<IActionResult> Put (int id, [FromBody] DTO.User user)
        // {
            // //fixme: comment valider que le client envoie toujours quelque chose de valide?
            // Model.Shop entity = await FindShopById(id);
            // if (entity == null)
            //     return NotFound();
            // //fixme: améliorer cette implémentation
            // entity.Name = dto.Name;
            // entity.OwnerId = dto.OwnerId;
            // //fixme: le premier RowVersion n'a pas d'impact. 
            // // Attardez-vous à comprendre pour quelle raison.
            // // entity.RowVersion = dto.RowVersion;
            // _context.Entry(entity).OriginalValues["RowVersion"] = dto.RowVersion;
            // //pas de gestion des opening periods (voir autre controller).
            // await _context.SaveChangesAsync();
            // return Ok(CreateDTOFromEntity(entity));
        // }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> Delete (int id)
        // {
            // Model.Shop shop = await FindShopById(id);
            // if (shop == null)
            //     // todo: débat: si l'on demande une suppression d'une entité qui n'existe pas
            //     // s'agit-il vraiment d'un cas d'erreur? 
            //     return NotFound();
            // _context.Shops.Remove(shop);
            // await _context.SaveChangesAsync();
            // return Ok();
        // }
    }
}