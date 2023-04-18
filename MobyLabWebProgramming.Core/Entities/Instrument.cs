using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Instrument : BaseEntity
    {
        public Subcategorie Subcategorie { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public float Price { get; set; } = default!;
        public List<string>? Reviews { get; set; }
        public string Color { get; set; } = default!;
        public Guid SubcategorieId { get; set; }
        public CosCumparaturi CosCumparaturi { get; set; } = default!;
        public Guid CosId { get; set; }
    }
}
