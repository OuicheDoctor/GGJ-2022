using GGJ.Characters;
using GGJ.Core;
using GGJ.Matchmaking;
using System.Collections.Generic;
using System.Linq;

public class BestTenPairMatching : Singleton<BestTenPairMatching>
{
    public PartenerCollection Process(IList<ICharacter> characters, WorldEventData currentEvent)
    {
        Dictionary<(ICharacter, ICharacter), int> scorings = new Dictionary<(ICharacter, ICharacter), int>();
        Dictionary<(ICharacter, ICharacter), Rating> matches = new Dictionary<(ICharacter, ICharacter), Rating>();
        int scoring;
        Rating match;
        var matchMgr = MatchmakingManager.Instance;
        foreach (var characA in characters)
        {
            foreach (var characB in characters)
            {
                match = matchMgr.Match(characA, characB);
                scoring = match.Scoring;
                scoring = currentEvent.ImpactOnScore(characA, characB, match, matchMgr.Settings.Bonus);
                matches.Add((characA, characB), match);
                scorings.Add((characA, characB), scoring);
            }
        }

        Dictionary<(ICharacter, ICharacter), int> availableMatches = scorings
            .OrderByDescending(m => m.Value)
            .ToDictionary(m => m.Key, m => m.Value);
        int score = 0;
        List<(ICharacter, ICharacter)> keepedMatches;
        Dictionary<List<(ICharacter, ICharacter)>, int> bestMatches = new Dictionary<List<(ICharacter, ICharacter)>, int>();
        foreach (var ma in availableMatches)
        {
            score = ma.Value;
            keepedMatches = new List<(ICharacter, ICharacter)>() { ma.Key };

            foreach (var other in availableMatches)
            {
                if (keepedMatches.Any(m => (m.Item1 == other.Key.Item1 || m.Item2 == other.Key.Item2)
                    || (m.Item2 == other.Key.Item1 || m.Item1 == other.Key.Item2)))
                    continue;

                keepedMatches.Add(other.Key);
                score += other.Value;

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