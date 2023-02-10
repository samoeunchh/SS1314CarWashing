using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace SS1314CarWashing.Models;

public class ItemDTO
{
    [Key]
    public Guid ItemId { get; set; }
    [ForeignKey("ItemType")]
    [Display(Name = "Item Type")]
    public Guid ItemTypeId { get; set; }
    [Required]
    [MaxLength(50)]
    [Display(Name = "Item Name")]
    public string ItemName { get; set; }
    public double Price { get; set; }
    [MaxLength(50)]
    public string Size { get; set; }
    [Display(Name = "Is Product")]
    public bool IsStock { get; set; }
    public IFormFile Image { get; set; }
    public int? Qty { get; set; }
    [Display(Name = "Brand Name")]
    public string BrandId { get; set; }
    [Display(Name = "Model")]
    public string ModelId { get; set; }
    [Display(Name = "Oil Type")]
    public string OilTypeId { get; set; }
    public ItemType ItemType { get; set; }
}
