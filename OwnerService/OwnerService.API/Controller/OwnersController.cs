using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OwnerService.Application.DTOs;
using OwnerService.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OwnerService.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IOwnerApplicationService _ownerService;

        public OwnersController(IOwnerApplicationService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OwnerDto>>> Get()
        {
            var owners = await _ownerService.GetAllAsync();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<OwnerDto>> Get(string id)
        {
            var owner = await _ownerService.GetByIdAsync(id);
            if (owner == null)
            {
                return NotFound();
            }
            return Ok(owner);
        }

        [HttpPost]
        public async Task<ActionResult<OwnerDto>> Post([FromBody] CreateOwnerDto createOwnerDto)
        {
            var newOwner = await _ownerService.CreateAsync(createOwnerDto);
            return CreatedAtAction(nameof(Get), new { id = newOwner.Id }, newOwner);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Put(string id, [FromBody] UpdateOwnerDto updateOwnerDto)
        {
            await _ownerService.UpdateAsync(id, updateOwnerDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            await _ownerService.DeleteAsync(id);
            return NoContent();
        }
    }
}
