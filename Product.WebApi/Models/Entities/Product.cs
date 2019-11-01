namespace Product.WebApi.Models.Entities
{
    public class Product : StorableEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
