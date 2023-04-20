using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public class CosCumparaturiDTO
{
    public Guid Id { get; set; }
    public float TotalCost { get; set; } = default!;
    public float DeliveryCost { get; set; } = default!;
    public UserDTO User { get; set; } = default!;

}
