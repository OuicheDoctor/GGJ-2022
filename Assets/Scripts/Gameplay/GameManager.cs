using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] private TextMeshProUGUI _eventDisplay;

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
        _eventDisplay.text = CurrentEvent.Headline;
        AudioManager.Instance.StopCurrentBGM();
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
        AudioManager.Instance.PlayBGM("Main Menu");
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

        // TODO maybe clean percentage calculation code location
        var percentage = _playerResult.GetEstimation() * 100 / _expectedResult.GetEstimation();
        if (percentage >= 60) AudioManager.Instance.PlayBGM("End Game Win");
        else AudioManager.Instance.PlayBGM("End Game Loose");

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
        var availableEvents = _settings.Events.Concat(_settings.NonChillEvents).ToList();
        int maxProba = Mathf.CeilToInt(availableEvents.Sum(e => e.Probability));
        availableEvents.Shuffle();
        int rand = Random.Range(0, maxProba);
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
        AudioManager.Instance.PlayBGM("Main Menu");
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