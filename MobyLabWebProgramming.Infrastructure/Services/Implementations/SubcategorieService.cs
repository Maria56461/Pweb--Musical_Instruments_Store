using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Implementation;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;
using System.Net;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class SubcategorieService : ISubcategorieService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public SubcategorieService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse> AddSubcategorie(SubcategorieAddDTO subcategorie, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can add subcategories!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new CategorieSpec(subcategorie.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "This subcategory already exists!", ErrorCodes.UserAlreadyExists));
        }

        await _repository.AddAsync(new Subcategorie
        {
            Name = subcategorie.Name,
            Description = subcategorie.Description,
            CategoryId = subcategorie.CategoryId
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteSubcategorie(Guid id, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete a subcategory!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Subcategorie>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse<SubcategorieDTO>> GetSubcategorie(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new SubcategorieProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<SubcategorieDTO>.ForSuccess(result) :
            ServiceResponse<SubcategorieDTO>.FromError(CommonErrors.SubcategorieNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<SubcategorieDTO>>> GetSubcategorii(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new SubcategorieProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<SubcategorieDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetSubcategoriiCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Subcategorie>(cancellationToken));


    public async Task<ServiceResponse> UpdateSubcategorie(SubcategorieUpdateDTO subcategorie, UserDTO? requestingUser = null, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can update the subcategory!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new SubcategorieSpec(subcategorie.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = subcategorie.Name ?? entity.Name;
            entity.Description = subcategorie.Description ?? entity.Description;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }
}
