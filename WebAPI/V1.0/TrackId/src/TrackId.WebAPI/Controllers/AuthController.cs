using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto;
using TrackId.Business.Interfaces;
using TrackId.Common.Constants;
using TrackId.Contracts.Models.User;
using TrackId.Data.Entities;

namespace TrackId.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public AuthController(IMapper mapper, ILogger<AuthController> logger,
            IJwtTokenHelper jwtTokenHelper, SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
            : base(mapper)
        {
            _logger = logger;
            _jwtTokenHelper = jwtTokenHelper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] AuthenticationRequest request)
        {
            try
            {
                if (request == null || string.IsNullOrWhiteSpace(request.Email)
                                    || string.IsNullOrWhiteSpace(request.Password))
                {
                    return BadRequest();
                }

                var user = await _userManager.FindByEmailAsync(request.Email);
                if (user is null)
                {
                    return NotFound();
                }

                var authResult = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);
                if (authResult is null || authResult.IsNotAllowed || authResult.IsLockedOut || !authResult.Succeeded)
                {
                    return Unauthorized();
                }

                var userDto = Mapper.Map<UserDto>(user);
                if (userDto is null)
                {
                    _logger.LogWarning($"Mapping of userId '{user.Id}' failed to map.");
                    return BadRequest();
                }

                var response = Mapper.Map<AuthenticationResponse>(userDto);
                response.Token = await _jwtTokenHelper.CreateToken(userDto);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, $"Unexpected error occured.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest request)
        {
            try
            {
                if (!request.Password.Equals(request.ConfirmPassword, StringComparison.InvariantCulture))
                {
                    ModelState.AddModelError("passwordMatch", ErrorConstants.Auth_PasswordsDoNotMatch);
                    return BadRequest(ModelState);
                }

                var user = Mapper.Map<ApplicationUser>(request);
                if (user is null)
                {
                    _logger.LogWarning("Could not map user from request to DTO.");
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }

                if (await _userManager.FindByEmailAsync(request.Email) is not null)
                {
                    ModelState.AddModelError("userExists", ErrorConstants.Auth_UserAlreadyExists);
                    return BadRequest(ModelState);
                }

                var registrationResult = await _userManager.CreateAsync(user, request.Password);
                if (!registrationResult.Succeeded)
                {
                    foreach (var error in registrationResult.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, RoleConstants.User);

                return Ok(registrationResult);
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex, "Unexpected error occured.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("forgotpassword")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> ForgotPassword([FromBody][EmailAddress] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                // return new ApiResponse(StatusCodes.Status400BadRequest);
            }

            try
            {
                //await _authenticationService.RequestNewPassword(email, cancellationToken);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Something went wrong.");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
