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


}
