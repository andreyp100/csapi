using System.Threading.Tasks;
using csapi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
namespace csapi.Services;

public class EntryService: IEntryService
{
  // static List<Entry> Entries { get; }
  // static int nextId = 3;

  private readonly AppDbContext _context;

  public EntryService(AppDbContext context)
  {
    _context = context;

  }

  public Task<List<Entry>> GetAllAsync() => Task.FromResult(_context.Entries.ToList());

  public Task<Entry?> GetAsync(int id) => Task.FromResult(_context.Entries.FirstOrDefault(e => e.Id == id));

  public async Task<Entry> CreateAsync(Entry entry)
  {

    _context.Entries.Add(entry);
    await _context.SaveChangesAsync();
    return entry;
  }

  // public void Delete(int id)
  // {
  //   var entry = GetAsync(id);
  //   if (entry is null)
  //     return;

  //   Entries.Remove(entry);
  // }

  // public static void Update(Entry entry)
  // {
  //   var index = Entries.FindIndex(e => e.Id == entry.Id);
  //   if (index == -1)
  //     return;

  //   Entries[index] = entry;
  // }



}
