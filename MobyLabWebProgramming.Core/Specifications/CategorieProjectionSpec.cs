using Microsoft.EntityFrameworkCore;
using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CategorieProjectionSpec : BaseSpec<CategorieProjectionSpec, Categorie, CategorieDTO>
{
    protected override Expression<Func<Categorie, CategorieDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description
    };

    public CategorieProjectionSpec(Guid id) : base(id)
    {
    }

    public CategorieProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr));
    }
}
