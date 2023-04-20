using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Diagnostics.Metrics;
using System.Net;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class CosCumparaturiService : ICosCumparaturiService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public CosCumparaturiService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse> AddCosCumparaturi(CosCumparaturiAddDTO cosCumparaturi, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add cosuri de cumparaturi!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new CosCumparaturiSpec(cosCumparaturi.UserId), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "This cosCumparaturi already exists!", ErrorCodes.CosCumparaturiAlreadyExists));
        }

        await _repository.AddAsync(new CosCumparaturi
        {
            TotalCost = cosCumparaturi.TotalCost,
            DeliveryCost = cosCumparaturi.DeliveryCost,
            UserId = cosCumparaturi.UserId
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCosCumparaturi(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete a cosCumparaturi!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<CosCumparaturi>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<CosCumparaturiDTO>> GetCosCumparaturi(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new CosCumparaturiProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<CosCumparaturiDTO>.ForSuccess(result) :
            ServiceResponse<CosCumparaturiDTO>.FromError(CommonErrors.CosCumparaturiNotFound);
    }

    public async Task<ServiceResponse<int>> GetCosuriCumparaturiCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<CosCumparaturi>(cancellationToken));


    public async Task<ServiceResponse> UpdateCosCumparaturi(CosCumparaturiUpdateDTO cosCumparaturi, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can update CosCumparaturi!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new CosCumparaturiSpec(cosCumparaturi.Id), cancellationToken);

        if (entity != null)
        {
            entity.TotalCost = cosCumparaturi.TotalCost ?? entity.TotalCost;
            entity.DeliveryCost = cosCumparaturi.DeliveryCost ?? entity.DeliveryCost;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }
}
