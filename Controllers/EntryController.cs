using csapi.Models;
using csapi.Services;
using Microsoft.AspNetCore.Mvc;

namespace csapi.Controllers;

[ApiController]
[Route("[controller]")]
public class EntryController : ControllerBase
{

  private readonly IEntryService _entryService;

  public EntryController(IEntryService entryService)
  {
    _entryService = entryService;
  }


  [HttpGet]
  public Task<List<Entry>> GetAllAsync() =>
    _entryService.GetAllAsync();

  [HttpGet("{id}")]
  public Task<Entry?> GetAsync(int id)
  {
    var entry = _entryService.GetAsync(id);

    // if (entry == null)
    //   return NotFound();

    return entry;
  }

  [HttpPost]
  public async Task<IActionResult> CreateAsync([FromBody] EntryDTO entrydto)
  {

    DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(entrydto.date);
    DateTime localDateTime = dateTimeOffset.UtcDateTime;

    Entry entry = new()
    {
      Name = entrydto.name,
      Sum = entrydto.sum,
      Date = localDateTime,
      Category = entrydto.category,
      // DateCreated = DateTime.UtcNow
    };


    var result = await _entryService.CreateAsync(entry);
    return Ok(result);
  }

  // [HttpPut("{id}")]
  // public IActionResult Update(int id, Entry entry)
  // {
  //   if (id != entry.Id)
  //     return BadRequest();

  //   var existingEntry = _entryService.Get(id);
  //   if (existingEntry is null)
  //     return NotFound();

  //   EntryService.Update(entry);

  //   return NoContent();
  // }

  // [HttpDelete("{id}")]
  // public IActionResult Delete(int id)
  // {
  //   var entry = EntryService.Get(id);
  //   if (entry is null)
  //     return NotFound();

  //   EntryService.Delete(id);
  //   return NoContent();
  // }


}
