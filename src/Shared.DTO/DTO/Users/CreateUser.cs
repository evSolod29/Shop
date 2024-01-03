using System;

namespace Shared.DTO.DTO.Users
{
    public class CreateUser
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
