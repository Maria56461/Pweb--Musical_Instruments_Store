namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CosCumparaturiAddDTO
{
    public float TotalCost { get; set; } = default!;
    public float DeliveryCost { get; set; } = default!;
    public Guid UserId { get; set; }
}
