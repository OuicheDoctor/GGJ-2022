using GGJ.Characters;
using GGJ.Matchmaking;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Affrontements", menuName = "GGJ/Event/Event - Affrontements")]
public class AffrontementsEventData : WorldEventData
{
    public List<string> ConflictedRegions { get; private set; }

    public override string Headline => string.Format(base.Headline, ConflictedRegions[0], ConflictedRegions[1]);

    public override string RadioFlashInfoSubtitle => string.Format(base.RadioFlashInfoSubtitle, ConflictedRegions[0], ConflictedRegions[1]);

    public override WorldEventType Type => WorldEventType.Affrontements;

    public override void FixGeneration(List<Character> characters)
    {
        var conflictedRegions = characters.Select(c => (c.Region, Count: characters.Count(cB => cB.Region == c.Region))).OrderByDescending(r => r.Count).Distinct().Take(2);
        ConflictedRegions = conflictedRegions.Select(cr => cr.Region).ToList();
        foreach (var r in conflictedRegions)
        {
            if (r.Count < 2)
            {
                var charToChange = characters.Where(c => !ConflictedRegions.Contains(c.Region)).PickOne();
                charToChange.ChangeRegion(r.Region);
            }
        }
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        if (mateA.Region != mateB.Region && ConflictedRegions.Contains(mateA.Region) && ConflictedRegions.Contains(mateB.Region))
        {
            switch (rating.Classification)
            {
                case Classification.Kill: return rating.Scoring + bonus.BigMalus;
                case Classification.NoMatch: return rating.Scoring + bonus.NormalMalus;
                case Classification.Match: return rating.Scoring + bonus.NormalBonus;
                case Classification.Perfect: return rating.Scoring + bonus.BigBonus;
                default: return rating.Scoring;
            }
        }
        else
            return rating.Scoring;
    }
}