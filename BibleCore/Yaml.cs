//using BibleCore.Properties;

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//using YamlDotNet.Serialization;

//namespace BibleCore
//{
//    public static class Yaml
//    {
//        public static string GetForms()
//        {
//            var forms = Resources.forms;

//            var deserializer = new DeserializerBuilder()
//                //.IgnoreUnmatchedProperties()
//                .Build();

//            var yaml = deserializer.Deserialize(Resources.forms);
//            //          var yaml = deserializer.Deserialize<Dictionary<string, LexemeEntry>>(Resources.forms);

//            DumpYaml(yaml);
//            return "";
//        }

//        public static string GetLexemes()
//        {
//            return Resources.lexemes;
//        }
//        private static void DumpYaml(object yaml)
//        {
//            //var d = yaml as Dictionary<object, object>;
//            DumpLexemeYaml(yaml);
//        }

//        private static void DumpLexemeYaml(object yaml)
//        {
//            var dictionary = (Dictionary<object, object>)yaml;

//            var paths = new HashSet<string>();
//            foreach (var lexeme in dictionary.Keys)
//            {
//                paths.UnionWith(GetPaths("", dictionary[lexeme]));
//            }

//            var sortedPaths = new List<String>(paths);
//            sortedPaths.Sort();
//            foreach (var path in sortedPaths)
//            {
//                Console.WriteLine(path);
//            }
//        }


//        private static HashSet<string> GetPaths(string path, object o)
//        {
//            switch (o)
//            {
//                case Dictionary<object, object> dictionary:
//                    {
//                        var result = new HashSet<string>();

//                        foreach (var key in dictionary.Keys)
//                        {
//                            result.UnionWith(GetPaths(path + "/" + key, dictionary[key]));
//                        }

//                        return result;
//                    }

//                case List<object> list:
//                    {
//                        var result = new HashSet<string>();

//                        foreach (var entry in list)
//                        {
//                            result.UnionWith(GetPaths(path, entry));
//                        }

//                        return result;
//                    }

//                default:
//                    {
//                        var result = new HashSet<string>
//                        {
//                            path // + ":" + o.ToString()
//                        };

//                        return result;
//                    }
//            }
//        }

//        private static void DumpObjectList(List<object> list)
//        {

//        }

//        private static void DumpObjectDictionary(Dictionary<object, object> dictionary)
//        {
//            foreach (var key in dictionary.Keys)
//            {
//                Console.WriteLine(key);
//                var value = dictionary[key];
//                if (value is string)
//                {
//                    Console.WriteLine(value);
//                }
//                else if (value is List<object>)
//                {
//                    List<object> values = value as List<object>;
//                    foreach (var v in values)
//                    {
//                        Console.WriteLine(v);
//                    }
//                }
//                else
//                {
//                    var v = value as Dictionary<object, object>;
//                    DumpObjectDictionary(v);
//                }
//            }
//        }
//    }

//    public class LexemeForm
//    {
//        public string Path { get; set; }

//        public string Form { get; set; }
//    }

//    public class LexemeEntryX
//    {
//        public NounGenderEntry? F { get; set; }
//        public NounGenderEntry? M { get; set; }
//        public NounGenderEntry? N { get; set; }
//    }

//    public class NounGenderEntry
//    {
//        public NounGenderInflectionEntry? AS { get; set; }
//        public NounGenderInflectionEntry? DS { get; set; }
//        public NounGenderInflectionEntry? GS { get; set; }
//        public NounGenderInflectionEntry? NS { get; set; }
//        public NounGenderInflectionEntry? VS { get; set; }
//        public NounGenderInflectionEntry? AP { get; set; }
//        public NounGenderInflectionEntry? DP { get; set; }
//        public NounGenderInflectionEntry? GP { get; set; }
//    }

//    public class NounGenderInflectionEntry()
//    {
//        [YamlMember(Alias = "forms")]
//        public FormEntry[] Forms { get; set; } = [];
//    }

//    public class FormEntry()
//    {
//        [YamlMember(Alias = "form")]
//        public string Form { get; set; } = string.Empty;
//    }

//}
