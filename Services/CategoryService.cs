using csapi.Models;
using csapi.Services;
using System.Linq;
using csapi.ErrorExceptions;
using Microsoft.EntityFrameworkCore;

public class CategoryService : ICategoryService
{
  private readonly AppDbContext _context;

  public CategoryService(AppDbContext context)
  {
    _context = context;
  }

  public List<CategoryDTO> ConvertToDTOs(List<Category> categories)
  {
    List<CategoryDTO> convertedList = categories.Select(category => new CategoryDTO
    {
      Name = category.Name,
      IsPrimary = category.IsPrimary,
      Limit = category.Limit
    }).ToList();

    return convertedList;
  }

  public Task<List<CategoryDTO>> GetAllCategoriesAsync()
  {
    return Task.FromResult(ConvertToDTOs(_context.Categories
                                                    .OrderByDescending(c => c.Limit)
                                                    .ToList()));
  }

  public async Task<Category> CreateCategoryAsync(CategoryDTO categoryDto)
  {

    Category category;

    category = _context.Categories.FirstOrDefault(c => c.Name == categoryDto.Name);

    if (category != null)
    {
      throw new CategoryError($"Category name '{categoryDto.Name}' already exists");
    }
  
    category = new()
    {
      Name = categoryDto.Name,
      IsPrimary = categoryDto.IsPrimary,
      Limit = categoryDto.Limit
    };

    _context.Categories.Add(category);
    await _context.SaveChangesAsync();
    return category;
  }

  public async Task<CategoryDTO> EditCategoryAsync(EditedCategoryDTO editedCategoryDTO)
  {
    Category category = _context.Categories.FirstOrDefault(c => c.Name == editedCategoryDTO.OriginalName);


    if (category == null)
    {
      throw new CategoryError($"Category does not exist");
    }


    category.Name = editedCategoryDTO.Name;
    category.IsPrimary = editedCategoryDTO.IsPrimary;
    category.Limit = editedCategoryDTO.Limit;

    _context.SaveChanges();

    return new CategoryDTO()
    {
      Name = category.Name,
      IsPrimary = category.IsPrimary,
      Limit = category.Limit
    };
  }

  public async Task<Category?> GetCategoryById(int id)
  {
    return _context.Categories.FirstOrDefault(c => c.Id == id);
  }

  public async Task<CategoryDTO> DeleteCategoryAsync(CategoryDTO categoryDTO)
  {
    Category category = _context.Categories.FirstOrDefault(c => c.Name == categoryDTO.Name);

    if (category == null)
    {
      throw new CategoryError($"Category does not exist");
    }
    _context.Categories.Remove(category);
    await _context.SaveChangesAsync();
    return categoryDTO;
    
  }
}
