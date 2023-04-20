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
public class InstrumentController : AuthorizedController
{
    private readonly IInstrumentService _instrumentService;
    public InstrumentController(IUserService userService, IInstrumentService instrumentService) : base(userService)
    {
        _instrumentService = instrumentService;
    }

    [Authorize]
    [HttpGet] // route /api/Instrument/GetInstruments
    public async Task<ActionResult<RequestResponse<PagedResponse<InstrumentDTO>>>> GetInstruments([FromQuery] PaginationSearchQueryParams pagination)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _instrumentService.GetInstruments(pagination)) :
            this.ErrorMessageResult<PagedResponse<InstrumentDTO>>(currentUser.Error);
    }

    [Authorize]
    [HttpGet("{id:guid}")] // route /api/Instrument/GetById/<some_guid>
    public async Task<ActionResult<RequestResponse<InstrumentDTO>>> GetById([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _instrumentService.GetInstrument(id)) :
            this.ErrorMessageResult<InstrumentDTO>(currentUser.Error);
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<RequestResponse>> Add([FromBody] InstrumentAddDTO instrument)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _instrumentService.AddInstrument(instrument, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpPut]
    public async Task<ActionResult<RequestResponse>> Update([FromBody] InstrumentUpdateDTO instrument)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _instrumentService.UpdateInstrument(instrument, currentUser.Result)) :
            this.ErrorMessageResult(currentUser.Error);
    }

    [Authorize]
    [HttpDelete("{id:guid}")] //route /api/Instrument/Delete/<some_guid>
    public async Task<ActionResult<RequestResponse>> Delete([FromRoute] Guid id)
    {
        var currentUser = await GetCurrentUser();

        return currentUser.Result != null ?
            this.FromServiceResponse(await _instrumentService.DeleteInstrument(id)) :
            this.ErrorMessageResult(currentUser.Error);
    }
}
