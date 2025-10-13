using csapi.Models;
namespace csapi.Services;

public static class EntryService
{
  static List<Entry> Entries { get; }
  static int nextId = 3;

  static EntryService()
  {
    Entries = new List<Entry>
    {
      new Entry {Id = 1, Name = "FirstEntry", Sum = 100, Date = DateTime.Now },
      new Entry {Id = 2, Name = "SecondEntry", Sum = 200, Date = DateTime.Now}
    };
  }

  public static List<Entry> GetAll() => Entries;

  public static Entry? Get(int id) => Entries.FirstOrDefault(e => e.Id == id);

  public static void Add(Entry entry)
  {
    entry.Id = nextId++;
    Entries.Add(entry);
  }

  public static void Delete(int id)
  {
    var entry = Get(id);
    if (entry is null)
      return;

    Entries.Remove(entry);
  }

  public static void Update(Entry entry)
  {
    var index = Entries.FindIndex(e => e.Id == entry.Id);
    if (index == -1)
      return;

    Entries[index] = entry;
  }



}
