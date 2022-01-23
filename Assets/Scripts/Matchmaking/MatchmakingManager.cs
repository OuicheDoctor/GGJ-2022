using System.Collections;
using System.Collections.Generic;
using GGJ.Characters;
using GGJ.Matchmaking;
using UnityEngine;

public class MatchmakingManager : MonoBehaviour
{
    public static MatchmakingManager Instance { get; private set; }

    [SerializeField] private HobbyMatcherSettings _hobbyMatcheSettings;
    private HobbyMatcher _hobbyMatcher;


    public int Match(ICharacter characterA, ICharacter characterB)
    {
        var score = 0;

        score += _hobbyMatcher.Matcher(characterA.Hobbies, characterB.Hobbies);

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
        _hobbyMatcher = new HobbyMatcher(_hobbyMatcheSettings);       
    }
}
