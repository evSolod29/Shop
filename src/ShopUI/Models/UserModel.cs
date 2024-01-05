using System.ComponentModel.DataAnnotations;
using Shared.Resources;

namespace ShopUI.Models
{
    public class UserModel
    {
        public string Id { get; set; } = null!;
        [Display(Name = nameof(Strings.UserName), ResourceType = typeof(Strings))]
        public string Name { get; set; } = null!;
        [Display(Name = nameof(Strings.Email), ResourceType = typeof(Strings))]
        public string Email { get; set; } = null!;
        [Display(Name = nameof(Strings.IsLocked), ResourceType = typeof(Strings))]
        public IEnumerable<string> Roles { get; set; } = new List<string>();
        public string RolesToStr { get; set; } = null!;
        public bool IsLocked { get; set; }
    }
}
