using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Amour Interdit", menuName = "GGJ/Event/Event - Amour Interdit")]
public class AmourInterditEventData : WorldEventData
{
    public override WorldEventType Type => WorldEventType.AmourInterdit;

    public override void FixGeneration(List<Character> characters)
    {
        // Does nothing
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        return -rating.Scoring;
    }
}