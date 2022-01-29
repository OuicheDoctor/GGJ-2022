using GGJ.Characters;
using GGJ.Core;
using GGJ.Matchmaking;
using System.Collections.Generic;
using System.Linq;

public class BestTenPairMatching : Singleton<BestTenPairMatching>
{
    public PartenerCollection Process(IList<ICharacter> characters, WorldEventData currentEvent)
    {
        Dictionary<(ICharacter, ICharacter), Rating> matches = new Dictionary<(ICharacter, ICharacter), Rating>();
        foreach (var characA in characters)
        {
            foreach (var characB in characters)
            {
                matches.Add((characA, characB), MatchmakingManager.Instance.Match(characA, characB));
            }
        }

        Dictionary<(ICharacter, ICharacter), Rating> availableMatches = matches
            .OrderByDescending(m => m.Value.Scoring)
            .ToDictionary(m => m.Key, m => m.Value);
        int score = 0;
        List<(ICharacter, ICharacter)> keepedMatches;
        Dictionary<List<(ICharacter, ICharacter)>, int> bestMatches = new Dictionary<List<(ICharacter, ICharacter)>, int>();
        foreach (var match in availableMatches)
        {
            score = match.Value.Scoring;
            keepedMatches = new List<(ICharacter, ICharacter)>() { match.Key };

            foreach (var other in availableMatches)
            {
                if (keepedMatches.Any(m => (m.Item1 == other.Key.Item1 || m.Item2 == other.Key.Item2)
                    || (m.Item2 == other.Key.Item1 || m.Item1 == other.Key.Item2)))
                    continue;

                keepedMatches.Add(other.Key);
                score += other.Value.Scoring;

                if (keepedMatches.SelectMany(m => new List<ICharacter>() { m.Item1, m.Item2 }).Count() >= characters.Count)
                    break;
            }

            bestMatches.Add(keepedMatches, score);

            if (bestMatches.Count >= 10)
                break;
        }

        var pairing = bestMatches.OrderByDescending(b => b.Value).FirstOrDefault().Key;
        PartenerCollection partener = new PartenerCollection(characters.Count);

        var remainingCharacter = new List<ICharacter>(characters);
        foreach (var pair in pairing)
        {
            remainingCharacter.Remove(pair.Item1);
            remainingCharacter.Remove(pair.Item2);
            partener.Add(pair.Item1, pair.Item2, matches[(pair.Item1, pair.Item2)]);
        }

        foreach (var charac in remainingCharacter)
            partener.AddSingle(charac);

        return partener;
    }
}