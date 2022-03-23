using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TrackId.Application.Commands.Auth.Login;
using TrackId.Application.Commands.Auth.Register;
using TrackId.Application.Queries;
using TrackId.Common.Constants;
using TrackId.Contracts.Models.User;

namespace TrackId.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseApiController
    {
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IMapper mapper,
            ILogger<AuthController> logger,
            IMediator mediator) : base(mapper, mediator)
        {
            _logger = logger;
        }

        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
        {
            try
            {
                if (request == null)
                {
                    return BadRequest();
                }

                var result = await Mediator.Send(new LoginCommand
                {
                    Email = request.Email,
                    Password = request.Password,
                    RememberMe = request.RememberMe
                }, HttpContext.RequestAborted);

                if (result.Errors.Any(err => err.Type.Equals(RequestErrorType.Unauthorized)))
                {
                    return Unauthorized();
                }

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(result.Result);
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
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> Register([FromBody] RegistrationRequest request)
        {
            try
            {
                var result = await Mediator.Send(new RegisterCommand
                {
                    Password = request.Password,
                    ConfirmPassword = request.ConfirmPassword,
                    Email = request.Email,
                    Username = request.Email
                }, HttpContext.RequestAborted);

                if (result == null)
                {
                    return BadRequest(ErrorConstants.GeneralError);
                }

                if (!result.Success)
                {
                    return BadRequest(result.Errors);
                }

                return NoContent();
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
