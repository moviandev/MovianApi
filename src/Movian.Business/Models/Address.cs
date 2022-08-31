using System;

namespace Movian.Business.Models
{
  public class Address : Entity
  {
    public Guid SupplierId { get; set; }
    public string CompleteAddress { get; set; }
    public string Neighborhood { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string AdditionalAddressData { get; set; }
    public string Number { get; set; }

    /* EF Relations */
    public Supplier Supplier { get; set; }
  }
}