using TMPro;
using UnityEngine;
using GGJ.Characters;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Drag and drop requirements")]
    [SerializeField] private Transform _unzoomedContainer;
    [SerializeField] private Transform _zoomedContainer;
    [SerializeField] private RectTransform _formsSpawnLocation;

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
        formDoc.GetComponent<RectTransform>().SetParent(_unzoomedContainer);
        formDoc.GetComponent<RectTransform>().position = _formsSpawnLocation.position;
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
    }
}