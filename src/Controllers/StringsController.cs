using Bad.Database;
using Bad.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bad.Controllers;

[ApiController]
[Route("[controller]")]
public class StringsController : ControllerBase
{
    private readonly BadDbContext _context;
    public StringsController(BadDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}")]
    public ActionResult<IAsyncEnumerable<StringDto>> GetStrings(int id)
    {
        var domain = new BadDomain(_context);
        return Ok(domain.GetString(id));
    }

    [HttpGet]
    public ActionResult<IAsyncEnumerable<StringDto>> GetStrings()
    {
        var domain = new BadDomain(_context);
        return Ok(domain.GetAllStrings());
    }


    [HttpPost]
    public async Task<ActionResult<StringDto>> StoreNumber(string submittedString)
    {
        var domain = new BadDomain(_context);
        var storedString = await domain.AddString(submittedString, User);

        if (storedString == null)
        {
            return BadRequest("Uh not sure why but that was not good enough");
        }

        return Ok(storedString);

    }
}
