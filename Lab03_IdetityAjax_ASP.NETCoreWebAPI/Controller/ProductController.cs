using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Service;
using Service.DTO;

namespace Lab03_IdetityAjax_ASP.NETCoreWebAPI.Controller;

[ApiController]
[Route("/api/v1/products")]
public class ProductController(ProductService productService) : ControllerBase
{
    private readonly ProductService productService = productService;
    [HttpGet]
    public async Task<ActionResult<ResponseEntity<IEnumerable<ProductResponse>>>> GetAll()
    {
        var response = await productService.GetAll();
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ResponseEntity<ProductResponse>>> GetById(int id)
    {
        var response = await productService.GetByIdAsync(id);
        if (response.StatusCode == 404)
        {
            return NotFound(response);
        }

        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<ResponseEntity<ProductResponse>>> AddProduct([FromBody] ProductRequest productRequest)
    {
        if (ModelState.IsValid)
        {
            return BadRequest(ResponseEntity<ProductResponse>.BadRequest(string.Join(", ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage))));
        }
        var response = await productService.AddAsync(productRequest);
        if (response.StatusCode == 500)
        {
            return StatusCode(500, response);
        }

        return CreatedAtAction(nameof(GetById), new { id = response.Data.ProductId }, response);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ResponseEntity<ProductResponse>>> UpdateProduct(int id, [FromBody] ProductRequest productRequest)
    {
        if (ModelState.IsValid)
        {
            return BadRequest(ResponseEntity<ProductResponse>.BadRequest(string.Join(", ", ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage))));
        }
        var response = await productService.UpdateAsync(id, productRequest);
        if (response.StatusCode == 404)
        {
            return NotFound(response);
        }

        if (response.StatusCode == 500)
        {
            return StatusCode(500, response);
        }

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ResponseEntity<object>>> DeleteProduct(int id)
    {
        var response = await productService.DeleteAsync(id);
        if (response.StatusCode == 404)
        {
            return NotFound(response);
        }

        if (response.StatusCode == 500)
        {
            return StatusCode(500, response);
        }

        return Ok(response);
    }
}