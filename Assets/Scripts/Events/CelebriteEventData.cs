using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Célébrité", menuName = "GGJ/Event/Event - Célébrité")]
public class CelebriteEventData : WorldEventData
{
    public Character Celebrity { get; set; }

    public override string Headline => string.Format(base.Headline, Celebrity.Name);

    public override WorldEventType Type => WorldEventType.Celebrite;

    public override void FixGeneration(List<Character> characters)
    {
        var matchMaker = MatchmakingManager.Instance;
        foreach (var charA in characters)
        {
            foreach (var charB in characters)
            {
                if (matchMaker.Match(charA, charB).Classification != Classification.Perfect)
                {
                    continue;
                }
                // Already a perfect match, set one member of the couple as a celebrity;
                Celebrity = Random.Range(0, 2) > 0 ? charA : charB;
                return;
            }
        }

        // No perfect match for now, pick random char as Celibrity
        Celebrity = characters.PickOne();
        // pick random mate
        var mate = characters.Where(c => c != Celebrity).PickOne();
        mate.ForgeMatchingFor(Celebrity, Classification.Perfect);
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        if (mateA == Celebrity || mateB == Celebrity)
        {
            if (rating.Classification == Classification.Perfect)
                return rating.Scoring + bonus.BigBonus;
            else
                return rating.Scoring + bonus.BigMalus;
        }

        return rating.Scoring;
    }
}