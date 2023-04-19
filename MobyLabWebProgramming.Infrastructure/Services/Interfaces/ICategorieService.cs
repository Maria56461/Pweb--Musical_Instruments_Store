using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface ICategorieService
{
    public Task<ServiceResponse<CategorieDTO>> GetCategorie(Guid id, CancellationToken cancellationToken = default);
   
    public Task<ServiceResponse<PagedResponse<CategorieDTO>>> GetCategorii(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    
    public Task<ServiceResponse<int>> GetCategoriiCount(CancellationToken cancellationToken = default);
   
    public Task<ServiceResponse> AddCategorie(CategorieAddDTO categorie, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
   
    public Task<ServiceResponse> UpdateCategorie(CategorieUpdateDTO categorie, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
   
    public Task<ServiceResponse> DeleteCategorie(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}
