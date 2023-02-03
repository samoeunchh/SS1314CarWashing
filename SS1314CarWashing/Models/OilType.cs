using System.ComponentModel.DataAnnotations;

namespace SS1314CarWashing.Models;

public class OilType
{
    [Key]
    public Guid OilTypeId { get; set; }
    [Required]
    [MaxLength(50)]
    [Display(Name = "Oil Type Name")]
    public string OilTypeName { get; set; }
}
