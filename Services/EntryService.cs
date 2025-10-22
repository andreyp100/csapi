using System.Threading.Tasks;
using csapi.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
namespace csapi.Services;

public class EntryService : IEntryService
{
  // static List<Entry> Entries { get; }
  // static int nextId = 3;

  private readonly AppDbContext _context;

  public List<EntryDTO> ConvertToDTOs(List<Entry> entries)
  {
    List<EntryDTO> convertedList = entries.Select(entry =>
    new EntryDTO
    {
      Name = entry.Name,
      Sum = entry.Sum,
      Date = new DateTimeOffset(entry.Date.ToUniversalTime()).ToUnixTimeMilliseconds(),
      CategoryName = _context.Categories.FirstOrDefault(c => c.Id == entry.CategoryId).Name
    }).ToList();

    return convertedList;
  }

  public EntryService(AppDbContext context)
  {
    _context = context;

  }

  public Task<List<EntryDTO>> GetAllAsync()
  {
    return Task.FromResult(ConvertToDTOs(_context.Entries.ToList()));
  }

  public Task<Entry?> GetAsync(int id) => Task.FromResult(_context.Entries.FirstOrDefault(e => e.Id == id));
 

  public async Task<EntryDTO> CreateAsync(EntryDTO entrydto)
  {

    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(entrydto.Date);
    DateTime localDateTime = dateTimeOffset.UtcDateTime;

    var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == entrydto.CategoryName);

    if (category == null)
    {
      throw new ArgumentException("category not found");
    }

    Entry entry = new()
    {
      Name = entrydto.Name,
      Sum = entrydto.Sum,
      Date = localDateTime,
      CategoryId = category.Id,
      DateCreated = DateTime.UtcNow
    };

    _context.Entries.Add(entry);
    await _context.SaveChangesAsync();
    return new EntryDTO()
    {
      Name = entry.Name,
      Sum = entry.Sum,
      Date = entrydto.Date,
CategoryName = entrydto.CategoryName

    };
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
