using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Dependencies")]
    [SerializeField] private UIManager _uiManager;

    [Header("Time params")]
    [SerializeField] private float _secondsPerHour = 60f;
    [SerializeField] private int _startingHour = 9;
    [SerializeField] private int _endingHour = 19;

    private float _secondsBuffer = 0f;

    public int CurrentDay { get; set; }
    public int CurrentHour { get; set; }

    public void StartGame()
    {
        CurrentDay = 1;
        CurrentHour = _startingHour;
        _uiManager.DisplayHour(CurrentHour);
        _uiManager.SetMainMenuVisible(false);
        _secondsBuffer = 0;
        enabled = true;
    }

    public void OnButtonNextDayClick()
    {
        enabled = false;
        CurrentDay++;
        CurrentHour = _startingHour;
        _secondsBuffer = 0;
        enabled = true;
    }

    public void OnButtonBackToMainMenuClick()
    {
        enabled = false;
        _uiManager.SetMainMenuVisible(true);
    }

    public void OnButtonQuitClick()
    {
        Application.Quit();
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