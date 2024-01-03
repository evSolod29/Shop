using System;

namespace Shared.DTO
{
    public class AuthDetails
    {
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
