namespace CodeGenerator
{
    public static class StringExtensions
    {
        public static string FirstLetterToLower(this string input)
        {
            if (input.Length <= 0)
            {
                return input;
            }
            else if (input.Length == 1)
            {
                return input.ToLower();
            }

            return string.Concat(input.Substring(0, 1).ToLower(), input.Substring(1));
        }
    }
}
