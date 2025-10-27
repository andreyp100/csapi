using csapi.Models;
using Microsoft.Extensions.Localization;

public interface IEntryService
{
  Task<EntryDTO> CreateAsync(EntryDTO entryDTO);
  Task<Entry?> GetAsync(int id);
  Task<List<EntryDTO>> GetAllAsync();
}

public interface ICategoryService
{
  Task<Category> CreateCategoryAsync(CategoryDTO categoryDto);
  Task<List<CategoryDTO>> GetAllCategoriesAsync();
  Task<CategoryDTO> EditCategoryAsync(EditedCategoryDTO editedCategoryDTO);
}
