using System.ComponentModel.DataAnnotations;

namespace csapi.Models;

public class Entry
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public float Sum { get; set; }
  public DateTime Date { get; set; }
  public DateTime DateCreated { get; set; }
  public int CategoryId { get; set; }
  public Category Category { get; set; }
}

public class EntryDTO
{
  public string? Name { get; set; }
  public float Sum { get; set; }
  public long Date { get; set; }
  public string CategoryName { get; set; }
}
