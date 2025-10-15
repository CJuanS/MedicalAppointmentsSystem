namespace patient_management_system.Utils;

public static class ConsoleHelper
{
    public static string ReadString(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write(prompt);
            var val = Console.ReadLine() ?? string.Empty;
            if (!allowEmpty && string.IsNullOrWhiteSpace(val))
            {
                Console.WriteLine("Value cannot be empty.");
                continue;
            }
            return val;
        }
    }

    public static int ReadInt(string prompt, bool allowEmpty = false)
    {
        while (true)
        {
            Console.Write(prompt);
            var s = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(s))
            {
                if (allowEmpty) return int.MinValue;
                Console.WriteLine("Value cannot be empty.");
                continue;
            }

            if (int.TryParse(s, out var v)) return v;
            Console.WriteLine("Invalid integer. Try again.");
        }
    }
}
