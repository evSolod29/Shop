using System;
using System.ComponentModel.DataAnnotations;
using Shared.Resources;
using Shop.Domain.Models.Base;

namespace Shop.Domain.Models
{
    [Display(Name = "Product", ResourceType = typeof(Strings))]
    public class Product : BaseEntity
    {
        [Display(Name = "Name", ResourceType = typeof(Strings))]
        public string Name { get; set; } = null!;
        [Display(Name = "Description", ResourceType = typeof(Strings))]
        public string Description { get; set; } = null!;
        [Display(Name = "Price", ResourceType = typeof(Strings))]
        public decimal Price { get; set; }
        [Display(Name = "CommonNote", ResourceType = typeof(Strings))]
        public string CommonNote { get; set; } = null!;
        [Display(Name = "AdditionalNote", ResourceType = typeof(Strings))]
        public string AdditionalNote { get; set; } = null!;
        [Display(Name = "Category", ResourceType = typeof(Strings))]
        public Category Category { get; set; } = null!;

    }
}
