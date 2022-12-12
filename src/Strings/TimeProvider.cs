namespace Bad.Strings
{
    public class TimeProvider : ITimeProvider
    {
        public DateTimeOffset Now()
        {
            return DateTimeOffset.Now;
        }
    }
}