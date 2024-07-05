// See https://aka.ms/new-console-template for more information
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        //LexiconReporter.DumpReferences(GlobalGreek.Instance.Lexicon);

        //Bookmark? bookmark;

        //bookmark = Bookmark.Parse("1 John 3:1");
        //Console.WriteLine(bookmark.HasValue ? bookmark.Value.Format() : "Failure!");

        //bookmark = Bookmark.Parse("43 John 3:1");
        //Console.WriteLine(bookmark.HasValue ? bookmark.Value.Format() : "Failure!");

        //bookmark = Bookmark.Parse("John 3:1");
        //Console.WriteLine(bookmark.HasValue ? bookmark.Value.Format() : "Failure!");

        //bookmark = Bookmark.Parse("John 3");
        //Console.WriteLine(bookmark.HasValue ? bookmark.Value.Format() : "Failure!");

        //bookmark = Bookmark.Parse("John");
        //Console.WriteLine(bookmark.HasValue ? bookmark.Value.Format() : "Failure!");

        //bookmark = Bookmark.Parse("p :2");
        //Console.WriteLine(bookmark.HasValue ? bookmark.Value.Format() : "Failure!");

        //var result = MorphGntLexemeParser.LongestCommonSubstrings("abcdabcdabcd", "abcqdabcqdabcrd");
        //foreach (var s in result)
        //{
        //    Console.WriteLine(s);
        //}

        //foreach (var c in "ἡγιασμένοις".Normalize(NormalizationForm.FormD))
        //{
        //    Console.WriteLine($"{c}, {CharUnicodeInfo.GetUnicodeCategory(c)}");
        //}

        //foreach (var c in "ἁγίου".Normalize(NormalizationForm.FormD))
        //{
        //    Console.WriteLine($"{c}, {CharUnicodeInfo.GetUnicodeCategory(c)} 0x{(short)c:x}");
        //}
        //foreach (var c in "ἁγίου")
        //{
        //    Console.WriteLine($"{c}, {CharUnicodeInfo.GetUnicodeCategory(c)} 0x{(short)c:x}");
        //}
    }
}
//Console.WriteLine(Yaml.GetForms());