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
        private IMapper mapper;
        public BepwayContext Context
        {
            get => context;
            set => context = value ??
                throw new ArgumentNullException(nameof(value));
        }
        public IMapper Mapper
        {
            get => mapper;
            set => mapper = value ??
                throw new ArgumentNullException(nameof(value));
        }

        public ILogger Logger { get; set; }
    }
}