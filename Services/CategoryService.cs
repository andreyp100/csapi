using csapi.Models;
using csapi.Services;
using System.Linq;

public class CategoryService: ICategoryService
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
      IsPrimary = category.IsPrimary
    }).ToList();

    return convertedList;
  }

  public Task<List<CategoryDTO>> GetAllCategoriesAsync()
  {
    return Task.FromResult(ConvertToDTOs(_context.Categories.ToList()));
  }

  public async Task<Category> CreateCategoryAsync(Category category)
  {
    _context.Categories.Add(category);
    await _context.SaveChangesAsync();
    return category;
  }
}
