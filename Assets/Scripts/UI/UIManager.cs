using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private Transform _unzoomedContainer;
    [SerializeField] private Transform _zoomedContainer;

    public Transform UnzoomedContainer => _unzoomedContainer;
    public Transform ZoomedContainer => _zoomedContainer;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
}