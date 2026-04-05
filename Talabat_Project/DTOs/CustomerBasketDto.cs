using Core.Entites;
using System.ComponentModel.DataAnnotations;

namespace Talabat_Project.DTOs
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public List<BasketItemDto> Items { get; set; }
    }
}
