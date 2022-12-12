using Bad.Database;
using Microsoft.AspNetCore.Mvc;

namespace Bad.Strings;

[ApiController]
[Route("[controller]")]
public class StringsController : ControllerBase
{
    private readonly StringsDomain _badDomain;
    public StringsController(StringsDomain badDomain)
    {
        _badDomain = badDomain;
    }

    [HttpGet("{id}")]
    public ActionResult<StringEntity> GetString(int id)
    {
        return Ok(_badDomain.GetString(id));
    }

    [HttpGet]
    public ActionResult<IAsyncEnumerable<StringEntity>> GetStrings()
    {
        return Ok(_badDomain.GetAllStrings());
    }


    [HttpPost]
    public ActionResult<StringEntity> StoreString(string submittedString)
    {
        var storedString = _badDomain.AddString(submittedString, User);

        if (storedString == null)
        {
            return BadRequest("Uh not sure why but that was not good enough");
        }

        return Created($"/{storedString.Id}", storedString);

    }
}