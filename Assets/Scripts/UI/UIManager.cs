using GGJ.Matchmaking;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using GGJ.Characters;

using System.Collections.Generic;

using System.Linq;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Drag and drop requirements")]
    [SerializeField] private Transform _unzoomedContainer;
    [SerializeField] private Transform _zoomedContainer;
    [SerializeField] private Transform[] _formsSpawnLocations;

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
    [SerializeField] private List<ResultRow> _playerRows;
    [SerializeField] private List<ResultRow> _expectedRows;
    [SerializeField] private TextMeshProUGUI _scoreText;

    public Transform UnzoomedContainer => _unzoomedContainer;
    public Transform ZoomedContainer => _zoomedContainer;

    private List<Transform> _availableFormSpawns;
    private List<GameObject> _formDocs = new List<GameObject>();

    public void SetMainMenuVisible(bool visible)
    {
        _mainMenu.SetActive(visible);
    }

    public void SetOptionsMenuVisible(bool visible)
    {
        _optionsMenu.SetActive(visible);
    }

    public void SetAchievementMenuVisible(bool visible)
    {
        _achievementsMenu.SetActive(visible);
    }

    public void SetCreditsMenuVisible(bool visible)
    {
        _creditsMenu.SetActive(visible);
    }

    public void DisplayResult(PartenerCollection player, PartenerCollection expected)
    {
        MatchmakingManager matchmakingMgr = MatchmakingManager.Instance;
        int i = 0;
        foreach (var p in player)
        {
            _playerRows[i].Setup(p.Character1, p.Character2, matchmakingMgr.Settings.GetMatchingClassification(p.Score));
            i++;
        }

        for (var p = 0; p < player.Singles.Count - 1; p++)
        {
            _playerRows[i].Setup(player.Singles[p], player.Singles[p + 1], matchmakingMgr.Settings.SingleClassification);
            if (p % 2 > 0)
                i++;
        }

        i = 0;
        foreach (var e in expected)
        {
            _expectedRows[i].Setup(e.Character1, e.Character2, matchmakingMgr.Settings.GetMatchingClassification(e.Score));
            i++;
        }

        for (var p = 0; p < expected.Singles.Count - 1; p++)
        {
            _expectedRows[i].Setup(expected.Singles[p], expected.Singles[p + 1], matchmakingMgr.Settings.SingleClassification);
            if (p % 2 > 0)
                i++;
        }

        _scoreText.text = $"{player.GetTotalScore()}/{expected.GetTotalScore()}";
        _resultPanel.SetActive(true);
    }

    public void Reset()
    {
        foreach (var doc in _formDocs)
        {
            Destroy(doc);
        }
        _availableFormSpawns = new List<Transform>(_formsSpawnLocations);
        _resultPanel.SetActive(false);
    }

    public void DisplayHour(int currentHour)
    {
        _hourDisplay.text = $"{currentHour}H";
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