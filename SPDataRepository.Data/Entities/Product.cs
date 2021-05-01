#nullable disable
namespace SPDataRepository.Data.Entities
{
    public record Product : EntityBase
    {
        public string Name { get; set; }

        public virtual Brand Brand { get; set; }
    }
}
