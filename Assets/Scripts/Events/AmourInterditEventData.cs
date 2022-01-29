using GGJ.Characters;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Lutte Des Races", menuName = "GGJ/Event/Event - Lutte Des Races")]
public class AmourInterditEventData : WorldEventData
{
    public override WorldEventType Type => WorldEventType.AmourInterdit;

    public override void FixGeneration(List<Character> characters)
    {
        // Does nothing
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, int initialScoring)
    {
        throw new System.NotImplementedException();
    }
}