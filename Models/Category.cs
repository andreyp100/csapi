public class Category
{
  public int Id { get; set; }
  public string Name { get; set; }
  public bool IsPrimary { get; set; }
}

public class CategoryDTO
{
  public string Name { get; set; }
  public bool IsPrimary { get; set; }
}
