
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICosCumparaturiService 
{
    public Task<ServiceResponse<CosCumparaturiDTO>> GetCosCumparaturi(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetCosuriCumparaturiCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddCosCumparaturi(CosCumparaturiAddDTO cosCumparaturi, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateCosCumparaturi(CosCumparaturiUpdateDTO cosCumparaturi, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteCosCumparaturi(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}

