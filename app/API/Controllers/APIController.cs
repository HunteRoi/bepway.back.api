using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class APIController : ControllerBase
    {
        private DbContext context;
        public DbContext Context { 
            get => context;
            set => context = value ?? throw new ArgumentNullException(nameof(value));
        }
        //public ILogger Logger { get; set; }
    }
}