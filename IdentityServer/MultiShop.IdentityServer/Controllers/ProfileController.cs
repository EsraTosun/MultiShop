using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace MultiShop.IdentityServer.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/[controller]")]
[ApiController]
public class ProfileController : ControllerBase
{
    [HttpGet]
    public IActionResult GetProfile()
    {
        var username = User.Identity?.Name;
        var name = User.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
        var surname = User.Claims.FirstOrDefault(c => c.Type == "surname")?.Value;

        return Ok(new
        {
            Username = username,
            Name = name,
            Surname = surname
        });
    }
}
