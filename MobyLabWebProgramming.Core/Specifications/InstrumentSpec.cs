using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class InstrumentSpec : BaseSpec<InstrumentSpec, Instrument>
{
    public InstrumentSpec(Guid id) : base(id)
    {
    }

    public InstrumentSpec(string nume)
    {
        Query.Where(e => e.Name == nume);
    }
}
