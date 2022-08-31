namespace Movian.Business.Models
{
  public class Product : Entity
  {
    public Guid SuplierId { get; set; }
    public decimal Value { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Image { get; set; }
    public DateTime CreatedIn { get; set; }
    public bool Active { get; set; }

    /* EF Relations */
    public Suplier Suplier { get; set; }
  }
}