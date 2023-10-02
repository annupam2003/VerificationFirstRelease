using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NLog;
using Microsoft.AspNetCore.Authorization;
using Verification.DomainModel;
using Verification.Repository;
using static Verification.Tracker.Track;
using static Verification.Tracker.GlobleEnums;

namespace Verification.Controller.Masters;

[Route("Masters/[controller]")]
[Authorize]
[ApiController]
public class AreaZoneController : ControllerBase
{
    private readonly IAreaZoneRepository areaZone;
    public AreaZoneController(IAreaZoneRepository areaZone)
    {
        this.areaZone = areaZone;
    }
    [HttpGet("[action]/{IsActive:bool?}")]
    [ProducesResponseType(200)]
    public async Task<IActionResult> Count(bool? IsActive)
    {
        return Ok(await areaZone.Count(IsActive == null ? null : IsActive));
    }

    [HttpGet("[action]/{IsActive:bool?}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(typeof(AreaZoneModel), 200)]
    public async Task<IActionResult> Records(bool? IsActive)
    {
        var result = await areaZone.Records(IsActive == null ? null : IsActive);
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
    [ProducesResponseType(typeof(AreaZoneModel), 200)]
    public async Task<IActionResult> RecordId(int Id)
    {
        var result = await areaZone.RecordById(Id);
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
    [ProducesResponseType(typeof(AreaZoneModel), 200)]
    public async Task<IActionResult> RecordsPaging(int take, int skip)
    {
        var result = await areaZone.RecordsByPagingAll(take, skip);
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
    [ProducesResponseType(typeof(AreaZoneModel), 200)]
    public async Task<IActionResult> RecordsPaging(bool IsActive, int take, int skip)
    {
        var result = await areaZone.RecordsByPagingFilter(IsActive, take, skip);
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
    public async Task<IActionResult> Create([FromBody] AreaZoneModel model)
    {
        var result = await areaZone.Create(model);
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
    public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] AreaZoneModel model)
    {
        var result = await areaZone.Update(Id, model);
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
        var result = await areaZone.Delete(Id);
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