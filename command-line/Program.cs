using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace command_line
{
    class Program
    {
        public static int Main(string[] args)
        {
            var rootCommand = new RootCommand
            {
                Name = "learn-cli",
                Description = "Learning System.CommandLine arg parsing"
            };
            
            // General 'multiplier' available to all sub commands
            rootCommand.AddGlobalOption(new Option<int>("--multiplier", () => 1, "General multiplier (defaults to 1)"));
            
            // subcommand will parse a number string out print to output
            var parseCommand = new Command("parse", "Parsing operation")
            {
                Handler = CommandHandler.Create<string, int>(Parse),
            };
            rootCommand.AddCommand(parseCommand);
            // 'parse' requires a single argument that is a string
            parseCommand.AddArgument(new Argument("parseArg")
                {
                    Arity = ArgumentArity.ExactlyOne
                }
            );

            return rootCommand.Invoke(args);
        }

        private static void Parse(string parseArg, int multiplier)
        {
            var i = parseArg switch
            {
                "zero" => 0,
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                _ => throw new ArgumentOutOfRangeException("parse", $"Cannot parse '{parseArg}'")
            };
            Console.WriteLine(i * multiplier);
        }
    }
}