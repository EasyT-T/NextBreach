namespace ModelConverter;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

        try
        {
            if (!Directory.Exists("./Opt"))
            {
                Directory.CreateDirectory("./Opt");
            }

            var tasks = new List<Task>();

            foreach (var arg in args)
            {
                if (!File.Exists(arg))
                {
                    Console.WriteLine("[ERROR]File not found: " + arg);
                    Console.ReadKey();
                    continue;
                }

                Console.WriteLine("Running Task: " + arg);

                tasks.Add(Task.Run(() => Converter.Convert(arg)));
            }

            await Task.WhenAll(tasks);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine("Done! Press any key to close the window.");
        Console.ReadLine();
    }
}