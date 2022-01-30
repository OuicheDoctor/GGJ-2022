using GGJ.Characters;
using GGJ.Matchmaking;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Lutte Des Races", menuName = "GGJ/Event/Event - Lutte Des Races")]
public class LutteDesRacesEventData : WorldEventData
{
    public List<RaceData> ConflictedRaces { get; set; }

    public override WorldEventType Type => WorldEventType.LutteDeRaces;

    public override void FixGeneration(List<Character> characters)
    {
        var conflictedRaces = characters.Select(c => (c.Race, Count: characters.Count(cB => cB.Race == c.Race))).OrderByDescending(r => r.Count).Distinct().Take(2);
        ConflictedRaces = conflictedRaces.Select(cr => cr.Race).ToList();
        foreach (var r in conflictedRaces)
        {
            if (r.Count < 2)
            {
                var charToChange = characters.Where(c => !ConflictedRaces.Contains(c.Race)).PickOne();
                charToChange.ChangeRace(r.Race, characters.Where(c => c.Race == r.Race).Select(c => c.Name).ToList());
            }
        }
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        if (mateA.Race != mateB.Race && ConflictedRaces.Contains(mateA.Race) && ConflictedRaces.Contains(mateB.Race))
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