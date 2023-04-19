
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class SubcategorieAddDTO
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public Guid CategoryId { get; set; }
}
