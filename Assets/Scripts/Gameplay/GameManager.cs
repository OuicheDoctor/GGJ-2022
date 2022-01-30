using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerActionsManager _playerActionsManager;
    [SerializeField] private AchivementManager _achivementManager;
    [SerializeField] private GameplaySettings _settings;

    [Header("Time params")]
    [SerializeField] private int _secondsPerHour = 60;
    [SerializeField] private int _startingHour = 9;
    [SerializeField] private int _endingHour = 17;

    private IList<ICharacter> _characters;
    private PartenerCollection _expectedResult;
    private PartenerCollection _playerResult;

    private float _secondsBuffer = 0f;

    public int CurrentDay { get; set; }
    public int CurrentHour { get; set; }
    public WorldEventData CurrentEvent { get; set; }
    public List<(Character character, GeneratedForm form)> CurrentCharactersAndForms { get; set; }
    public GameplaySettings Settings => _settings;

    public void StartGame()
    {
        _uiManager.Clear();
        CurrentDay = 1;
        CurrentHour = _startingHour;
        _uiManager.DisplayHour(CurrentHour);
        SetupStressLevel(!_settings.StressLess);
        _uiManager.SetMainMenuVisible(false);
        _secondsBuffer = 0;
        CurrentEvent = PickEvent();
        CurrentCharactersAndForms = CharactersGenerationManager.Instance.GenerateCharactersWithForm(8, CurrentEvent);
        GenerateSolution();
        GenerateFormsDocs();
        RadioManager.Instance.InitState();
        enabled = true;
    }

    public void SetupStressLevel(bool visible)
    {
        _uiManager.ShowHour(visible);
        _uiManager.ShowScore(visible);
        _uiManager.ShowResultRightColumn(visible);
    }

    public void OnButtonNextDayClick()
    {
        enabled = false;
        RadioManager.Instance.ResetState();
        InitiateNewDay();
        Resolve();
    }

    private void InitiateNewDay()
    {
        _secondsBuffer = 0;
        CurrentHour = _startingHour;
        CurrentDay += 1;
    }

    public void GenerateSolution()
    {
        _characters = CurrentCharactersAndForms.ConvertAll<ICharacter>(e => e.character);
        _expectedResult = BestTenPairMatching.Instance.Process(_characters, CurrentEvent);
        _playerResult = new PartenerCollection(_characters.Count);
    }

    public void OnButtonBackToMainMenuClick()
    {
        enabled = false;
        _playerActionsManager.Clear();
        RadioManager.Instance.ResetState();
        _uiManager.SetMainMenuVisible(true);
    }

    public void OnButtonQuitClick()
    {
        Application.Quit();
    }

    private void Resolve()
    {
        Rating rating;

        // Treat unmatched as singles
        foreach (var unmatched in _characters.Where(c => !_playerActionsManager.StoredMatches.SelectMany(m => m).Contains(c)))
            _playerResult.AddSingle(unmatched);

        foreach (var pairing in _playerActionsManager.StoredMatches)
        {
            if (pairing.Count > 1)
            {
                rating = MatchmakingManager.Instance.Match(pairing[0], pairing[1]);
                _playerResult.Add(pairing[0], pairing[1], rating);
            }
            else
            {
                foreach (var single in pairing) // might be empty
                    _playerResult.AddSingle(single);
            }
        }

        _uiManager.DisplayResult(_playerResult, _expectedResult);
    }

    private void GenerateFormsDocs()
    {
        foreach (var (character, form) in CurrentCharactersAndForms)
        {
            _uiManager.AddDragDropFormDoc(character, form);
        }
    }

    private WorldEventData PickEvent()
    {
        var availableEvents = new List<WorldEventData>(_settings.Events);
        availableEvents.Shuffle();
        int rand = Random.Range(0, 100);
        float current = 0;
        foreach (var ev in availableEvents)
        {
            current += ev.Probability;
            if (rand < current)
                return ev;
        }

        return availableEvents.Last();
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        _secondsBuffer += Time.deltaTime;
        if ((int)_secondsBuffer > _secondsPerHour)
        {
            CurrentHour++;
            if (Settings.StressLess && CurrentHour > 23)
            {
                InitiateNewDay();
            }
            else if (!Settings.StressLess && CurrentHour > _endingHour)
            {
                OnButtonNextDayClick();
            }

            _uiManager.DisplayHour(CurrentHour);
            _secondsBuffer -= (float)_secondsPerHour;
        }
    }
}
