using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class Zoomable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IBeginDragHandler
{
    [SerializeField] private float _unzoomedScale = .5f;
    [SerializeField] private float _zoomedScale = 1f;
    [SerializeField] private float _zoomingDuration = .5f;

    private bool _zoomed = false;
    private bool _wasPointerDownOnIt = false;
    private UIManager _uiManager;

    public void OnBeginDrag(PointerEventData eventData)
    {
        _wasPointerDownOnIt = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _wasPointerDownOnIt = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (_wasPointerDownOnIt)
        {
            ToggleZoom();
            _wasPointerDownOnIt = false;
        }
    }

    public void ToggleZoom()
    {
        transform.SetParent(_zoomed ? _uiManager.UnzoomedContainer : _uiManager.ZoomedContainer);
        transform.DOScale(_zoomed ? _unzoomedScale : _zoomedScale, _zoomingDuration);
        _zoomed = !_zoomed;
    }

    private void Awake()
    {
        transform.localScale = Vector3.one * _unzoomedScale;
    }

    private void Start()
    {
        _uiManager = UIManager.Instance;
    }
}