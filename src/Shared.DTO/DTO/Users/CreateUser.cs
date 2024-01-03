using System;
using System.ComponentModel.DataAnnotations;
using Shared.Resources;

namespace Shared.DTO.DTO.Users
{
    public class CreateUser
    {
        [Display(Name = nameof(Strings.UserName), ResourceType = typeof(Strings))]
        public string Name { get; set; } = null!;
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        public string Email { get; set; } = null!;
        [Display(Name = nameof(Strings.Password), ResourceType = typeof(Strings))]
        public string Password { get; set; } = null!;
    }
}
