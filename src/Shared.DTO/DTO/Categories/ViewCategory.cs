using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.DTO.Categories
{
    public class ViewCategory
    {
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Strings))]
        public string Name { get; set; } = null!;
    }
}
