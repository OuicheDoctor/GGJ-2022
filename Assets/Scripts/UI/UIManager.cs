using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Drag and drop requirements")]
    [SerializeField] private Transform _unzoomedContainer;
    [SerializeField] private Transform _zoomedContainer;

    [Header("Overlay elements")]
    [SerializeField] private TextMeshProUGUI _hourDisplay;

    [Header("Menus")]
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionsMenu;
    [SerializeField] private GameObject _achievementsMenu;
    [SerializeField] private GameObject _creditsMenu;

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