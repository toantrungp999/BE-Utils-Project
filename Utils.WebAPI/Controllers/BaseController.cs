﻿using Microsoft.AspNetCore.Mvc;
using Utils.WebAPI.Extensions;

namespace Utils.WebAPI.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid? UserId => HttpContext.GetUserId();
        protected string AuthorizationToken => HttpContext.GetAuthorizationToken();
    }
}
