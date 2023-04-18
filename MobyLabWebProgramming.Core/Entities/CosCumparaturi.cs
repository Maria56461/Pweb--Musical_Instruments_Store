using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Core.Entities
{
    public class CosCumparaturi : BaseEntity
    {
        public User User { get; set; } = default!;
        public ICollection<Instrument> Instruments { get; set; } = default!;
        public float TotalCost { get; set; } = default!;
        public float DeliveryCost { get; set; } = default!;
        public Guid UserId { get; set; }

    }
}
