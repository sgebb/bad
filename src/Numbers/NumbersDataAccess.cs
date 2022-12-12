using Bad.Database;

namespace Bad.Numbers;

public class NumbersDataAccess : INumbersDataAccess
{
    private readonly BadDbContext _context;

    public NumbersDataAccess(BadDbContext context)
    {
        _context = context;
    }

    public NumberEntity? GetNumber(int id)
    {
        return _context.Numbers.FirstOrDefault(n => n.Id == id);
    }

    public IAsyncEnumerable<NumberEntity> GetNumbersAsAsyncEnumerable()
    {
        return _context.Numbers.AsAsyncEnumerable();
    }

    public NumberEntity StoreNumber(int value)
    {
        var entity = new NumberEntity(value);
        _context.Numbers.Add(entity);
        _context.SaveChanges();

        return entity;
    }
}
