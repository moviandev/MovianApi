using System;
using System.Collections.Generic;
using Movian.Business.EnumTypes;

namespace Movian.Business.Models
{
  public class Supplier : Entity
  {
    public string Name { get; set; }
    public string Document { get; set; }
    public bool Active { get; set; }
    public SupplierType SupplierType { get; set; }
    public Guid AddressId { get; set; }

    /* EF Relations */
    public virtual IEnumerable<Product> Products { get; set; }
    public virtual Address Address { get; set; }

  }
}