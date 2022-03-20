using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrackId.Application.Queries;
using TrackId.Business.Dto;
using TrackId.Business.Interfaces;
using TrackId.Common.Constants;
using TrackId.Contracts.Models.User;
using TrackId.Data.Entities;

namespace TrackId.Application.Commands.Auth.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginCommandResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtTokenHelper _jwtTokenHelper;

        public LoginCommandHandler(ILogger<LoginCommandHandler> logger,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IJwtTokenHelper jwtTokenHelper,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _jwtTokenHelper = jwtTokenHelper;
        }

        public async Task<LoginCommandResult> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (request == null ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                return new LoginCommandResult(RequestErrorType.InvalidArgument, ErrorConstants.Auth.UnableToLogin);
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user is null)
            {
                return new LoginCommandResult(RequestErrorType.NotFound, ErrorConstants.Auth.UserDoesNotExist);
            }

            var authResult = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: false);
            if (authResult is null || authResult.IsNotAllowed || authResult.IsLockedOut || !authResult.Succeeded)
            {
                return new LoginCommandResult(RequestErrorType.Unauthorized, ErrorConstants.Auth.UnableToLogin);
            }

            var userDto = _mapper.Map<UserDto>(user);
            if (userDto is null)
            {
                _logger.LogWarning($"Mapping of userId '{user.Id}' failed to map.");
                return new LoginCommandResult(RequestErrorType.InvalidArgument, ErrorConstants.Auth.UnableToLogin);
            }

            var jwtToken = await _jwtTokenHelper.CreateToken(userDto);
            if (string.IsNullOrEmpty(jwtToken))
            {
                return new LoginCommandResult(RequestErrorType.Unauthorized, ErrorConstants.Auth.UnableToLogin);
            }

            var response = _mapper.Map<LoginResponse>(userDto);
            response.Token = jwtToken;

            return new LoginCommandResult(response);
        }
    }
}
