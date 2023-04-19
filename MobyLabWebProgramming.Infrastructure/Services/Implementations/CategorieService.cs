using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using System.Net;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class CategorieService : ICategorieService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public CategorieService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddCategorie(CategorieAddDTO categorie, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add categories!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new CategorieSpec(categorie.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "This category already exists!", ErrorCodes.UserAlreadyExists));
        }

        await _repository.AddAsync(new Categorie
        {
            Name = categorie.Name,
            Description = categorie.Description,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteCategorie(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete a category!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Categorie>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<CategorieDTO>> GetCategorie(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new CategorieProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<CategorieDTO>.ForSuccess(result) :
            ServiceResponse<CategorieDTO>.FromError(CommonErrors.CategorieNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<CategorieDTO>>> GetCategorii(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new CategorieProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<CategorieDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetCategoriiCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Categorie>(cancellationToken));

    public async Task<ServiceResponse> UpdateCategorie(CategorieUpdateDTO categorie, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can update the category!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new CategorieSpec(categorie.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = categorie.Name ?? entity.Name;
            entity.Description = categorie.Description ?? entity.Description;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }
}
