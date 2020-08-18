using System;

namespace PackingLibrary
{
    public static class Main
    {
        public static bool StartsWithUpper(this String str)
        {
            Console.WriteLine("Checking if starts with upper letter...");

            if (String.IsNullOrWhiteSpace(str))
                return false;

            Char ch = str[0];
            return Char.IsUpper(ch);

        }
    }
}
