namespace BibleCore.Service.Data
{
    public static class Verbs
    {
        public static TenseStemData GetTenseStem(TenseData tense, VoiceData voice)
        {
#pragma warning disable IDE0066 // Convert switch statement to expression
            switch (tense)
            {
                case TenseData.Present:
                case TenseData.Imperfect: return TenseStemData.Present;

                case TenseData.Future: return voice == VoiceData.Passive ? TenseStemData.AoristPassive : TenseStemData.FutureActive;

                case TenseData.Aorist: return voice == VoiceData.Passive ? TenseStemData.AoristPassive : TenseStemData.AoristActive;

                case TenseData.Perfect:
                case TenseData.Pluperfect: return voice == VoiceData.Active ? TenseStemData.PerfectActive : TenseStemData.PerfectPassive;

                default: throw new Exception($"Unknown tense {tense}.");
            }
#pragma warning restore IDE0066 // Convert switch statement to expression
        }

        public static string[] GetRoot(string allRoots, TenseStemData tenseStem)
        {
            var parsedRoot = allRoots.Trim();

            // Retrieve prefix of compound roots
            //
            var prefix = string.Empty;
            var idxPlus = parsedRoot.IndexOf('+');
            if (idxPlus >= 0)
            {
                prefix = parsedRoot[..(idxPlus + 1)] + ' ';
                parsedRoot = parsedRoot[(idxPlus + 1)..].TrimStart();
            }

            var suffix = string.Empty;
            var idxSpace = parsedRoot.IndexOf(' ');
            if (idxSpace >= 0)
            {
                suffix = parsedRoot[idxSpace..];
                parsedRoot = parsedRoot[..idxSpace];
            }

#pragma warning disable IDE0066 // Convert switch statement to expression
            var parts = parsedRoot.Split('/');
            if (parts.Length == 1)
            {
                return [prefix + parts[0], suffix];
            }
            else if (parts.Length == 2)
            {
                switch (tenseStem)
                {
                    case TenseStemData.Present: return [prefix + parts[0], suffix];
                    default: return [prefix + parts[1], suffix];
                }
            }
            else if (parts.Length == 6)
            {
                switch (tenseStem)
                {
                    case TenseStemData.Present: return [prefix + parts[0], suffix];
                    case TenseStemData.FutureActive: return [prefix + parts[1], suffix];
                    case TenseStemData.AoristActive: return [prefix + parts[2], suffix];
                    case TenseStemData.PerfectActive: return [prefix + parts[3], suffix];
                    case TenseStemData.PerfectPassive: return [prefix + parts[4], suffix];
                    case TenseStemData.AoristPassive: return [prefix + parts[5], suffix];
                    default: return [allRoots, string.Empty];
                }
            }
            else
            {
                return [allRoots, string.Empty];
            }
#pragma warning restore IDE0066 // Convert switch statement to expression
        }
    }
}
