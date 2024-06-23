// See https://aka.ms/new-console-template for more information
using BibleCore;
using BibleCore.Greek.SblGnt;

using System.Text;

Console.WriteLine("Hello, World!");

Console.OutputEncoding = Encoding.UTF8;

var p = new SblGntFileParser();
p.Parse();
//Console.WriteLine(Yaml.GetForms());