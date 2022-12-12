using Bad.Database;

namespace Bad.Numbers;

public interface INumbersDataAccess
{
    NumberEntity StoreNumber(int value);

    NumberEntity? GetNumber(int id);
    IAsyncEnumerable<NumberEntity> GetNumbersAsAsyncEnumerable();
}