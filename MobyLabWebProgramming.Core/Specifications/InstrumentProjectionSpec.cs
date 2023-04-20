
using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class InstrumentProjectionSpec : BaseSpec<InstrumentProjectionSpec, Instrument, InstrumentDTO>
{
    protected override Expression<Func<Instrument, InstrumentDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        Price = e.Price,
        SubcategorieId = e.SubcategorieId,
        CosId = e.CosId,
        Color = e.Color,
        Reviews = new List<string>(e.Reviews)
    };

    public InstrumentProjectionSpec(Guid id) : base(id)
    {
    }

    public InstrumentProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr) ||
                         EF.Functions.ILike(e.Description, searchExpr) ||
                         EF.Functions.ILike(e.Color, searchExpr));
    }
}
