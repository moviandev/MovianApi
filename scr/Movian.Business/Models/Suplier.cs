using Movian.Business.EnumTypes;

namespace Movian.Business.Models
{
  public class Suplier : Entity
  {
    public string Name { get; set; }
    public string Documents { get; set; }
    public bool Active { get; set; }
    public SuplierType SuplierType { get; set; }
    public Address Address { get; set; }

    /* EF Relations */
    public IEnumerable<Product> Products { get; set; }
  }
}