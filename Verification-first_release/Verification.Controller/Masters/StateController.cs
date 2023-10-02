using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.DomainModel;
using Verification.Repository;
using static Verification.Tracker.GlobleEnums;

namespace Verification.Controller.Masters;

[Route("Masters/[controller]")]
[Authorize]
[ApiController]
public class StateController : ControllerBase
{

    private readonly IStateRepository state;

    public StateController(IStateRepository state)
    {
        this.state = state;
    }

    [HttpGet("[action]/{IsActive:bool?}")]
    public async Task<IActionResult> Count(bool? IsActive)
    {

        return Ok(await state.Count(IsActive == null ? null : IsActive));

    }
    [HttpGet("[action]/{IsActive:bool?}")]
    public async Task<IActionResult> Records(bool? IsActive)
    {
        var result = await state.Records(IsActive == null ? null : IsActive);
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
    public async Task<IActionResult> RecordById(int Id)
    {
        var result = await state.RecordById(Id);
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
    public async Task<IActionResult> RecordsByPaging(int take, int skip)
    {
        var result = await state.RecordsByPagingAll(take, skip);
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
    public async Task<IActionResult> RecordsByPaging(bool IsActive, int take, int skip)
    {
        var result = await state.RecordsByPagingFilter(IsActive, take, skip);
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
    public async Task<IActionResult> Create([FromBody] StateModel model)
    {
        var result = await state.Create(model);
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

    [HttpPut("[action]/{Id}")]
    public async Task<IActionResult> Update([FromRoute] int Id, [FromBody] StateModel model)
    {
        var result = await state.Update(Id, model);
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

    [HttpDelete("[action]/{Id}")]
    public async Task<IActionResult> Delete([FromRoute] int Id)
    {
        var result = await state.Delete(Id);
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
