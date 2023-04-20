namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class InstrumentDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public float Price { get; set; } = default!;
    public List<string>? Reviews { get; set; }
    public string Color { get; set; } = default!;
    public Guid SubcategorieId { get; set; }
    public Guid CosId { get; set; }
}
