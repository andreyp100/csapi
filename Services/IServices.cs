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
  Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto);
  Task<List<CategoryDTO>> GetAllCategoriesAsync();
  Task<CategoryDTO> EditCategoryAsync(CategoryDTO categoryDto);
  Task<CategoryDTO> DeleteCategoryAsync(int id);
}

public interface IUserService
{
  Task<UserDTO> CreateUserAsync(UserDTO user);
}
