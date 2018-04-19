using System;
using System.IO;
using System.Threading.Tasks;
using NJsonSchema;
using CommandLine;

namespace JsonValidator
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            try
            {
                Console.WriteLine($"Raw args: {args.Length}");
                Options options = null;
                CommandLine.Parser.Default.ParseArguments<Options>(args)
                           .WithParsed<Options>(opts => options = opts);
                if (options == null)
                {
                    return 1;
                }
                else
                {
                    await Process(options);
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 1;
            }
        }

        private static async Task Process(Options options)
        {
            var schema = await JsonSchema4.FromFileAsync(options.SchemaPath);
            var sample = await File.ReadAllTextAsync(options.JsonPath);
            var errors = schema.Validate(sample);
            Console.WriteLine($"There are {errors.Count} errors");
            foreach (var error in errors)
            {
                Console.WriteLine($"{error}: property={error.Property}, location=({error.LineNumber},{error.LinePosition})");
            }
        }
    }
}
