// See https://aka.ms/new-console-template for more information
using BibleCore;
using BibleCore.Greek;
using BibleCore.Greek.SblGnt;

using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Hello, World!");

        var text = new Text();
        var lexicon = new Lexicon();

        MorphGntFileParser.Parse(text, lexicon);
        MorphGntLexemeParser.Parse(lexicon);

        LexiconReporter.DumpReferences(lexicon);
    }
}
//Console.WriteLine(Yaml.GetForms());