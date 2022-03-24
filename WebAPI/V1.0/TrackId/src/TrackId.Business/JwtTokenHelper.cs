using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TrackId.Business.Dto;
using TrackId.Business.Interfaces;
using TrackId.Data;
using TrackId.Data.Entities;
using TrackId.Data.Interfaces;

namespace TrackId.Business
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly JwtTokenOptions _tokenOptions;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IDateTimeProvider _dateTimeProvider;

        public JwtTokenHelper(
            JwtTokenOptions tokenOptions,
            UserManager<ApplicationUser> userManager,
            IMapper mapper,
            IDateTimeProvider dateTimeProvider)
        {
            if (tokenOptions == null)
            {
                throw new ArgumentNullException(nameof(tokenOptions));
            }

            _tokenOptions = tokenOptions;
            _userManager = userManager;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        public async Task<string> CreateToken(UserDto user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var applicationUser = _mapper.Map<ApplicationUser>(user);
            foreach (var role in await _userManager.GetRolesAsync(applicationUser))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenOptions.Secret));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _tokenOptions.Issuer,
                Audience = _tokenOptions.Audience,
                Expires = _dateTimeProvider.UtcNow.AddMonths(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
