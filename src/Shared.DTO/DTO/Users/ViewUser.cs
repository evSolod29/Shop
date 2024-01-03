using System;
using System.ComponentModel.DataAnnotations;
using Shared.Resources;

namespace Shared.DTO.DTO.Users
{
    public class ViewUser
    {
        public string UserId { get; set; } = null!;
        [Display(Name = nameof(Strings.UserName), ResourceType = typeof(Strings))]
        public string Name { get; set; } = null!;
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        public string Email { get; set; } = null!;
        [Display(Name = nameof(Strings.IsLocked), ResourceType = typeof(Strings))]
        public bool IsLocked { get; set; }
    }
}
