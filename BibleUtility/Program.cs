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

        //LexiconReporter.DumpReferences(GlobalGreek.Instance.Lexicon);

        Bookmark? bookmark;

        bookmark = BookmarkFactory.Parse("1 John 3:1");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.Parse("43 John 3:1");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.Parse("John 3:1");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.Parse("John 3");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.Parse("John");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.Parse("p :2");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");
    }
}
//Console.WriteLine(Yaml.GetForms());