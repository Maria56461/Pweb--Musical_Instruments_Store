using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class Categorie : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public ICollection<Subcategorie> Subcategorii { get; set; } = default!;
    }
}
