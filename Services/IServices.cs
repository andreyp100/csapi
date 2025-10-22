using csapi.Models;

public interface IEntryService
{
  Task<Entry> CreateAsync(EntryDTO entryDTO);
  Task<Entry?> GetAsync(int id);
  Task<List<Entry>> GetAllAsync();
}

public interface ICategoryService
{
  Task<Category> CreateCategoryAsync(Category category);
  Task<List<CategoryDTO>> GetAllCategoriesAsync();
}
