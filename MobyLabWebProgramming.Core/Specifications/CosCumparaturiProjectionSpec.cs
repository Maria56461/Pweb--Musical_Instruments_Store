using Ardalis.Specification;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using System.Linq.Expressions;

namespace MobyLabWebProgramming.Core.Specifications;

public sealed class CosCumparaturiProjectionSpec : BaseSpec<CosCumparaturiProjectionSpec, CosCumparaturi, CosCumparaturiDTO>
{
    protected override Expression<Func<CosCumparaturi, CosCumparaturiDTO>> Spec => e => new()
    {
        Id = e.Id,
        TotalCost = e.TotalCost,
        DeliveryCost = e.DeliveryCost,
        User = new()
        {
            Id = e.User.Id,
            Email = e.User.Email,
            Name = e.User.Name,
            Role = e.User.Role
        }
    };

    public CosCumparaturiProjectionSpec(Guid id) : base(id)
    {
    }
}
