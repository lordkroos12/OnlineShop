using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace OnlineShopUI.Models
{
    [Table("Category")]
    public class Category
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public List<Part>  Parts { get; set; }
    }
}
