// See https://aka.ms/new-console-template for more information
using BibleCore;
using BibleCore.Greek.SblGnt;

using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.WriteLine("Hello, World!");


        SblGntFileParser.Parse();
    }
}
//Console.WriteLine(Yaml.GetForms());