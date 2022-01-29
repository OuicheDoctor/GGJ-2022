using GGJ.Characters;
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
}