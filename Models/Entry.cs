namespace csapi.Models;

public class Entry
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public float Sum { get; set; }
  public DateTime Date { get; set; }
  public string? Category { get; set; }
  // public DateTime DateCreated { get; set; }
}

public class EntryDTO
{
  public string? Name { get; set; }
  public float Sum { get; set; }
  public int Date { get; set; }
  public string? Category { get; set; }
}
