using GGJ.Characters;
using GGJ.Matchmaking;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Criminalité", menuName = "GGJ/Event/Event - Criminalité")]
public class CriminaliteEventData : WorldEventData
{
    public override WorldEventType Type => WorldEventType.Criminalite;

    public override void FixGeneration(List<Character> characters)
    {
        var matchMgr = MatchmakingManager.Instance;
        Rating currentRating;
        foreach (var charA in characters)
        {
            foreach (var charB in characters)
            {
                currentRating = matchMgr.Match(charA, charB);
                if (currentRating.Classification == Classification.Kill)
                    return;
            }
        }

        var toKillA = characters.PickOne();
        var toKillB = characters.Where(c => c != toKillA).PickOne();
        toKillB.ForgeMatchingFor(toKillA, Classification.Kill);
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        return rating.Classification == Classification.Kill ? rating.Scoring + bonus.BigMalus : rating.Scoring;
    }
}