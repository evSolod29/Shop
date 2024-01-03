using System.ComponentModel.DataAnnotations;
using Shared.Resources;
using Shop.Domain.Models.Base;

namespace Shop.Domain.Models
{
    [Display(Name = nameof(Strings.Category), ResourceType = typeof(Strings))]
    public class Category : BaseEntity
    {
        [Display(Name = nameof(Strings.Name), ResourceType = typeof(Strings))]
        public string Name { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
