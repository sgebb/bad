using Bad.Database;
using Microsoft.AspNetCore.Mvc;

namespace Bad.Strings;

[ApiController]
[Route("[controller]")]
public class StringsController : ControllerBase
{
    private readonly IStringsDomain _badDomain;
    public StringsController(IStringsDomain badDomain)
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
            return Unauthorized();
        }

        return Created($"/{storedString.Id}", storedString);

    }
}