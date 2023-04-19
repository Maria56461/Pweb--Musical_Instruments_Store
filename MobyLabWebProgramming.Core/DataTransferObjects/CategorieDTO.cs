
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CategorieDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
}
