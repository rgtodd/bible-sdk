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

        bookmark = BookmarkFactory.ParseBookmark("1 John 3:1");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.ParseBookmark("43 John 3:1");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.ParseBookmark("John 3:1");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.ParseBookmark("John 3");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.ParseBookmark("John");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        bookmark = BookmarkFactory.ParseBookmark("p :2");
        Console.WriteLine(bookmark.HasValue ? BookmarkFactory.Format(bookmark.Value) : "Failure!");

        var result = MorphGntLexemeParser.LongestCommonSubstrings("abcdabcdabcd", "abcqdabcqdabcrd");
        foreach (var s in result)
        {
            Console.WriteLine(s);
        }
    }
}
//Console.WriteLine(Yaml.GetForms());