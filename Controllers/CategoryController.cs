using csapi.ErrorExceptions;
using csapi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
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
    try
    {
      var result = await _categoryService.CreateCategoryAsync(categoryDto);
      return Created("/category/add", result);
    }
    catch (CategoryError error)
    {
      return Conflict(new ErrorResponse(error));
    }
  }

  [HttpPost("/category/edit")]
  public async Task<IActionResult> EditCategoryAsync([FromBody]EditedCategoryDTO editedCategoryDTO)
  {
    try
    {
      var result = await _categoryService.EditCategoryAsync(editedCategoryDTO);
      return Ok(result);
    }
    catch (CategoryError error)
    {
       return Conflict(new ErrorResponse(error));
    }
  }

  [HttpDelete("/category/delete")]
  public async Task<IActionResult> DeleteCategoryAsync([FromBody] CategoryDTO categoryDTO)
  {
    Console.WriteLine("processing delete");
    try
    {
      var result = await _categoryService.DeleteCategoryAsync(categoryDTO);
      return NoContent();
    }
    catch (CategoryError error)
    {
      Console.WriteLine("error");
      return Conflict(new ErrorResponse(error));
    }
  }
}
