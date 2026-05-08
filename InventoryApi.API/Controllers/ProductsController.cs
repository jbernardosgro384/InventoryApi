using AutoMapper;
using InventoryApi.Application.Interfaces;
using InventoryApi.Domain.Entities;
using InventoryApi.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryApi.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _service;
    private readonly IMapper _mapper;

    public ProductsController(
        IProductService service,
        IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var product = await _service.GetByIdAsync(id);

        if (product == null)
            return NotFound();

        return Ok(product);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAllAsync();

        var response = _mapper.Map<IEnumerable<ProductDto>>(products);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);

        var created = await _service.CreateAsync(product);

        var response = _mapper.Map<ProductDto>(created);

        return CreatedAtAction(nameof(GetAll),
            new { id = response.IdProduct },
            response);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Product product)
    {
        var updated = await _service.UpdateAsync(id, product);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}
