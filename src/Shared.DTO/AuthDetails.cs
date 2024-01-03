using System;

namespace Shared.DTO
{
    public class AuthDetails
    {
        public string Username { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = null!;
    }
}
