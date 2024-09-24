using Microsoft.AspNetCore.Mvc;
using BCrypt.Net;
using experience_survey_backend.Data;
using experience_survey_backend.Models;
using experience_survey_backend.Dtos;
using Microsoft.EntityFrameworkCore;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly SurveyContext _context;

    public AuthController(SurveyContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto request)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (existingUser != null)
        {
            return BadRequest(new { message = "Email is already registered." });
        }

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password 
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto request)
    {
        var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);

        if (user == null || user.Password != request.Password)
        {
            return Unauthorized(new { message = "Invalid Email or password" });
        }

        return Ok(new { message = "Login successful" });
    }

    [HttpPost("getUserId")]
    public async Task<IActionResult> GetUserId(UserDto request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

        return Ok(new { userId = user.Id });
    }
}