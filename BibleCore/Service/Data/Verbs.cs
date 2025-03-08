namespace BibleCore.Service.Data
{
    public static class Verbs
    {
        public static TenseStemData GetTenseStem(TenseData tense, VoiceData voice)
        {
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
        }

        public static string GetRoot(string allRoots, TenseStemData tenseStem)
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
                suffix = parsedRoot[(idxSpace + 1)..];
                parsedRoot = parsedRoot[..idxSpace];
            }

            var parts = parsedRoot.Split('/');
            if (parts.Length == 1)
            {
                return prefix + parts[0];
            }
            else if (parts.Length == 2)
            {
                if (tenseStem == TenseStemData.Present)
                {
                    return prefix + parts[0];
                }
                else
                {
                    return prefix + parts[1];
                }
            }
            else if (parts.Length == 6)
            {
                switch (tenseStem)
                {
                    case TenseStemData.Present: return prefix + parts[0];
                    case TenseStemData.FutureActive: return prefix + parts[1];
                    case TenseStemData.AoristActive: return prefix + parts[2];
                    case TenseStemData.PerfectActive: return prefix + parts[3];
                    case TenseStemData.PerfectPassive: return prefix + parts[4];
                    case TenseStemData.AoristPassive: return prefix + parts[5];
                }
            }

            return allRoots;
        }
    }
}
