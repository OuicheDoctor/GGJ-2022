using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Natalité", menuName = "GGJ/Event/Event - Natalité")]
public class NataliteEventData : WorldEventData
{
    public override WorldEventType Type => WorldEventType.Natalite;

    public override void FixGeneration(List<Character> characters)
    {
        // Do nothing
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        if (rating.Classification == Classification.Perfect)
            return rating.Scoring + bonus.BigBonus;

        if (rating.Classification == Classification.Match)
            return rating.Scoring + bonus.NormalBonus;

        return rating.Scoring;
    }
}