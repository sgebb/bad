using Bad.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bad.Controllers;

[ApiController]
[Route("[controller]")]
public class NumbersController : ControllerBase
{
    private readonly BadDbContext _badDbContext;

    public NumbersController(BadDbContext badDbContext)
    {
        _badDbContext = badDbContext;
    }

    [HttpGet]
    public ActionResult<IAsyncEnumerable<NumberEntity>> GetNumbers()
    {
        return Ok(_badDbContext
            .Numbers
            .AsAsyncEnumerable());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NumberEntity>> GetNumber(int id)
    {
        return Ok(await _badDbContext
            .Numbers
            .FirstOrDefaultAsync(n => n.Id == id));
    }

    [HttpPost]
    public async Task<ActionResult<NumberEntity>> PostNumber(int value)
    {
        var number = new NumberEntity(value);
        _badDbContext.Numbers.Add(number);
        await _badDbContext.SaveChangesAsync();

        return Created($"[controller]/{number.Id!.Value}", number);
    }
}
