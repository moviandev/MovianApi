using System;
using System.ComponentModel.DataAnnotations;

namespace Movian.Api.Models
{
  public class AddressDto
  {
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(200, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string StreetName { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(200, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string NeighborhoodName { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(8, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 8)]
    public string ZipCode { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(100, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string City { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(50, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string StateName { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(200, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string AddtionalAddressData { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(50, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string Number { get; set; }

    [ScaffoldColumn(false)]
    public Guid SupplierId { get; set; }
  }
}