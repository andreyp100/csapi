using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using csapi.Models;

public class Category
{
  public int Id { get; set; }
  public string Name { get; set; }
  public bool IsPrimary { get; set; }
  // [ForeignKey("CategoryId")]
  public ICollection<Entry> Entries { get; set; }
}

public class CategoryDTO
{
  public string Name { get; set; }
  public bool IsPrimary { get; set; }
  public int EntriesCount { get; set; }
}
