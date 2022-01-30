using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Diversité", menuName = "GGJ/Event/Event - Diversité")]
public class DiversiteEventData : WorldEventData
{
    public override WorldEventType Type => WorldEventType.Diversite;

    public override void FixGeneration(List<Character> characters)
    {
        var countByRaces = characters.Select(c => (c.Race, Count: characters.Count(cB => cB.Race == c.Race))).OrderByDescending(r => r.Count).Distinct().Take(2);
        var racesToDouble = countByRaces.Select(cr => cr.Race).ToList();
        foreach (var r in countByRaces)
        {
            if (r.Count < 2)
            {
                var charToChange = characters.Where(c => !racesToDouble.Contains(c.Race)).PickOne();
                charToChange.ChangeRace(r.Race, characters.Where(c => c.Race == r.Race).Select(c => c.Name).ToList());
            }
        }
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        return mateA.Race != mateB.Race ? rating.Scoring + bonus.NormalBonus : rating.Scoring;
    }
}