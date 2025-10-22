using csapi.Models;
using Microsoft.AspNetCore.Mvc;
namespace csapi.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController: ControllerBase
{
  private readonly ICategoryService _categoryService;

  public CategoryController(ICategoryService categoryService)
  {
    _categoryService = categoryService;
  }

  [HttpGet("/categories/list")]
  public Task<List<CategoryDTO>> GetCategoriesAsync() =>
    _categoryService.GetAllCategoriesAsync();


  [HttpPost("/category/add")]
  public async Task<IActionResult> CreateCategoryAsync([FromBody] CategoryDTO categoryDto)
  {

    var result = await _categoryService.CreateCategoryAsync(categoryDto);
    return Ok(result);
  }
}
