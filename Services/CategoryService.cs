using csapi.Models;
using csapi.Services;
using System.Linq;

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
    return Task.FromResult(ConvertToDTOs(_context.Categories.ToList()));
  }

  public async Task<Category> CreateCategoryAsync(CategoryDTO categoryDto)
  {

    Category category = new()
    {
      Name = categoryDto.Name,
      IsPrimary = categoryDto.IsPrimary,
      Limit = categoryDto.Limit
    };

    _context.Categories.Add(category);
    await _context.SaveChangesAsync();
    return category;
  }

  public async Task<CategoryDTO> EditCategoryAsync(string categoryName, CategoryDTO updatedDTO)
  {
    Category category = _context.Categories.FirstOrDefault(c => c.Name == categoryName);

    if (category == null)
    {
      throw new ArgumentException("category not found");
    }

    category.Name = updatedDTO.Name;
    category.IsPrimary = updatedDTO.IsPrimary;
    category.Limit = updatedDTO.Limit;

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
}
