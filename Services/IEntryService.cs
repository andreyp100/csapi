using csapi.Models;

public interface IEntryService
{
  Task<Entry> CreateAsync(Entry entry);
  Task<Entry?> GetAsync(int id);
  Task<List<Entry>> GetAllAsync();
}
