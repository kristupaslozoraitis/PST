using KAMANDAX.JWT;
using KAMANDAX.Models;
using KAMANDAX.Models.Responses;
using KAMANDAX.Services;
using KAMANDAX.Services.Authenticators;
using KAMANDAX.Services.RefreshTokenRepositories;
using KAMANDAX.Services.TokenValidators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace KAMANDAX.Controllers
{
    public class AuthController : Controller
    {
        private readonly RefreshTokenValidator _refreshTokenValidator;
        private readonly Authenticator _authenticator;
        private readonly UserService _users;
        private readonly RefreshTokenService _refreshTokenService;
        private AuthenticatedUserResponse _authenticatedUserResponse;
        private IProductService _productService;

        public AuthController(RefreshTokenValidator refreshTokenValidator,
            Authenticator authenticator,
            UserService users,
            RefreshTokenService refreshTokenService, AuthenticatedUserResponse authenticatedUserResponse, IProductService productService)
        {
            _refreshTokenValidator = refreshTokenValidator;
            _authenticator = authenticator;
            _users = users;
            _refreshTokenService = refreshTokenService;
            _authenticatedUserResponse = authenticatedUserResponse;
            _productService = productService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User existingUserByEmail = await _users.GetUserByEmail(user.Email);
            if(existingUserByEmail != null)
            {
                return Conflict();
            }

            User registeredUser = new User()
            {
                Email = user.Email,
                FullName = user.FullName,
                Password = user.Password,
                Address = null,
                Country = null,
                City = null,
                PostalCode = null
            };

             await _users.Create(registeredUser);

            AuthenticatedUserResponse response = await _authenticator.Authenticate(registeredUser);

            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            User existingUser = await _users.GetUserByEmail(user.EmailAdress);
            if (existingUser == null)
            {
                return Unauthorized();
            }

            bool isCorrectPassword = existingUser.Password == user.Password;
            if (!isCorrectPassword)
            {
                return Unauthorized();
            }

            AuthenticatedUserResponse response = await _authenticator.Authenticate(existingUser);

            return Ok(response);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh (RefreshRequest refreshRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool isValidRefreshToken = _refreshTokenValidator.Validate(refreshRequest.RefreshToken);
            if (!isValidRefreshToken)
            {
                return BadRequest();
            }

            RefreshToken rToken = await _refreshTokenService.GetByToken(refreshRequest.RefreshToken);
            if(rToken == null)
            {
                return NotFound();
            }

            await _refreshTokenService.Delete(rToken.Id);

            User user = await _users.GetUserById(rToken.UserId);
            if(user == null)
            {
                return NotFound();
            }

            _authenticatedUserResponse = await _authenticator.Authenticate(user);

            return Ok(_authenticatedUserResponse);
        }
        [HttpDelete("logout")]
        public async Task<IActionResult> Logout(string token)
        {
            RefreshToken rToken = await _refreshTokenService.GetByToken(token);
            User rawUser = await _users.GetUserById(rToken.UserId);
            string rawUserId = rawUser.Id.ToString();

            if(!Guid.TryParse(rawUserId, out Guid userId))
            {
                return Unauthorized();
            }

            await _refreshTokenService.DeleteAll(userId);

            return NoContent();
        }
    }
}
