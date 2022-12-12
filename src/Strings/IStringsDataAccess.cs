using Bad.Database;

namespace Bad.Strings
{
    public interface IStringsDataAccess
    {
        StringEntity AddString(string value);
        IAsyncEnumerable<StringEntity> GetAllStrings();
        StringEntity? GetString(int id);
    }
}