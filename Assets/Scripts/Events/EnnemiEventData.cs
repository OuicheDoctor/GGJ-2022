using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Ennemi", menuName = "GGJ/Event/Event - Ennemi")]
public class EnnemiEventData : WorldEventData
{
    public Character Target { get; private set; }

    public override WorldEventType Type => WorldEventType.Ennemi;

    public override void FixGeneration(List<Character> characters)
    {
        var matchMaker = MatchmakingManager.Instance;
        foreach (var charA in characters)
        {
            foreach (var charB in characters)
            {
                if (matchMaker.Match(charA, charB).Classification == Classification.Kill)
                {
                    // Already a Kill match, set one member of the couple as the target;
                    Target = Random.Range(0, 2) > 0 ? charA : charB;
                    return;
                }
            }
        }

        // No kill match for now, pick random char as Target
        Target = characters.PickOne();
        // pick random mate
        var mate = characters.Where(c => c != Target).PickOne();
        mate.ForgeMatchingFor(Target, Classification.Kill);
    }

    public override int ImpactOnScore(ICharacter mateA, ICharacter mateB, Rating rating, Bonus bonus)
    {
        if ((mateA == Target || mateB == Target) && rating.Classification == Classification.Kill)
            return rating.Scoring + bonus.BigBonus;

        return rating.Scoring;
    }
}