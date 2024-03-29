using GGJ.Characters;
using GGJ.Matchmaking;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Drag and drop requirements")]
    [SerializeField] private Transform _unzoomedContainer;
    [SerializeField] private Transform _zoomedContainer;
    [SerializeField] private Transform[] _formsSpawnLocations;
    [SerializeField] private MatchFolder[] _folders;

    [Header("Overlay elements")]
    [SerializeField] private TextMeshProUGUI _hourDisplay;

    [Header("Menus")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _achievementsMenu;
    [SerializeField] private GameObject _creditsMenu;

    [Header("Prefabs")]
    [SerializeField] private GameObject _formPrefab;

    [Header("Results")]
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private GameObject _leftColumn;
    [SerializeField] private GameObject _rightColumn;
    [SerializeField] private List<ResultRow> _playerRows;
    [SerializeField] private List<ResultRow> _expectedRows;
    [SerializeField] private TextMeshProUGUI _scoreText;

    [Header("Achivements")]
    [SerializeField] private List<LovePolaroid> _lovePolaroids;
    [SerializeReference] private Button _previousButton;
    [SerializeReference] private Button _nextButton;
    [SerializeReference] private Button _megaHeartFilterButton;
    [SerializeReference] private Button _heartFilterButton;
    [SerializeReference] private Button _brokenHeartFilterButton;
    [SerializeReference] private Button _skullFilterButton;

    public Transform UnzoomedContainer => _unzoomedContainer;
    public Transform ZoomedContainer => _zoomedContainer;
    public List<LovePolaroid> LovePolaroids => _lovePolaroids;

    private List<Transform> _availableFormSpawns;
    private List<GameObject> _formDocs = new List<GameObject>();

    public void SetMainMenuVisible(bool visible)
    {
        _mainMenu.SetActive(visible);
    }

    public void SetOptionsMenuVisible(bool visible)
    {
        _optionsMenu.SetActive(visible);
        _mainMenu.SetActive(!visible);
    }

    public void SetAchievementMenuVisible(bool visible)
    {
        AchivementManager.Instance.Refresh(reset: true);
        _achievementsMenu.SetActive(visible);
        _mainMenu.SetActive(!visible);
    }

    public void SetCreditsMenuVisible(bool visible)
    {
        _creditsMenu.SetActive(visible);
        _mainMenu.SetActive(!visible);
    }

    public void DisplayResult(PartenerCollection player, PartenerCollection expected)
    {
        int playerScore = 0;
        int bestScore = 0;
        MatchmakingManager matchmakingMgr = MatchmakingManager.Instance;
        int i = 0;
        Rating currentRange;
        foreach (var p in player)
        {
            currentRange = p.Rating;
            playerScore += currentRange.Scoring;
            _playerRows[i].Setup(p.Character1, p.Character2, currentRange);
            i++;

            var polaroidData = new LovePolaroidData(
                nameA: p.Character1.Name,
                imageA: p.Character1.Skin.Avatar,
                nameB: p.Character2.Name,
                imageB: p.Character2.Skin.Avatar,
                status: currentRange.Status
                );
            AchivementManager.Instance.Collection.Add(polaroidData);
        }

        if (player.Singles.Any())
        {
            playerScore += matchmakingMgr.Settings.SingleClassification.Scoring * player.Singles.Count / 2;
            for (var p = 0; p < player.Singles.Count - 1; p += 2)
            {
                _playerRows[i].Setup(player.Singles[p], player.Singles[p + 1], matchmakingMgr.Settings.SingleClassification);
                i++;
            }
        }

        i = 0;
        foreach (var e in expected)
        {
            currentRange = e.Rating;
            bestScore += currentRange.Scoring;
            _expectedRows[i].Setup(e.Character1, e.Character2, currentRange);
            i++;
        }

        if (expected.Singles.Any())
        {
            bestScore += matchmakingMgr.Settings.SingleClassification.Scoring * expected.Singles.Count / 2;
            for (var p = 0; p < expected.Singles.Count - 1; p += 2)
            {
                _expectedRows[i].Setup(expected.Singles[p], expected.Singles[p + 1], matchmakingMgr.Settings.SingleClassification);
                i++;
            }
        }

        _scoreText.text = $"{playerScore}/{bestScore}";
        _resultPanel.SetActive(true);
    }

    public void Clear()
    {
        foreach (var doc in _formDocs)
        {
            Destroy(doc);
        }
        _availableFormSpawns = new List<Transform>(_formsSpawnLocations);
        _resultPanel.SetActive(false);
        foreach (var f in _folders)
        {
            f.Clear();
        }
    }

    public void EnableAchivementsButtons(bool previousEnabled, bool nextEnabled, bool megaHeartEnabled, bool heartEnabled, bool borkenHeartEnabled, bool skullEnabled)
    {
        _previousButton.interactable = previousEnabled;
        _nextButton.interactable = nextEnabled;

        _megaHeartFilterButton.interactable = megaHeartEnabled;
        _heartFilterButton.interactable = heartEnabled;
        _brokenHeartFilterButton.interactable = borkenHeartEnabled;
        _skullFilterButton.interactable = skullEnabled;
    }

    public void ResetFilterButtons(bool megaHeartSelected, bool heartSelected, bool borkenHeartSelected, bool skullSelected)
    {
        if (megaHeartSelected)
        {
            _megaHeartFilterButton.GetComponent<SelectedButton>().ForceState(false);
        }
        if (heartSelected)
        {
            _heartFilterButton.GetComponent<SelectedButton>().ForceState(false);
        }
        if (borkenHeartSelected)
        {
            _brokenHeartFilterButton.GetComponent<SelectedButton>().ForceState(false);
        }
        if (skullSelected)
        {
            _skullFilterButton.GetComponent<SelectedButton>().ForceState(false);
        }
    }

    public void DisplayHour(int currentHour)
    {
        _hourDisplay.text = $"{currentHour}H";
    }

    public void ShowHour(bool visible)
    {
        _hourDisplay.gameObject.SetActive(visible);
    }

    public void ShowScore(bool visible)
    {
        _scoreText.gameObject.SetActive(visible);
    }

    public void ShowResultRightColumn(bool visible)
    {
        _rightColumn.SetActive(visible);
    }

    public void AddDragDropFormDoc(Character character, GeneratedForm form)
    {
        var formDoc = Instantiate(_formPrefab);
        formDoc.name = $"D&DDoc({character.Name})";
        formDoc.transform.position = _availableFormSpawns.First().position;
        _availableFormSpawns.RemoveAt(0);
        formDoc.transform.SetParent(_unzoomedContainer);
        formDoc.GetComponent<UIFormDoc>().FillForm(character, form);
        _formDocs.Add(formDoc);
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        _mainMenu.SetActive(true);
    }
}