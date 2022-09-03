using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Movian.Api.Models
{
  public class SupplierDto
  {
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(200, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(14, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 11)]
    public string Document { get; set; }

    public int SupplierType { get; set; }

    public bool Active { get; set; }

    public IEnumerable<ProductDto> Products { get; set; }

    public AddressDto Address { get; set; }
  }
}