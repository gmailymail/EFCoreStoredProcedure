using System.Collections.Generic;
using System.Collections.ObjectModel;

#nullable disable
namespace SPDataRepository.Data.Entities
{
    public record Brand : EntityBase
    {
        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
