using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class APIController : ControllerBase
    {
        public DbContext Context { get; set; }
        //public ILogger Logger { get; set; }
    }
}