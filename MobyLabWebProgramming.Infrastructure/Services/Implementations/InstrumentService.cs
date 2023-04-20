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
using System.Net;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class InstrumentService : IInstrumentService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public InstrumentService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddInstrument(InstrumentAddDTO instrument, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add instruments!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new InstrumentSpec(instrument.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "This instrument already exists!", ErrorCodes.InstrumentAlreadyExists));
        }

        await _repository.AddAsync(new Instrument
        {
            Name = instrument.Name,
            Description = instrument.Description,
            Price = instrument.Price,
            Color = instrument.Color,
            SubcategorieId = instrument.SubcategorieId,
            CosId = instrument.CosId,
            Reviews = new List<string>(instrument.Reviews),
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteInstrument(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete an Instrument!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Instrument>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<InstrumentDTO>> GetInstrument(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new InstrumentProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<InstrumentDTO>.ForSuccess(result) :
            ServiceResponse<InstrumentDTO>.FromError(CommonErrors.InstrumentNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<InstrumentDTO>>> GetInstruments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new InstrumentProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<InstrumentDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetInstrumentsCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Instrument>(cancellationToken));


    public async Task<ServiceResponse> UpdateInstrument(InstrumentUpdateDTO instrument, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can update the instrument!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new InstrumentSpec(instrument.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = instrument.Name ?? entity.Name;
            entity.Description = instrument.Description ?? entity.Description;
            entity.Price = instrument.Price ?? entity.Price;
            entity.Reviews = instrument.Reviews ?? entity.Reviews;
            entity.Color = instrument.Color ?? entity.Color;
            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }
}

