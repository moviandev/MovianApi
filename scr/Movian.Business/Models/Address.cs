namespace Movian.Business.Models
{
  public class Address : Entity
  {
    public Guid SuplierId { get; set; }
    public string CompleteAddress { get; set; }
    public string Neighborhood { get; set; }
    public string ZipCode { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string AdditionalAddressData { get; set; }
    public string Number { get; set; }

    /* EF Relations */
    public Suplier Suplier { get; set; }
  }
}