using Microsoft.AspNetCore.Mvc;

namespace BlogMVC.Api.Controllers;

[ApiController]
[Produces("application/json")]
[ApiConventionType(typeof(DefaultApiConventions))]
public class BlogController : ControllerBase
{
    
}
