using FootballApi.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootballApi.Core.Services
{
    public interface IAuthSevice
    {
        Task<AuthModelDTO> RegistrationAsync(RegisterDTO dto);
        Task<AuthModelDTO> LoginAsync(LoginDTO dto);
        Task<string> AddRoleAsync(RoleDTO dto);
        Task<bool> RevokeTokenAsync(string token);
    }
}
