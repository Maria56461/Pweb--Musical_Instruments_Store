using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Subcategorie : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public Categorie Categorie { get; set; } = default!;
        public Guid CategoryId { get; set; }
        public ICollection<Instrument> Instruments { get; set; } = default!;
    }
}
