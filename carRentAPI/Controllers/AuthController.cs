using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using carRentAPI.DTOs;
using carRentAPI.Models;
using carRentAPI.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNet.Identity;
using System.Data;
using MongoDbGenericRepository;
using MongoDB.Driver;
using carRentAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace carRentAPI.Controllers
{

    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUsersService _userService;

        public AuthController(IConfiguration configuration, IUsersService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }

        // User Registration API Easydrive
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UsersClass user)
        {
            try
            {
                // Register the user in Easydrive
                var registeredUser = await _userService.RegisterAsyn(user);

                return Ok(new { Message = "Registration successful", User = registeredUser });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Registration failed", Error = ex.Message });
            }
        }

        // User Login API
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UsersClass model)
        {
            try
            {
                var user = await _userService.AuthenticateAsync(model.Username, model.Password);

                if (user == null)
                {
                    return Unauthorized(new { Message = "Invalid username or password" });
                }

                // Create JWT token for authenticated users.
                var token = GenerateJwtToken(user);

                return Ok(new { Message = "Login successful", Token = token, NIC = user.NIC });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = "Login failed", Error = ex.Message });
            }
        }

        // Customer Role API
        [Authorize(Roles = "Customer")]
        [HttpGet("customer-dashboard")]
        public IActionResult AdminDashboard()
        {
            return Ok(new { Message = "Welcome to the Admin Dashboard" });
        }

        // Car Renter Role API
        [Authorize(Roles = "CarRenter")]
        [HttpGet("carRenter-dashboard")]
        public IActionResult TravelAgentDashboard()
        {
            return Ok(new { Message = "Welcome to the Travel Agent Dashboard" });
        }
        // JWT Token Generate Function
        private string GenerateJwtToken(UsersClass user)
        {
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

            if (!string.IsNullOrEmpty(user.NIC))
            {
                claims.Add(new Claim(ClaimTypes.Authentication, user.NIC));
            }

            if (!string.IsNullOrEmpty(user.Role))
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}

