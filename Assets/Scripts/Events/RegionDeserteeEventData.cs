using GGJ.Characters;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Lutte Des Races", menuName = "GGJ/Event/Event - Lutte Des Races")]
public class RegionDeserteeEventData : WorldEventData
{
    public override WorldEventType Type => WorldEventType.RegionDesertee;

    public override void FixGeneration(List<Character> characters)
    {
        // TODO
    }
}