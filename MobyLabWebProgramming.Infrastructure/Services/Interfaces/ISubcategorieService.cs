using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ISubcategorieService
{
    public Task<ServiceResponse<SubcategorieDTO>> GetSubcategorie(Guid id, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<PagedResponse<SubcategorieDTO>>> GetSubcategorii(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);

    public Task<ServiceResponse<int>> GetSubcategoriiCount(CancellationToken cancellationToken = default);

    public Task<ServiceResponse> AddSubcategorie(SubcategorieAddDTO subcategorie, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> UpdateSubcategorie(SubcategorieUpdateDTO subcategorie, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

    public Task<ServiceResponse> DeleteSubcategorie(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}

