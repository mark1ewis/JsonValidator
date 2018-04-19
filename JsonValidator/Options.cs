using CommandLine;

namespace JsonValidator
{
    public class Options
    {
        [Value(0, HelpText = "Path to the schema to validate with", MetaName="schema path", Required=true)]
        public string SchemaPath { get; set; }

        [Value(1, HelpText = "Path to the JSON file to validate", MetaName="json path", Required = true)]
        public string JsonPath { get; set; }
    }
}
