using Bad.Database;
using Bad.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bad.Controllers;

[ApiController]
[Route("[controller]")]
public class StringsController : ControllerBase
{
    private readonly BadDomain _badDomain;
    public StringsController(BadDomain badDomain)
    {
        _badDomain = badDomain;
    }

    [HttpGet("{id}")]
    public ActionResult<IAsyncEnumerable<StringDto>> GetStrings(int id)
    {
        return Ok(_badDomain.GetString(id));
    }

    [HttpGet]
    public ActionResult<IAsyncEnumerable<StringDto>> GetStrings()
    {
        return Ok(_badDomain.GetAllStrings());
    }


    [HttpPost]
    public async Task<ActionResult<StringDto>> StoreString(string submittedString)
    {
        var storedString = await _badDomain.AddString(submittedString, User);

        if (storedString == null)
        {
            return BadRequest("Uh not sure why but that was not good enough");
        }

        return Created($"/{storedString.Id}", storedString);

    }
}
