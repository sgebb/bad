using Bad.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bad.Controllers;

[ApiController]
[Route("[controller]")]
public class NumbersController : ControllerBase
{
    private readonly BadDbContext _context;

    public NumbersController(IConfiguration configuration)
    {
        var connectionString = DbHelper.DbConnectionString(configuration);
        var optionsBuilder = new DbContextOptionsBuilder<BadDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        _context = new BadDbContext(optionsBuilder.Options);
    }

    [HttpGet]
    public ActionResult<IAsyncEnumerable<NumberEntity>> GetNumbers()
    {
        return Ok(_context
            .Numbers
            .AsAsyncEnumerable());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NumberEntity>> GetNumber(int id)
    {
        return Ok(await _context
            .Numbers
            .FirstOrDefaultAsync(n => n.Id == id));
    }

    [HttpPost]
    public async Task<ActionResult<NumberEntity>> StoreNumberBetween(int value)
    {
        var number = new NumberEntity(value);
        _context.Numbers.Add(number);
        await _context.SaveChangesAsync();

        return Created($"[controller]/{number.Id!.Value}", number);
    }
}
