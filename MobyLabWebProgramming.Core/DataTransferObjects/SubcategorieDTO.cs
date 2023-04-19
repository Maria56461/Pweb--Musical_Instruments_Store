
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class SubcategorieDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public CategorieDTO Categorie { get; set; } = default!;
}
