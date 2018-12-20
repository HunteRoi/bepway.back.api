using System;
using AutoMapper;
using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class APIController : ControllerBase
    {
        private BepwayContext context;
        public BepwayContext Context { 
            get => context;
            set => context = value ?? throw new ArgumentNullException(nameof(value));
        }
        public IMapper Mapper { get; set ;}
        public ILogger Logger { get; set; }
    }
}