using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class CategorieController : AuthorizedController
{
    private readonly ICategorieService _categorieService;
    public CategorieController(IUserService userService, ICategorieService categorieService) : base(userService)
    {
        _categorieService = categorieService;
    }

    [Authorize]
    [HttpGet] // route /api/Categorie/GetCategories
    public async Task<ActionResult<RequestResponse<PagedResponse<CategorieDTO>>>> GetPage([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _categorieService.GetCategorii(pagination)) :
            this.ErrorMessageResult<PagedResponse<CategorieDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")] // route /api/Categorie/GetById/<some_guid>
    public async Task<ActionResult<RequestResponse<CategorieDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _categorieService.GetCategorie(id)) :
            this.ErrorMessageResult<CategorieDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] CategorieAddDTO categorie)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _categorieService.AddCategorie(categorie, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] CategorieUpdateDTO categorie)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _categorieService.UpdateCategorie(categorie, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")] //route /api/Categorie/Delete/<some_guid>
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _categorieService.DeleteCategorie(id)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
