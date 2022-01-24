using System.Collections;
using System.Collections.Generic;
using GGJ.Characters;
using GGJ.Matchmaking;
using UnityEngine;

public class MatchmakingManager : MonoBehaviour
{
    public static MatchmakingManager Instance { get; private set; }

    [SerializeField] private HobbyMatcherSettings _hobbyMatcherSettings;
    [SerializeField] private TraitMatcherSettings _traitMatcherSettings;
    private HobbyMatcher _hobbyMatcher;
    private TraitMatcher _traitMatcher;


    public int Match(ICharacter characterA, ICharacter characterB)
    {
        var score = 0;

        score += _hobbyMatcher.Matcher(characterA.Hobbies, characterB.Hobbies);
        score += _traitMatcher.Matcher(characterA, characterB);

        return score;
    }


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _hobbyMatcher = new HobbyMatcher(_hobbyMatcherSettings);
        _traitMatcher = new TraitMatcher(_traitMatcherSettings);
    }
}
