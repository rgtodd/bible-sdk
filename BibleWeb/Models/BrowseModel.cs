using BibleCore.Service.Data;

namespace BibleWeb.Models
{
    public class BrowseModel
    {
        public string? Message { get; set; }

        public TextData? TextData { get; set; }

        public string? RangeExpression { get; set; }
    }
}
