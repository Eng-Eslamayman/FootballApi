using AutoMapper;
using FootballApi.Core.DTOs;
using FootballApi.EF;
using FootballApi.Core.Services;
using FootballApi.EF.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootballApi.Core.Helpers;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using FootballApi.Core.Models;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace FootballApi.EF.Repositories
{
    public class AuthRepository : IAuthSevice
    {
        readonly UserManager<ApplicationUser> _userManager;
        readonly IMapper _mapper;
        readonly RoleManager<IdentityRole> _roleManager;
        readonly JWT _jwt;

        public AuthRepository(UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwt = jwt.Value;
            _roleManager = roleManager;
        }

        public async Task<AuthModelDTO> RegistrationAsync(RegisterDTO dto)
        {
            if (await _userManager.FindByNameAsync(dto.UserName) is not null)
                return new AuthModelDTO { Message = "Username is already registered!" };

            if (await _userManager.FindByEmailAsync(dto.Email) is not null)
                return new AuthModelDTO { Message = "Email is already registered!" };

            var user = _mapper.Map<ApplicationUser>(dto);

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description},";
                }
                return new AuthModelDTO { Message = errors };
            }
            await _userManager.AddToRoleAsync(user, "User");
            var token = await CreateJwtToken(user);
            return new AuthModelDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                ExpiredOn = token.ValidTo,
                IsAuthenticated = true,
                Message = "Registration Successed",
                Roles = new List<string>() { "User" }
            };

        }

        public async Task<AuthModelDTO> LoginAsync(LoginDTO dto)
        {
            var authDto = new AuthModelDTO();
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user is null ||! await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                authDto.Message = "Email or Password is incorrect!";
                return authDto;
            }

            var token = await CreateJwtToken(user);
            var roles = await _userManager.GetRolesAsync(user);

            authDto.IsAuthenticated = true;
            authDto.UserName = user.UserName;
            authDto.Roles = roles.ToList();
            authDto.Email = user.Email;
            authDto.Token = new JwtSecurityTokenHandler().WriteToken(token);
            authDto.ExpiredOn = token.ValidTo;

            if(user.RefreshTokens.Any(r => r.IsActive))
            {
                var activeRefreshToken = user.RefreshTokens.SingleOrDefault(r => r.IsActive);
                authDto.RefreshToken = activeRefreshToken.Token;
                authDto.RefreshTokenExpiration = activeRefreshToken.ExpiredOn;
            }
            else
            {
                var refreshToken = GenerateRefreshToken();
                authDto.RefreshToken = refreshToken.Token;
                authDto.RefreshTokenExpiration = refreshToken.ExpiredOn;
                user.RefreshTokens.Add(refreshToken);
                await _userManager.UpdateAsync(user);
            }
            return authDto;
        }

        public async Task<string> AddRoleAsync(RoleDTO model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);
            if (user is null || await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went worng";
        }

        public async Task<bool> RevokeTokenAsync(string token)
        {
            var user = await _userManager.Users.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user is null)
                return false;

            var refreshToken = user.RefreshTokens.Single(t => t.Token == token);
            if (!refreshToken.IsActive)
                return false;

            refreshToken.RevokedOn = DateTime.UtcNow;

            await _userManager.UpdateAsync(user);
            return true;
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaim = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaim.Add(new Claim(ClaimTypes.Role, role));
            }

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaim);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwt.ValidIssuer,
                audience: _jwt.ValidAudiance,
                claims: claims,
                expires:DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signInCredentials
                );
            return token;
        }
        private RefreshToken GenerateRefreshToken()
        {
            var RandomNumber = new byte[32];
            using var generator = new RNGCryptoServiceProvider();
            generator.GetBytes(RandomNumber);
            return new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumber),
                ExpiredOn = DateTime.UtcNow.AddDays(10),
                CreatedOn = DateTime.UtcNow,
            };

        }
    }
}
