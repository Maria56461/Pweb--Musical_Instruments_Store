using Ardalis.Specification;
using Microsoft.EntityFrameworkCore;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class SubcategorieProjectionSpec : BaseSpec<SubcategorieProjectionSpec, Subcategorie, SubcategorieDTO>
{
    protected override Expression<Func<Subcategorie, SubcategorieDTO>> Spec => e => new()
    {
        Id = e.Id,
        Name = e.Name,
        Description = e.Description,
        Categorie = new()
        {
            Id = e.Categorie.Id,
            Name = e.Categorie.Name,
            Description = e.Categorie.Description
        },
    };

    public SubcategorieProjectionSpec(Guid id) : base(id)
    {
    }

    public SubcategorieProjectionSpec(string? search)
    {
        search = !string.IsNullOrWhiteSpace(search) ? search.Trim() : null;

        if (search == null)
        {
            return;
        }

        var searchExpr = $"%{search.Replace(" ", "%")}%";

        Query.Where(e => EF.Functions.ILike(e.Name, searchExpr) ||
                         EF.Functions.ILike(e.Description, searchExpr));
    }
}
