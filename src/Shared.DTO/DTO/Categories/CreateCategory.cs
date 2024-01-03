using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.DTO.Categories
{
    public class CreateCategory
    {
        [Display(Name = "Name", ResourceType = typeof(Resources.Strings))]
        public string Name { get; set; } = null!;
    }
}
