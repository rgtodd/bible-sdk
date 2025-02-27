using System.Text;

namespace BibleCore.Greek.Study
{
    internal class DefinitionExerciseFactory() : IExerciseFactory
    {
        private static readonly int AnswerCount = 3;

        public string Name => "Definitions";

        public Exercise CreateExercise(WordList wordList)
        {
            var lexemes = wordList.Lexemes;
            Random.Shared.Shuffle(lexemes);

            var allGlosses = lexemes.Select(l => l.Gloss).ToArray();

            var questions = new List<Question>();
            foreach (var lexeme in lexemes)
            {
                var correctGloss = lexeme.Gloss;

                Random.Shared.Shuffle(allGlosses);
                var possibleGlosses = allGlosses.Where(g => g != correctGloss).Take(AnswerCount - 1).Union([correctGloss]).ToArray();
                Random.Shared.Shuffle(possibleGlosses);

                var answers = possibleGlosses.Select(g => new Answer(g, g == correctGloss)).ToArray();

                var fullCitationForm = lexeme.FullCitationForm;
                if (!string.IsNullOrEmpty(lexeme.Verbs))
                {
                    fullCitationForm += ", " + lexeme.Verbs;
                }
                else
                {
                    fullCitationForm = lexeme.PartOfSpeech.ToString() + ": " + fullCitationForm;
                }

                var detail = new string[] {
                    fullCitationForm,
                    lexeme.Root,
                    Concatenate(lexeme.MounceMorphcat, GetMorphcatDescription(lexeme.MounceMorphcat)) };

                var question = new Question(lexeme.Lemma, detail, answers, First(lexeme.StrongsNumber), First(lexeme.GkNumber));
                questions.Add(question);
            }

            return new Exercise(Name, wordList.Description, wordList.WordListId, wordList.Range, [.. questions]);
        }

        private static string Concatenate(params string[] values)
        {
            var result = new StringBuilder();

            var prefix = string.Empty;
            foreach (var value in values)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    result.Append(prefix);
                    prefix = " - ";

                    result.Append(value);
                }
            }

            return result.ToString();
        }

        private string GetMorphcatDescription(string morphcat)
        {
            bool isCompound = false;
            if (morphcat.StartsWith('c'))
            {
                morphcat = morphcat[1..];
                isCompound = true;
            }

            string description = morphcat switch
            {
                "v-1a(4)" => "Present = Root / Ending in consonantal iota",
                "v-1a(6)" => "Present = Root / Ending in ευ",
                "v-1a(8)" => "Present = Root / Ending in ου",
                "v-1b(1)" => "Present = Root / Ending in labial (π, β, φ)",
                "v-1b(2)" => "Present = Root / Ending in velar (κ, γ, χ)",
                "v-1b(3)" => "Present = Root / Ending in dental (τ, δ, θ)",
                "v-1b(4)" => "Present = Root / Ending in stop (add ε)",
                "v-1c(1)" => "Present = Root / Εnding in ρ",
                "v-1c(2)" => "Present = Root / Ending in μ or ν",
                "v-1d(1a)" => "Present = Root / α lengthens",
                "v-1d(2a)" => "Present = Root / ε lengthens",
                "v-1d(2b)" => "Present = Root / ε does not lengthen",
                "v-1d(2c)" => "Present = Root / ε drops in present tense",
                "v-1d(3)" => "Present = Root / Ending in ο",
                "v-2a(1)" => "Present = Root + ι / ι -> ζς",
                "v-2a(2)" => "Present = Root + ι / ι -> ζς",
                "v-2b" => "Present = Root + ι / ι -> σσω",
                "v-2d(1)" => "Present = Root + ι / λ",
                "v-2d(2)" => "Present = Root + ι / αρ",
                "v-2d(3)" => "Present = Root + ι / ερ",
                "v-2d(5)" => "Present = Root + ι / εν",
                "v-2d(6)" => "Present = Root + ι / ?",
                "v-2d(7)" => "Present = Root + ι / ?",
                "v-3a(1)" => "Present = Root + ν / Root ends in vowel, add ν",
                "v-3a(2a)" => "Present = Root + ν / Root ends in consonant, add αν",
                "v-3a(2b)" => "Present = Root + ν / Root ends in consonant, αν (epenthetic)",
                "v-3b" => "Present = Root + ν / Add νε",
                "v-3c(2)" => "Present = Root + ν / Root ends in vowel, add νυ",
                "v-5a" => "Present = Root + σκ / Root ends in vowel, add σκ",
                "v-5b" => "Present = Root + σκ / Root ends in consonant, add ισκ",
                "v-6a" => "Athematic / ",
                "v-6b" => "Athematic / ",
                "v-8" => "Multiple Roots / ",
                _ => "",
            };

            if (isCompound)
            {
                description = description + " (Compound)";
            }

            return description;
        }

        private int? First(int[] values) => values.Length > 0 ? values[0] : null;
    }
}
