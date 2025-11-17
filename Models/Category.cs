using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using csapi.Models;

public class Category
{
  public int Id { get; set; }
  public string Name { get; set; }
  public bool IsPrimary { get; set; }
  public int Limit { get; set; }
  public ICollection<Entry> Entries { get; set; }
}

public class CategoryDTO
{
  public int Id {get; set;}
  public string Name { get; set; }
  public bool IsPrimary { get; set; }
  public int Limit { get; set; }
  public int EntriesCount { get; set; }
  public float CurrentSpent {get; set;}
  public float Left => Limit - CurrentSpent;
}
