using csapi.Models;
using csapi.Services;
using System.Linq;
using csapi.ErrorExceptions;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using NpgsqlTypes;
using System.Globalization;

public class CategoryService : ICategoryService
{
  private readonly AppDbContext _context;

  public CategoryService(AppDbContext context)
  {
    _context = context;
  }

  public List<CategoryDTO> ConvertToDTOs(List<Category> categories)
  {
    List<CategoryDTO> convertedList = categories.Select(category =>
    {

      

      return new CategoryDTO
      {
        Id = category.Id,
        Name = category.Name,
        IsPrimary = category.IsPrimary,
        Limit = category.Limit,
        EntriesCount = _context.Entries.Count(e => e.CategoryId == category.Id),
        CurrentSpent = GetCurrentCategorySpent(category),
        CategoryMonth = new CategoryMonth
        {
          MonthNumber = category.CategoryMonth,
          Year = category.CategoryYear
        }
      };
    }).ToList();

    return convertedList;
  }

  public Task<List<CategoryDTO>> GetAllCategoriesAsync()
  {
    return Task.FromResult(ConvertToDTOs(_context.Categories
                                                    .OrderByDescending(c => c.IsPrimary)
                                                    .ThenByDescending(c => c.Limit)
                                                    .ToList()));
  }

  public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto)
  {

    Category category;

    category = _context.Categories.FirstOrDefault(c => c.Name == categoryDto.Name);

    if (category != null)
    {
      throw new AlreadyExistsError($"Category name '{categoryDto.Name}' already exists");
    }

    int startMonthNumber = categoryDto.CategoryMonth.MonthNumber == 0 ? 12 : categoryDto.CategoryMonth.MonthNumber + 1;
    int endMonthNumber = startMonthNumber == 12 ? 1 : startMonthNumber + 1;
    int startYearNumber = startMonthNumber == 12 ? categoryDto.CategoryMonth.Year - 1 : categoryDto.CategoryMonth.Year;
    int endYearNumber = categoryDto.CategoryMonth.Year;

    category = new()
    {
      Name = categoryDto.Name,
      IsPrimary = categoryDto.IsPrimary,
      Limit = categoryDto.Limit,
      CategoryMonth = categoryDto.CategoryMonth.MonthNumber + 1,
      CategoryYear = categoryDto.CategoryMonth.Year,
      CategoryStartDate = new DateTime(startYearNumber,startMonthNumber, 19).ToUniversalTime(),
      CategoryEndDate = new DateTime(endYearNumber, endMonthNumber, 18, 23, 59, 59).ToUniversalTime()
    };

    _context.Categories.Add(category);
    await _context.SaveChangesAsync();

    categoryDto.Id = category.Id;
    categoryDto.CurrentSpent = 0;


    return categoryDto;
  }

  public float GetCurrentCategorySpent(Category category)
  {
    float currentSpent = _context.Entries.Where(
      e => e.CategoryId == category.Id
      &&
      e.Date < category.CategoryEndDate && e.Date > category.CategoryStartDate
      ).Sum(e => e.Sum);
    return currentSpent;
  }

  public async Task<CategoryDTO> EditCategoryAsync(CategoryDTO categoryDTO)
  {
    Category category = _context.Categories.FirstOrDefault(c => c.Id == categoryDTO.Id);


    if (category == null)
    {
      throw new AlreadyExistsError($"Category does not exist");
    }


    category.Name = categoryDTO.Name;
    category.IsPrimary = categoryDTO.IsPrimary;
    category.Limit = categoryDTO.Limit;

    _context.SaveChanges();

    return new CategoryDTO()
    {
      Name = category.Name,
      IsPrimary = category.IsPrimary,
      Limit = category.Limit,
      Id = category.Id,
      CurrentSpent = GetCurrentCategorySpent(category)
    };
  }

  public async Task<Category?> GetCategoryById(int id)
  {
    return _context.Categories.FirstOrDefault(c => c.Id == id);
  }

  public async Task<CategoryDTO> DeleteCategoryAsync(int id)
  {
    Category category = await GetCategoryById(id);

    if (category == null)
    {
      throw new AlreadyExistsError($"Category does not exist");
    }

    CategoryDTO categoryDTO = new CategoryDTO()
    {
      Id = category.Id,
      Name = category.Name,
    };

    _context.Categories.Remove(category);
    await _context.SaveChangesAsync();
    return categoryDTO;

  }
}
