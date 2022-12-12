using Bad.Database;
using Bad.Domain.Numbers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bad.Controllers;

[ApiController]
[Route("[controller]")]
public class NumbersController : ControllerBase
{
    private readonly INumbersDataAccess _numbersDataAccess;

    public NumbersController(INumbersDataAccess numbersDataAccess)
    {
        _numbersDataAccess = numbersDataAccess;
    }

    [HttpGet]
    public ActionResult<IAsyncEnumerable<NumberEntity>> GetNumbers()
    {
        return Ok(_numbersDataAccess.GetNumbersAsAsyncEnumerable());
    }

    [HttpGet("{id}")]
    public  ActionResult<NumberEntity> GetNumber(int id)
    {
        return Ok(_numbersDataAccess.GetNumber(id));
    }

    [HttpPost]
    public ActionResult<NumberEntity> PostNumber(int value)
    {
        var created = _numbersDataAccess.StoreNumber(value);

        if (created != null)
        {
            return Created($"[controller]/{created.Id!.Value}", created);
        }
        return BadRequest(); //?
    }
}
