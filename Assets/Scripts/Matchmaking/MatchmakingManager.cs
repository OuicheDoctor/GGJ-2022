using GGJ.Characters;
using GGJ.Matchmaking;
using UnityEngine;

public class MatchmakingManager : MonoBehaviour
{
    public static MatchmakingManager Instance { get; private set; }

    [SerializeField] private MatchmakingSettings _settings;
    private HobbyMatcher _hobbyMatcher;
    private TraitMatcher _traitMatcher;

    public MatchmakingSettings Settings => _settings;

    public Rating Match(ICharacter characterA, ICharacter characterB)
    {
        var score = 0;

        score += _hobbyMatcher.Matcher(characterA.Hobbies, characterB.Hobbies);
        score += _traitMatcher.Matcher(characterA, characterB);

        return _settings.GetMatchingClassification(score);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        _hobbyMatcher = new HobbyMatcher(_settings.HobbyScoring, _settings.NothingHobbyScoring);
        _traitMatcher = new TraitMatcher(_settings.TraitScoring);
    }
}