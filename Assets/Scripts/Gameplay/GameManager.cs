using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private PlayerActionsManager _playerActionsManager;

    [Header("Time params")]
    [SerializeField] private float _secondsPerHour = 60f;
    [SerializeField] private int _startingHour = 9;
    [SerializeField] private int _endingHour = 19;

	private IList<ICharacter> _characters;
    private PartenerCollection _expectedResult;
    private PartenerCollection _playerResult;

    private float _secondsBuffer = 0f;
	
    public int CurrentDay { get; set; }
    public int CurrentHour { get; set; }
    public List<(Character character, GeneratedForm form)> CurrentCharactersAndForms { get; set; }

    public void StartGame()
    {
        CurrentDay = 1;
        CurrentHour = _startingHour;
        _uiManager.DisplayHour(CurrentHour);
        _uiManager.SetMainMenuVisible(false);
        _secondsBuffer = 0;

        CurrentCharactersAndForms = CharactersGenerationManager.Instance.GenerateCharactersWithForm(8);
        GenerateFormsDocs();
        GenerateMonstersAndSolution();
        enabled = true;
    }

    public void OnButtonNextDayClick()
    {
        enabled = false;
        CurrentHour = _startingHour;
        _secondsBuffer = 0;
        Resolve();
    }
    public void GenerateMonstersAndSolution()
    {
        var generation = CharactersGenerationManager.Instance.GenerateCharactersWithForm(8);
        _characters = generation.ConvertAll<ICharacter>(e => e.character);
        _expectedResult = BruteForcePairMatching.Instance.Process(_characters);
        _playerResult = new PartenerCollection(_characters.Count);
        foreach (var partener in parteners)
        {
            Debug.Log(partener);
        }
    }

    public void OnButtonBackToMainMenuClick()
    {
        enabled = false;
        _playerActionsManager.Clear();
        _uiManager.SetMainMenuVisible(true);
    }

    public void OnButtonQuitClick()
    {
        Application.Quit();
    }
    
    private void Resolve()
    {
        //int score;
        //foreach (var pairing in _playerActionsManager.StoredMatches)
        //{
        //    score = MatchmakingManager.Instance.Match(pairing[0], pairing[1]);
        //    _playerResult.Add(pairing[0], pairing[1], score);
        //}

        _uiManager.DisplayResult(_expectedResult, _expectedResult);
    }

    private void GenerateFormsDocs()
    {
        foreach (var (character, form) in CurrentCharactersAndForms)
        {
            _uiManager.AddDragDropFormDoc(character, form);
        }
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
        if (_secondsBuffer > _secondsPerHour)
        {
            CurrentHour++;
            if (CurrentHour > _endingHour)
            {
                OnButtonNextDayClick();
            }
            _uiManager.DisplayHour(CurrentHour);

            _secondsBuffer -= _secondsPerHour;
        }
    }
}
