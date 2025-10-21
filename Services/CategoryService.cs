using csapi.Models;
using csapi.Services;

public class CategoryService: ICategoryService
{
  private readonly AppDbContext _context;

  public CategoryService(AppDbContext context)
  {
    _context = context;
  }

  public Task<List<Category>> GetAllCategoriesAsync()
  {
    return Task.FromResult(_context.Categories.ToList());
  }

  public async Task<Category> CreateCategoryAsync(Category category)
  {
    _context.Categories.Add(category);
    await _context.SaveChangesAsync();
    return category;
  }
}
