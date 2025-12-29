using Microsoft.AspNetCore.Mvc;
using Mypcot.Models.Dto;
using Mypcot.Services;

namespace Mypcot.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    [HttpPost("Login")]
    public async Task<IActionResult> Login(LoginDto request)
    {
        var result = await _authService.Login(request);
        if (!result.Item1)
            return BadRequest(result.Item2);

        return Ok(result.Item2);
    }
}
