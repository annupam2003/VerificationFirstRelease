using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Verification.DomainModel;
using Verification.Repository;
using static Verification.Tracker.GlobleEnums;

namespace Verification.Controller.Masters;

[Route("Masters/[controller]")]
[Authorize]
[ApiController]
public class StateZoneController : ControllerBase
{
    private readonly IStateZoneRepository stateZone;
    public StateZoneController(IStateZoneRepository stateZone)
	{
        this.stateZone = stateZone;
    }

    [HttpGet("[action]/{IsActive:bool?}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Count(bool? IsActive)
    {
        return Ok(await stateZone.Count(IsActive == null ? null : IsActive));
    }

    [HttpGet("[action]/{IsActive:bool?}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(StateZoneModel), 200)]
    public async Task<IActionResult> Records(bool? IsActive)
    {
        var result = await stateZone.Records(IsActive == null ? null : IsActive);
        switch (result.Item1)
        {
            case ResponseStatus.Success:
                return Ok(result.Item2);
            case ResponseStatus.Fail:
                return NotFound();
            case ResponseStatus.Error:
                return BadRequest();
        }
        return BadRequest();
    }

    [HttpGet("[action]/{Id:int:min(1)}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(StateZoneModel), 200)]
    public async Task<IActionResult> RecordId(int Id)
    {
        var result = await stateZone.RecordById(Id);
        switch (result.Item1)
        {
            case ResponseStatus.Success:
                return Ok(result.Item2);
            case ResponseStatus.Fail:
                return NotFound();
            case ResponseStatus.Error:
                return BadRequest();
        }
        return BadRequest();
    }
    [HttpGet("[action]/{take:int}/{skip:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(StateZoneModel), 200)]
    public async Task<IActionResult> RecordsPaging(int take, int skip)
    {
        var result = await stateZone.RecordsByPagingAll(take, skip);
        switch (result.Item1)
        {
            case ResponseStatus.Success:
                return Ok(result.Item2);
            case ResponseStatus.Fail:
                return NotFound();
            case ResponseStatus.Error:
                return BadRequest();
        }
        return BadRequest();
    }
    [HttpGet("[action]/{IsActive:bool}/{take:int}/{skip:int}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(StateZoneModel), 200)]
    public async Task<IActionResult> RecordsPaging(bool IsActive, int take, int skip)
    {
        var result = await stateZone.RecordsByPagingFilter(IsActive, take, skip);
        switch (result.Item1)
        {
            case ResponseStatus.Success:
                return Ok(result.Item2);
            case ResponseStatus.Fail:
                return NotFound();
            case ResponseStatus.Error:
                return BadRequest();
        }
        return BadRequest();
    }
    [HttpPost("[action]")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Create([FromBody] StateZoneModel model)
    {
        var result = await stateZone.Create(model);
        switch (result.Item1)
        {
            case ResponseStatus.Success:
                return Ok(result.Item2);
            case ResponseStatus.Exists:
                return Ok(result.Item2);
            case ResponseStatus.Fail:
                return NotFound();
            case ResponseStatus.Error:
                return BadRequest();
        }
        return BadRequest();
    }
    [HttpPut("[action]/{Id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] StateZoneModel model)
    {
        var result = await stateZone.Update(Id, model);
        switch (result.Item1)
        {
            case ResponseStatus.Success:
                return Ok(result.Item2);
            case ResponseStatus.Exists:
                return Ok(result.Item2);
            case ResponseStatus.Fail:
                return NotFound();
            case ResponseStatus.Error:
                return BadRequest();
        }
        return BadRequest();
    }
    [HttpDelete("[action]/{Id:int}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Delete([FromRoute] int Id)
    {
        var result = await stateZone.Delete(Id);
        switch (result.Item1)
        {
            case ResponseStatus.Success:
                return Ok(result.Item2);
            case ResponseStatus.Fail:
                return NotFound();
            case ResponseStatus.Error:
                return BadRequest();
        }
        return BadRequest();
    }
}
