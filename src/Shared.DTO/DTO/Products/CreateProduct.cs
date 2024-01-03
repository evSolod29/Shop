using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTO.DTO.Products
{
    public class CreateProduct
    {
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
        public long CategoryId { get; set; }
    }
}
