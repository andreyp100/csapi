using csapi.Models;
using csapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace csapi.Controllers;

[ApiController]
[Route("[controller]")]
public class EntryController : ControllerBase
{

  public EntryController()
  {    
  }

  [HttpGet]
  public ActionResult<List<Entry>> GetAll() =>
    EntryService.GetAll();

  [HttpGet("{id}")]
  public ActionResult<Entry> Get(int id)
  {
    var entry = EntryService.Get(id);

    if (entry == null)
      return NotFound();

    return entry;
  }

  [HttpPost]
  public IActionResult Create(Entry entry)
  {
    EntryService.Add(entry);
    return CreatedAtAction(nameof(Get), new { id = entry.Id, name = entry.Name, sum = entry.Sum, Date = DateTime.Now }, entry);
  }

  [HttpPut("{id}")]
  public IActionResult Update(int id, Entry entry)
  {
    if (id != entry.Id)
      return BadRequest();

    var existingEntry = EntryService.Get(id);
    if (existingEntry is null)
      return NotFound();

    EntryService.Update(entry);

    return NoContent();
  }

  [HttpDelete("{id}")]
  public IActionResult Delete(int id)
  {
    var entry = EntryService.Get(id);
    if (entry is null)
      return NotFound();

    EntryService.Delete(id);
    return NoContent();
  }


}
