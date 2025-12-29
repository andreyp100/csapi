using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using csapi.Models;
using NpgsqlTypes;
using NodaTime;

public class Category
{
  public int Id { get; set; }
  public string Name { get; set; }
  public bool IsPrimary { get; set; }
  public int Limit { get; set; }
  public ICollection<Entry> Entries { get; set; }
  public int CategoryMonth {get; set;}
  public int CategoryYear {get; set;}
  public DateTime CategoryStartDate {get; set;}
  public DateTime CategoryEndDate {get; set; }
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
  public CategoryMonth CategoryMonth {get; set;}
}

public class CategoryMonth
{
  public int MonthNumber {get; set;}
  public int Year {get; set;}
}
