namespace WebService.Infrastructure
{
    public static class Extensions
    {
        public static bool IsEqualIgnoreCasing(this string source, string toCheck)
        {
            return string.Equals(source, toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ContainsIgnoreCasing(this string source, string toCheck)
        {
            return source.Contains(toCheck, StringComparison.OrdinalIgnoreCase);
        }

        public static bool ContainsIgnoreCasing(this IEnumerable<String> source, string toCheck)
        {
            return source.Contains(toCheck, StringComparer.OrdinalIgnoreCase);
        }

    }

}
