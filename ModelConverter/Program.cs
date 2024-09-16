namespace ModelConverter;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        var tasks = args.Select(filePath => Task.Run(() => Converter.Convert(filePath))).ToList();

        await Task.WhenAll(tasks);

        Console.WriteLine("done!");
    }
}