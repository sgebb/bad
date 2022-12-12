using Bad.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bad.Numbers;

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
    public ActionResult<NumberEntity> GetNumber(int id)
    {
        return Ok(_context
            .Numbers
            .FirstOrDefault(n => n.Id == id));
    }

    [HttpPost]
    public ActionResult<NumberEntity> PostNumber(int value)
    {
        var number = new NumberEntity(value);
        _context.Numbers.Add(number);
        _context.SaveChanges();

        return Created($"[controller]/{number.Id!.Value}", number);
    }
}
