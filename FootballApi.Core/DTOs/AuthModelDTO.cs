using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FootballApi.Core.DTOs
{
    public class AuthModelDTO
    {
        public DateTime ExpiredOn { get; set; }
        public string? Token { get; set; }
        public bool IsAuthenticated { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? Message { get; set; }
        public List<string>? Roles { get; set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }


    }
}
