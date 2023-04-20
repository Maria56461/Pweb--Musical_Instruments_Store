using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IInstrumentService
{
    public Task<ServiceResponse<InstrumentDTO>> GetInstrument(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<InstrumentDTO>>> GetInstruments(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetInstrumentsCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddInstrument(InstrumentAddDTO instrument, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateInstrument(InstrumentUpdateDTO instrument, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteInstrument(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}


