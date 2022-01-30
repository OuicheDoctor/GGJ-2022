using GGJ.Characters;
using GGJ.Matchmaking;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Région Désertée", menuName = "GGJ/Event/Event - Région Désertée")]
public class RegionDeserteeEventData : WorldEventData
{
    public string DesertedRegion { get; private set; }

    public override WorldEventType Type => WorldEventType.RegionDesertee;

    public override void FixGeneration(List<Character> characters)
    {
        var mostRepresentedRegion = characters.Select(c => (c.Region, Count: characters.Count(cB => cB.Region == c.Region))).OrderByDescending(r => r.Count).Distinct().FirstOrDefault();
        DesertedRegion = mostRepresentedRegion.Region;
        for (var i = 0; i < (4 - mostRepresentedRegion.Count); i++)
        {
            characters.Where(c => c.Region != DesertedRegion)
                .PickOne()
                .ChangeRegion(DesertedRegion);
        }
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        if (mateA.Region == mateB.Region && mateA.Region == DesertedRegion)
            return rating.Scoring + bonus.BigBonus;

        return rating.Scoring;
    }
}