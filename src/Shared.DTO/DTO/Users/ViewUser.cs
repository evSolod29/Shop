using System;

namespace Shared.DTO.DTO.Users
{
    public class ViewUser
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool IsLocked { get; set; }
    }
}
