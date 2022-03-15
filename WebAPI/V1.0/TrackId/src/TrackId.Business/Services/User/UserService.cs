using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TrackId.Business.Dto;
using TrackId.Business.User;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces.Repositories;

namespace TrackId.Business
{
    public class UserService : IUserService
    {
        private readonly ILogger<UserService> _logger;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserService(
            ILogger<UserService> logger, IUserRepository userRepository, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _userRepository = userRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<UserDto> GetById(Guid id, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user is null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return _mapper.Map<UserDto>(user);
        }
    }
}
