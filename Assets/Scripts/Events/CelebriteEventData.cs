using GGJ.Characters;
using GGJ.Races;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Event - Lutte Des Races", menuName = "GGJ/Event/Event - Lutte Des Races")]
public class CelebriteEventData : WorldEventData
{
    public Character Celebrity { get; set; }

    public override WorldEventType Type => WorldEventType.Celebrite;

    public override void FixGeneration(List<Character> characters)
    {
        var matchMaker = MatchmakingManager.Instance;
        var bestClassfication = matchMaker.Settings.BestClassification;
        foreach (var charA in characters)
        {
            foreach (var charB in characters)
            {
                if (matchMaker.Match(charA, charB).ClassificationName == bestClassfication.ClassificationName)
                {
                    // Already a perfect match, set one member of the couple as a celebrity;
                    Celebrity = Random.Range(0, 2) > 0 ? charA : charB;
                    return;
                }
            }
        }

        // No perfect match for now, pick random char as Celibrity
        Celebrity = characters.PickOne();
        // pick random mate
        var mate = characters.Where(c => c != Celebrity).PickOne();
        mate.ForgeMatchingFor(Celebrity, bestClassfication);
    }
}