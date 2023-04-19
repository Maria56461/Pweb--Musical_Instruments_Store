using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Infrastructure.Authorization;
using MobyLabWebProgramming.Infrastructure.Extensions;
using MobyLabWebProgramming.Infrastructure.Services.Implementations;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Backend.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class SubcategorieController : AuthorizedController
{
    private readonly ISubcategorieService _subcategorieService;
    public SubcategorieController(IUserService userService, ISubcategorieService subcategorieService) : base(userService)
    {
        _subcategorieService = subcategorieService;
    }

    [Authorize]
    [HttpGet] // route /api/Subcategorie/GetSubcategories
    public async Task<ActionResult<RequestResponse<PagedResponse<SubcategorieDTO>>>> GetSubcategories([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subcategorieService.GetSubcategorii(pagination)) :
            this.ErrorMessageResult<PagedResponse<SubcategorieDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")] // route /api/Subcategorie/GetById/<some_guid>
    public async Task<ActionResult<RequestResponse<SubcategorieDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subcategorieService.GetSubcategorie(id)) :
            this.ErrorMessageResult<SubcategorieDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] SubcategorieAddDTO subcategorie)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subcategorieService.AddSubcategorie(subcategorie, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] SubcategorieUpdateDTO subcategorie)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subcategorieService.UpdateSubcategorie(subcategorie, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")] //route /api/Subcategorie/Delete/<some_guid>
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _subcategorieService.DeleteSubcategorie(id)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
