using GGJ.Characters;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Lutte Des Races", menuName = "GGJ/Event/Event - Lutte Des Races")]
public class DiversiteEventData : WorldEventData
{
    public override WorldEventType Type => WorldEventType.Diversite;

    public override void FixGeneration(List<Character> characters)
    {
        // TODO
    }
}