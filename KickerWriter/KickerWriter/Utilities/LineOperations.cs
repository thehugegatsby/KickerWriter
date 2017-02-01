namespace KickerWriter.Utilities
{
    public static class LineOperations
    {
        public static string GetKeyWord(string line)
        {
            if (string.IsNullOrEmpty(line) && !line.Contains(":"))
            {
                return null;
            }
            return line.Split(new char[] {':'}, 2)[0].Trim();
        }

        public static string GetValue(string line)
        {
            if (string.IsNullOrEmpty(line) && !line.Contains(":"))
            {
                return null;
            }
            return line.Split(new char[] { ':' }, 2)[1].Trim();
        }
    }
}
