﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrackId.Application.Queries;
using TrackId.Common.Constants;
using TrackId.Contracts.Models.User;
using TrackId.Data.Entities;

namespace TrackId.Application.Commands.Auth.Register
{
    public class RegisterCommand : IRequest<RegisterCommandResult>
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterCommandResult>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<RegisterCommandHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegisterCommandHandler(IMapper mapper,
            ILogger<RegisterCommandHandler> logger,
            UserManager<ApplicationUser> userManager)
        {
            _mapper = mapper;
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<RegisterCommandResult> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                return new RegisterCommandResult(RequestErrorType.InvalidArgument, "Invalid arguments");
            }

            if (!request.Password.Equals(request.ConfirmPassword, StringComparison.InvariantCulture))
            {
                return new RegisterCommandResult(RequestErrorType.ValidationError, ErrorConstants.Auth.PasswordsDoNotMatch);
            }

            if (_mapper.Map<ApplicationUser>(request) is not ApplicationUser user)
            {
                _logger.LogWarning("Could not map user from request to DTO.");
                return new RegisterCommandResult(RequestErrorType.Unknown, ErrorConstants.GeneralError);
}

            if (await _userManager.FindByEmailAsync(request.Email) is not null)
            {
                return new RegisterCommandResult(RequestErrorType.NotCreated, ErrorConstants.Auth.UserAlreadyExists);
            }

            if (await _userManager.CreateAsync(user, request.Password) is not IdentityResult identityResult)
            {
                return new RegisterCommandResult(RequestErrorType.NotCreated, ErrorConstants.Auth.CreatingAccountFailed);
            }

            if (!identityResult.Succeeded)
            {
                return new RegisterCommandResult(RequestErrorType.NotCreated,
                    string.Join(',', identityResult.Errors.Select(s => s.Description)));
            }

            await _userManager.AddToRoleAsync(user, RoleConstants.User);

            return new RegisterCommandResult(new RegistrationResponse());
        }
    }

    public class RegisterCommandResult : BaseQueryResponse<RegistrationResponse>
    {
        public RegisterCommandResult(RegistrationResponse result) : base(result)
        {
        }

        public RegisterCommandResult(RequestErrorType errorType, string errorMessage) : base(errorType, errorMessage)
        {
        }

        public RegisterCommandResult(bool success, RequestErrorType errorType, string errorMessage) : base(success, errorType, errorMessage)
        {
        }
    }
}
