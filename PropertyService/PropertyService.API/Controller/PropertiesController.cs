using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PropertyService.Application.Dtos;
using PropertyService.Application.Services;
using PropertyService.Domain.Models;
using System.Threading.Tasks;

namespace PropertyService.API.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PropertiesController : ControllerBase
    {
        private readonly IPropertyAppService _propertyAppService;

        public PropertiesController(IPropertyAppService propertyAppService)
        {
            _propertyAppService = propertyAppService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 6)
        {
            var properties = await _propertyAppService.GetAllAsync(pageNumber, pageSize);
            return Ok(properties);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var property = await _propertyAppService.GetByIdAsync(id);
            if (property == null) return NotFound();
            return Ok(property);
        }

        [HttpPost]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] CreatePropertyDto createPropertyDto)
        {
            var newProperty = await _propertyAppService.CreateAsync(createPropertyDto);
            return CreatedAtAction(nameof(GetById), new { id = newProperty.Id }, newProperty);
        }

        [HttpPut("{id}")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update(string id, [FromForm] UpdatePropertyDto updatePropertyDto)
        {
            var result = await _propertyAppService.UpdateAsync(id, updatePropertyDto);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _propertyAppService.DeleteAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(
            [FromQuery] PropertySearchParams searchParams,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 6)
        {
            var properties = await _propertyAppService.SearchByParams(searchParams, pageNumber, pageSize);
            return Ok(properties);
        }
    }
}
