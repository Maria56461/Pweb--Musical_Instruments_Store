using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CategorieSpec : BaseSpec<CategorieSpec, Categorie>
{
    public CategorieSpec(Guid id) : base(id)
    {
    }

    public CategorieSpec(string nume)
    {
        Query.Where(e => e.Name == nume);
    }
}
