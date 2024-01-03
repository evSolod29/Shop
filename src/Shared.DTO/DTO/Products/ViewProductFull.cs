using System.ComponentModel.DataAnnotations;
using Shared.DTO.DTO.Categories;

namespace Shared.DTO.DTO.Products
{
    public class ViewProductFull
    {
        public int Id { get; set; }
        [Display(Name = "Name", ResourceType = typeof(Resources.Strings))]
        public string Name { get; set; } = null!;
        [Display(Name = "Description", ResourceType = typeof(Resources.Strings))]
        public string Description { get; set; } = null!;
        [Display(Name = "Price", ResourceType = typeof(Resources.Strings))]
        public decimal Price { get; set; }
        [Display(Name = "CommonNote", ResourceType = typeof(Resources.Strings))]
        public string CommonNote { get; set; } = null!;
        [Display(Name = "AdditionalNote", ResourceType = typeof(Resources.Strings))]
        public string AdditionalNote { get; set; } = null!;
        [Display(Name = "Category", ResourceType = typeof(Resources.Strings))]
        public ViewCategory Category { get; set; } = null!;

    }
}
