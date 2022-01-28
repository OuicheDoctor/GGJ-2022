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

    public Transform UnzoomedContainer => _unzoomedContainer;
    public Transform ZoomedContainer => _zoomedContainer;

    private List<Transform> _availableFormSpawns;

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
        _availableFormSpawns = new List<Transform>(_formsSpawnLocations);
    }
}