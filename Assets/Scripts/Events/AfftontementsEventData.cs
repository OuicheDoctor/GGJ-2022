using GGJ.Characters;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Affrontements", menuName = "GGJ/Event/Event - Affrontements")]
public class AffrontementsEventData : WorldEventData
{
    public override WorldEventType Type => WorldEventType.Affrontements;

    public override void FixGeneration(List<Character> characters)
    {
        // TODO WHEN REGION READY
    }
}