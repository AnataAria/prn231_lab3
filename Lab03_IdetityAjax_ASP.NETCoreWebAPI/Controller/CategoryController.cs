using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTO;

namespace Lab03_IdetityAjax_ASP.NETCoreWebAPI.Controller;
[ApiController]
[Route("/api/v1/categories")]
public class CategoryController (CategoryService categoryService): ControllerBase {
    private readonly CategoryService categoryService = categoryService;

    [HttpGet("/all")]
    public async Task<ActionResult<ResponseEntity<CategoryResponse>>> GetAllCategories()
    {
        var categories = await categoryService.GetAllCategory();
        return StatusCode(categories.StatusCode, categories);
    }
}