using Ardalis.Specification;
using MobyLabWebProgramming.Core.Entities;
namespace MobyLabWebProgramming.Core.Specifications;

public sealed class SubcategorieSpec : BaseSpec<SubcategorieSpec, Subcategorie>
{
    public SubcategorieSpec(Guid id) : base(id)
    {
    }

    public SubcategorieSpec(string nume)
    {
        Query.Where(e => e.Name == nume);
    }
}
