using BibleCore.Service.Data;

namespace BibleWeb.Models
{
    public class VerbListModel
    {
        public required MoodData Mood { get; init; }
        
        public required TenseData Tense { get; init; }
        
        public required VoiceData Voice { get; init; }

        public required IList<VerbModel> Verbs { get; init; }
    }

}
