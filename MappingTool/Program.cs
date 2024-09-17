namespace MappingTool;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        Directory.SetCurrentDirectory(AppDomain.CurrentDomain.BaseDirectory);

        try
        {
            if (!File.Exists("./Mappings.txt"))
            {
                Console.WriteLine("[ERROR]Mapping list not found.");
                Console.ReadKey();
                return;
            }

            if (!Directory.Exists("./Opt"))
            {
                Directory.CreateDirectory("./Opt");
            }

            var mappingList = await File.ReadAllLinesAsync("./Mappings.txt");

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

                tasks.Add(Task.Run(() => Converter.Convert(arg, mappingList)));
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