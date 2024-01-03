using System;

namespace Auth.Domain.Models
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsLocked { get; init; }
    }

}
