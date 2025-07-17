﻿// to unify routing configs

using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
    }
}
