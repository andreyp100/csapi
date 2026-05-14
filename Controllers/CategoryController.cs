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
    catch (AlreadyExistsError error)
    {
      return Conflict(new ErrorResponse(error));
    }
  }

  [HttpPost("/category/edit")]
  public async Task<IActionResult> EditCategoryAsync([FromBody]CategoryDTO categoryDTO)
  {
    try
    {
      var result = await _categoryService.EditCategoryAsync(categoryDTO);
      return Ok(result);
    }
    catch (AlreadyExistsError error)
    {
       return Conflict(new ErrorResponse(error));
    }
  }

  [HttpDelete("/category/delete/{id}")]
  public async Task<IActionResult> DeleteCategoryAsync(int id)
  {
    try
    {
      var result = await _categoryService.DeleteCategoryAsync(id);
      return Ok(result);
    }
    catch (AlreadyExistsError error)
    {
      Console.WriteLine("error");
      return Conflict(new ErrorResponse(error));
    }
  }
}
