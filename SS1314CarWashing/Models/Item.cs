using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SS1314CarWashing.Models;

public class Item
{
    [Key]
    public Guid ItemId { get; set; }
    [ForeignKey("ItemType")]
    [Display(Name ="Item Type")]
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
    public string Image { get; set; }
    public int? Qty { get; set; }
    [Display(Name = "Brand Name")]
    public string BrandId { get; set; }
    [Display(Name = "Model")]
    public string ModelId { get; set; }
    [Display(Name = "Oil Type")]
    public string OilTypeId { get; set; }
    public ItemType ItemType { get; set; }
}
