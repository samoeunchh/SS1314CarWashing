﻿using System.ComponentModel.DataAnnotations;

namespace SS1314CarWashing.Models;

public class Model
{
    [Key]
    public Guid ModelId { get; set; }
    [Required]
    [MaxLength(50)]
    [Display(Name = "Model Name")]
    public string ModelName { get; set; }
}
