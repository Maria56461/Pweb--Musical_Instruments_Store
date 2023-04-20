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
public class CosCumparaturiController : AuthorizedController
{
    private readonly ICosCumparaturiService _cosCumparaturiService;
    public CosCumparaturiController(IUserService userService, ICosCumparaturiService cosCumparaturiService) : base(userService)
    {
        _cosCumparaturiService = cosCumparaturiService;
    }

    [Authorize]
    [HttpGet("{id:guid}")] 
    public async Task<ActionResult<RequestResponse<CosCumparaturiDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _cosCumparaturiService.GetCosCumparaturi(id)) :
            this.ErrorMessageResult<CosCumparaturiDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] CosCumparaturiAddDTO cosCumparaturi)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _cosCumparaturiService.AddCosCumparaturi(cosCumparaturi, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] CosCumparaturiUpdateDTO cosCumparaturi)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _cosCumparaturiService.UpdateCosCumparaturi(cosCumparaturi, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")] 
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _cosCumparaturiService.DeleteCosCumparaturi(id)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
