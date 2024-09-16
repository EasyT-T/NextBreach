namespace ModelConverter;

class Program
{
    static void Main(string[] args)
    {
        var path = args.Length == 0 ? Console.ReadLine() : args[0];

        Converter.Convert(path ?? string.Empty);
    }
}