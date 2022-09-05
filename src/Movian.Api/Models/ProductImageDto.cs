using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Movian.Api.Extensions;

namespace Movian.Api.Models
{
  [ModelBinder(typeof(ProductBinder), Name = "product")]
  public class ProductImageDto
  {
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    public Guid SupplierId { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(200, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string Name { get; set; }

    [Required(ErrorMessage = "The field {0} is required")]
    [StringLength(1000, ErrorMessage = "The field {0} need to have in between {2} and {1} Characteres", MinimumLength = 2)]
    public string Description { get; set; }

    public IFormFile ImageUpload { get; set; }

    public string Image { get; set; }

    [Required(ErrorMessage = "O campo {0} é obrigatório")]
    public decimal Value { get; set; }

    public bool Active { get; set; }

    [ScaffoldColumn(false)]
    public DateTime CreatedIn { get; set; }

    [ScaffoldColumn(false)]
    public string SupplierName { get; set; }
  }
}